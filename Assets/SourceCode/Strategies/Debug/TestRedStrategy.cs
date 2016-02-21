using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.SourceCode.Boxers;
using Assets.SourceCode.Boxers.Attacks;

namespace Assets.SourceCode.Strategies.Debug {
    public class TestRedStrategy : AbstractBoxingStrategy {

        public override void Act() {
            Do.Attack(Attacks.Jab);
            Do.Attack(Attacks.Jab);
            Do.RecoverStamina();
            Do.Attack(Attacks.LiverShot);
            Do.RecoverStamina();
            Do.Attack(Attacks.Jab);
            Do.RecoverStamina();
            Do.Attack(Attacks.Haymaker);
            Do.ChangeStance(Boxer.Stance.BLOCKING);
            Do.RecoverStaminaUpTo(90);
            Do.ChangeStance(Boxer.Stance.NORMAL);
        }

        protected override List<ConditionalStrategy> RegisterConditionalStrategies() {
            List<ConditionalStrategy> result = new List<ConditionalStrategy>();
            result.Add(new ConditionalStrategy(bs => bs.Stance == Boxer.Stance.BLOCKING, 10, CounterBlock));
            result.Add(new ConditionalStrategy(bs => bs.Stance == Boxer.Stance.DODGING, 10, CounterDodge));
            result.Add(new ConditionalStrategy(bs => bs.HitPoints < 15, 10, Finisher));
            return result;
        }

        private void Finisher() {
            Do.Attack(Attacks.Jab);
            Do.RecoverStaminaUpTo(50);
        }

        private void CounterDodge() {
            Do.Attack(Attacks.LiverShot);
            Do.RecoverStaminaUpTo(75);
        }

        private void CounterBlock() {
            Do.Attack(Attacks.Haymaker);
            Do.ChangeStance(Boxer.Stance.BLOCKING);
            Do.RecoverStaminaUpTo(75);
            Do.ChangeStance(Boxer.Stance.NORMAL); //peek
        }
    }
}
