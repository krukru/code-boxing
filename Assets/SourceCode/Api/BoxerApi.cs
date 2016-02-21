using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Assets.SourceCode.Boxers;
using Assets.SourceCode.Boxers.Attacks;
using Assets.SourceCode.Events;

namespace Assets.SourceCode.Api {
    public class BoxerApi {

        private Boxer boxer;

        public BoxerApi(Boxer boxer) {
            this.boxer = boxer;
        }
        public void Attack(AbstractAttack attack) {
            ResolveAccumulatedStun();
            boxer.Attack(attack);
        }

        public bool TryScanOpponent(out BoxerStatus status) {
            ResolveAccumulatedStun();
            if (boxer.CanSeeOpponent()) {
                status = new BoxerStatus(boxer.Opponent);
                return true;
            }
            else {
                status = null;
                return false;
            }
        }

        public void RecoverStamina() {
            try {
                Thread.Sleep(1000);
            }
            catch (Exception ex) {
                boxer.EmitDebugMessage(ex.Message, DebugMessageEventArgs.SeverityLevel.Error);
            }
            finally {
                boxer.RecoverStamina();
            }
        }

        public void RecoverStaminaUpTo(int targetStamina) {
            if (targetStamina < 0 || targetStamina > 100) {
                throw new ArgumentException();
            }
            while (boxer.Stamina < targetStamina) {
                RecoverStamina();
            }
        }

        public void ChangeStance(Boxer.Stance newStance) {
            if (boxer.BoxerStance != newStance) {
                ResolveAccumulatedStun();
                boxer.ChangeStance(newStance);
            }
        }

        private void ResolveAccumulatedStun() {
            if (boxer.StunDuration > 0) {
                try {
                    Thread.Sleep(boxer.StunDuration);
                }
                catch (Exception ex) {
                    boxer.EmitDebugMessage(ex.Message, DebugMessageEventArgs.SeverityLevel.Error);
                }
                finally {
                    boxer.ClearStun();
                }
            }
        }
    }
}
