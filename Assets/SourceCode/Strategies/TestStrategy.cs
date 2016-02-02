using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.SourceCode.Boxers;
using Assets.SourceCode.Boxers.Attacks;

namespace Assets.SourceCode.Strategies {
    class TestStrategy : AbstractBoxingStrategy {

        public override void Act() {
            Do.ChangeStance(Boxer.Stance.BLOCKING);
        }
    }
}
