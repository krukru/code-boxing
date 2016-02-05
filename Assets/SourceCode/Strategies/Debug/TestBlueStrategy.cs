using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.SourceCode.Boxers;
using Assets.SourceCode.Boxers.Attacks;

namespace Assets.SourceCode.Strategies.Debug {
    public class TestBlueStrategy : AbstractBoxingStrategy {

        public override void Act() {
            Do.ChangeStance(Boxer.Stance.BLOCKING);
            Do.Attack(Attacks.LiverShot);
            Do.RecoverStamina();
            Do.Attack(Attacks.LiverShot);
            Do.ChangeStance(Boxer.Stance.DODGING);
            Do.RecoverStamina();
            Do.RecoverStamina();
        }
    }
}
