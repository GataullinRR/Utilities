using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using Utilities.Extensions;
using Utilities.Types;

namespace Utilities.Extensions
{
    public static class EventEx
    {
        public static void Invoke(this EventHandler @event, object sender)
        {
            @event.Invoke(sender, EventArgs.Empty);
        }
    }
}
