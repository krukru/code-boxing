﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.SourceCode.Boxers;
using Assets.SourceCode.Boxers.Attacks;

namespace Assets.SourceCode.Strategies.Debug {
    public class TestRedStrategy : AbstractBoxingStrategy {

        public override void Act() {
            Do.ChangeStance(Boxer.Stance.BLOCKING);
            Do.Attack(Attacks.Jab);
            Do.Attack(Attacks.Jab);
            Do.RecoverStamina();
            Do.Attack(Attacks.LiverShot);
            Do.RecoverStamina();
            Do.Attack(Attacks.Jab);
            Do.RecoverStamina();
            Do.Attack(Attacks.Haymaker);
            Do.RecoverStamina();
            Do.RecoverStamina();
            Do.RecoverStamina();
        }
    }
}
