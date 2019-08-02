using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using Utilities.Types;
using Utilities.Extensions;
using System.Reflection.Emit;
using System.Reflection;

namespace Utilities.Types
{
    public class ClassBuilder : IEquatable<ClassBuilder>
    {
        readonly TypeBuilder _builder;
        readonly List<(string Name, Type Type)> _properties = new List<(string Name, Type Type)>();
        Type _type;

        public ClassBuilder()
        {
            _builder = getTypeBuilder();

            TypeBuilder getTypeBuilder()
            {
                var typeSignature = "MyDynamicType";
                var an = new AssemblyName(typeSignature);
                var assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(an, AssemblyBuilderAccess.Run);
                var moduleBuilder = assemblyBuilder.DefineDynamicModule("MainModule");
                var tb = moduleBuilder.DefineType(typeSignature,
                        TypeAttributes.Public |
                        TypeAttributes.Class |
                        TypeAttributes.AutoClass |
                        TypeAttributes.AnsiClass |
                        TypeAttributes.BeforeFieldInit |
                        TypeAttributes.AutoLayout,
                        null);
                tb.DefineDefaultConstructor(
                    MethodAttributes.Public | 
                    MethodAttributes.SpecialName | 
                    MethodAttributes.RTSpecialName);

                return tb;
            }
        }

        public bool HasSameProperties(IEnumerable<(string Name, Type Type)> properties)
        {
            return _properties.Select(p => p.Name).SequenceEqual(properties.Select(p => p.Name))
                && _properties.Select(p => p.Type).SequenceEqual(properties.Select(p => p.Type));
        }

        public object Instantiate(IEnumerable<object> propertiesValues)
        {
            if (_type == null)
            {
                throw new InvalidOperationException("The type must be created before instantiating");
            }
            propertiesValues = propertiesValues.MakeCached();
            if (propertiesValues.Count() != _properties.Count)
            {
                throw new ArgumentException();
            }

            var obj = Activator.CreateInstance(_type);
            foreach (var i in _properties.Count.Range())
            {
                _type.GetProperty(_properties[i].Name).SetValue(obj, propertiesValues.ElementAt(i));
            }

            return obj;
        }

        public void FinishBuilding()
        {
            if (_type != null)
            {
                throw new InvalidOperationException();
            }

            _type = _builder.CreateType();
        }

        public void AddProperty(string name, Type type)
        {
            if (_type != null)
            {
                throw new InvalidOperationException("The type has already been made");
            }

            FieldBuilder fieldBuilder = _builder.DefineField("_" + name, type, FieldAttributes.Private);

            PropertyBuilder propertyBuilder = _builder.DefineProperty(name, PropertyAttributes.HasDefault, type, null);
            MethodBuilder getPropMthdBldr = _builder.DefineMethod("get_" + name, MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, type, Type.EmptyTypes);
            ILGenerator getIl = getPropMthdBldr.GetILGenerator();

            getIl.Emit(OpCodes.Ldarg_0);
            getIl.Emit(OpCodes.Ldfld, fieldBuilder);
            getIl.Emit(OpCodes.Ret);

            MethodBuilder setPropMthdBldr =
                _builder.DefineMethod("set_" + name,
                  MethodAttributes.Public |
                  MethodAttributes.SpecialName |
                  MethodAttributes.HideBySig,
                  null, new[] { type });

            ILGenerator setIl = setPropMthdBldr.GetILGenerator();
            Label modifyProperty = setIl.DefineLabel();
            Label exitSet = setIl.DefineLabel();

            setIl.MarkLabel(modifyProperty);
            setIl.Emit(OpCodes.Ldarg_0);
            setIl.Emit(OpCodes.Ldarg_1);
            setIl.Emit(OpCodes.Stfld, fieldBuilder);

            setIl.Emit(OpCodes.Nop);
            setIl.MarkLabel(exitSet);
            setIl.Emit(OpCodes.Ret);

            propertyBuilder.SetGetMethod(getPropMthdBldr);
            propertyBuilder.SetSetMethod(setPropMthdBldr);

            _properties.Add((name, type));
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ClassBuilder);
        }

        public bool Equals(ClassBuilder other)
        {
            return other != null
                && _properties?.Count == other._properties?.Count
                && (_properties?.Select(p => p.Name)?.SequenceEqual(other._properties.Select(p => p.Name)) ?? true)
                && (_properties?.Select(p => p.Type)?.SequenceEqual(other._properties.Select(p => p.Type)) ?? true);
        }

        public override int GetHashCode()
        {
            return 80682163 + EqualityComparer<List<(string Name, Type Type)>>.Default.GetHashCode(_properties);
        }
    }
}
