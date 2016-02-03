using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.SourceCode.Boxers;
using Assets.SourceCode.Boxers.Attacks;

namespace Assets.SourceCode.Strategies.Debug {
    class TestRedStrategy : AbstractBoxingStrategy {

        public override void Act() {
            Do.Attack(Attacks.Haymaker);
            Do.RecoverStamina();
            Do.RecoverStamina();
            Do.Attack(Attacks.Jab);
            Do.Attack(Attacks.Jab);
            Do.Attack(Attacks.Jab);
            Do.RecoverStamina();
            Do.RecoverStamina();
        }
    }
}
