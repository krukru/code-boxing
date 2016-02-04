using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.SourceCode.Boxers;

namespace Assets.SourceCode.Events {
    public delegate void BoxerEventHandler(Boxer sender, EventArgs eventArgs);
    public delegate void BoxerEventHandler<T>(Boxer sender, T eventArgs) where T : EventArgs;
    public class BoxerEvent {

        public Delegate EventHandler { get; private set; }
        public Boxer Sender { get; private set; }
        public EventArgs EventArgs { get; private set; }

        public BoxerEvent(Delegate eventHandler, Boxer sender) {
            this.EventHandler = eventHandler;
            this.Sender = sender;
            this.EventArgs = EventArgs.Empty;
        }

        public BoxerEvent(Delegate eventHandler, Boxer sender, EventArgs eventArgs) {
            this.EventHandler = eventHandler;
            this.Sender = sender;
            this.EventArgs = eventArgs;
        }
    }
}
