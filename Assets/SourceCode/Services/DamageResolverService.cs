using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.SourceCode.Boxers;
using Assets.SourceCode.Boxers.Attacks;

namespace Assets.SourceCode.Services {
    public class DamageResolverService {

        private const double MINIMAL_ATTACK_INTENSITY_FACTOR = 0.1;
        private const double STAGGERING_DAMAGE_MULTIPLIER = 2;
        private const double COUNTER_ATTACK_DAMAGE_MULTIPLIER = 2;

        public double GetDamage(Boxer defender, AbstractAttack attack, double attackIntensityFactor) {
            double result;
            if (defender.IsCastingAttack) {
                result = attack.FullDamage * COUNTER_ATTACK_DAMAGE_MULTIPLIER;
            }
            else {
                switch (defender.BoxerStance) {
                    case Boxer.Stance.NORMAL:
                        result = attack.FullDamage;
                        break;
                    case Boxer.Stance.BLOCKING:
                        result = attack.BlockedDamage;
                        break;
                    case Boxer.Stance.DODGING:
                        result = attack.IsDodgeable ? attack.FullDamage : 0;
                        break;
                    case Boxer.Stance.STAGGERING:
                        result = attack.FullDamage * STAGGERING_DAMAGE_MULTIPLIER;
                        break;
                    default:
                        throw new InvalidOperationException();
                }
            }
            return result * attackIntensityFactor;
        }

        public double GetAttackIntensityFactor(Boxer boxer) {
            return Math.Max(MINIMAL_ATTACK_INTENSITY_FACTOR, (double)boxer.Stamina / Boxer.MAX_STAMINA);
        }

        public int GetStunDuration(Boxer boxer, AbstractAttack attack, double attackIntensityFactor) {
            return attack.StunDurationInMs;
        }
    }
}
