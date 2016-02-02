using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Assets.SourceCode.Boxers;
using Assets.SourceCode.Boxers.Attacks;

namespace Assets.SourceCode.Client {
    class BoxerApi {

        private Boxer boxer;

        public BoxerApi(Boxer boxer) {
            this.boxer = boxer;
        }
        public void Attack(AbstractAttack attack) {
            boxer.Attack(attack);
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
