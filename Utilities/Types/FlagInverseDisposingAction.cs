using System;

namespace Utilities.Types
{
    public class FlagInverseDisposingAction : DisposingAction
    {
        public FlagInverseDisposingAction(bool initialValue, Action<bool> flagSetter)
            : base(() => flagSetter(!initialValue))
        {
            flagSetter(initialValue);
        }
    }
}
