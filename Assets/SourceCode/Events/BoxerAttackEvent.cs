using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.SourceCode.Boxers;

namespace Assets.SourceCode.Events {
    internal delegate void BoxerAttackEventHandler(Boxer sender, BoxerAttackEventArgs eventArgs);
    class BoxerAttackEvent {

        public BoxerAttackEventHandler EventHandler { get; private set; }
        public Boxer Sender { get; private set; }
        public BoxerAttackEventArgs EventArgs { get; private set; }

        public BoxerAttackEvent(BoxerAttackEventHandler eventHandler, Boxer sender, BoxerAttackEventArgs eventArgs) {
            this.EventHandler = eventHandler;
            this.Sender = sender;
            this.EventArgs = eventArgs;
        }
    }
}
