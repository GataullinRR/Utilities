using System;
using System.Collections.Generic;
using Utilities;
using Utilities.Extensions;
using Utilities.Types;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Reflection;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace Utilities.Types.Tracer
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
    public class TraceAttribute : Attribute
    {
        public readonly bool IsEnabled;

        public TraceAttribute(bool isEnabled)
        {
            IsEnabled = isEnabled;
        }
    }

    public static class Tracing
    {
        internal enum TraceDecision
        {
            NEXT = 0,
            IGNORE_OBJECT,
            IGNORE_MEMBER,
            IGNORE_LINE,
            COMPLETE_TRACING
        }

        readonly static List<Tracer> _tracers = new List<Tracer>();
        readonly static Tracer _completedTracer;

        static Tracing()
        {
            _completedTracer = new Tracer(null);
            _completedTracer.CompleteTracing();
        }

        public static Tracer GetTracer(Type owner, bool enableTracing = true)
        {
            if (!enableTracing)
            {
                return _completedTracer;
            }

            var trace = Assembly.GetCallingAssembly().GetCustomAttribute<TraceAttribute>().IsEnabled;
            if (trace)
            {
                var tracer = new Tracer(owner.FullName);
                _tracers.Add(tracer);

                return tracer;
            }
            else
            {
                return _completedTracer;
            }
        }

        public static void CompleteTracing()
        {
            _tracers.ForEach(t => t.CompleteTracing());
        }
    }

    public class Tracer
    {
        List<TraceDialog> _dialogs = new List<TraceDialog>();

        string _objectName;
        HashSet<string> _ignoredMembers = new HashSet<string>();
        HashSet<int> _ignoredLines = new HashSet<int>();
        bool _ignore;

        internal Tracer(string objectName)
        {
            _objectName = objectName;
        }

        public void CompleteTracing()
        {
            _ignore = true;

            _ignoredMembers = null;
            _ignoredLines = null;
            _objectName = null;
        }

        public void Trace(string commentary = "", [CallerMemberName]string member = "", [CallerLineNumber]int line = -1)
        {
            if (_ignore || _ignoredLines.Contains(line) || _ignoredMembers.Contains(member))
            {
                return;
            }

            var dialog = _dialogs.FirstOrDefault(d => d.Visible == false);
            if (dialog == null)
            {
                dialog = new TraceDialog();
                _dialogs.Add(dialog);
            }
            dialog.Initialize(_objectName, member, line.ToString(), commentary);
            dialog.ShowDialog();

            switch (dialog.Decision)
            {
                case Tracing.TraceDecision.NEXT:
                    break;
                case Tracing.TraceDecision.IGNORE_OBJECT:
                    CompleteTracing();
                    break;
                case Tracing.TraceDecision.IGNORE_MEMBER:
                    _ignoredMembers.Add(member);
                    break;
                case Tracing.TraceDecision.IGNORE_LINE:
                    _ignoredLines.Add(line);
                    break;
                case Tracing.TraceDecision.COMPLETE_TRACING:
                    Tracing.CompleteTracing();
                    break;
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
