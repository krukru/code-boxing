using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.SourceCode.Boxers.Attacks;

namespace Assets.SourceCode.Strategies {
    class TestStrategy : AbstractFighterStrategy {

        public override void Act() {
            Do.Attack(Attacks.Jab);
        }
    }
}
