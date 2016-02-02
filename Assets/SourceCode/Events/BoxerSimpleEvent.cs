using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.SourceCode.Boxers;

namespace Assets.SourceCode.Events {
    internal delegate void BoxerSimpleEventHandler(Boxer sender);
    class BoxerSimpleEvent {

        public BoxerSimpleEventHandler EventHandler { get; private set; }
        public Boxer Sender { get; private set; }

        public BoxerSimpleEvent(BoxerSimpleEventHandler eventHandler, Boxer sender) {
            this.EventHandler = eventHandler;
            this.Sender = sender;
        }
    }
}
