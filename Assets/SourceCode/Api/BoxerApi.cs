using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Assets.SourceCode.Boxers;
using Assets.SourceCode.Boxers.Attacks;

namespace Assets.SourceCode.Api {
    class BoxerApi {

        private Boxer boxer;

        public BoxerApi(Boxer boxer) {
            this.boxer = boxer;
        }
        public void Attack(AbstractAttack attack) {
            ResolveAccumulatedStun();
            boxer.Attack(attack);
        }

        public void RecoverStamina() {
            Delay(1000);
            boxer.RecoverStamina();
        }

        public void ChangeStance(Boxer.Stance newStance) {
            ResolveAccumulatedStun();
            if (boxer.BoxerStance != newStance) {
                boxer.ChangeStance(newStance);
            }
        }

        private void Delay(int milis) {
            Thread.Sleep(milis);
        }

        private void ResolveAccumulatedStun() {
            if (boxer.StunDuration > 0) {
                try {
                    Thread.Sleep(boxer.StunDuration);
                }
                catch (ThreadInterruptedException ex) {
                    //this should never happen! rethrow and log the error
                    throw ex;
                }
                finally {
                    boxer.ClearStun();
                }
            }
        }
    }
}
