using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Assets.SourceCode.Boxers;

namespace Assets.SourceCode.Events {
    internal delegate void BoxerEventHandler(Boxer sender, EventArgs eventArgs);
    internal delegate void BoxerEventHandler<T>(Boxer sender, T eventArgs) where T : EventArgs;

    class EventCore {

    }
}
