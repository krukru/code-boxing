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
            boxer.Attack(attack);
        }
        internal void Attack(object param) {
            //for debugging, remove method in production
            Attack((AbstractAttack)param);
        }

        public void RecoverStamina() {
            Delay(1000);
            boxer.RecoverStamina();
        }

        public void ChangeStance(Boxer.Stance newStance) {
            if (boxer.BoxerStance != newStance) {
                boxer.ChangeStance(newStance);
            }
        }

        private void Delay(int milis) {
            Thread.Sleep(milis);
        }

        private void ResolveAccumulatedStun() {

        }
    }
}
