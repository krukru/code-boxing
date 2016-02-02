using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.SourceCode.Boxers.Attacks;

namespace Assets.SourceCode.Events {
    class BoxerAttackEventArgs : EventArgs {
        public AbstractAttack Attack { get; private set; }
        public BoxerAttackEventArgs(AbstractAttack attack) {
            this.Attack = attack;
        }
    }
}
