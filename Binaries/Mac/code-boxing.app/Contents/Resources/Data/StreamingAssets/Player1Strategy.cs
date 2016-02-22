using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.SourceCode.Strategies;
using Assets.SourceCode.Boxers;
using Assets.SourceCode.Boxers.Attacks;
class Player1Strategy : AbstractBoxingStrategy {
    public override void Act() {
        Do.ChangeStance(Boxer.Stance.BLOCKING);
        Do.Attack(Attacks.Jab);
        Do.Attack(Attacks.Jab);
        Do.RecoverStamina();
    }
}
