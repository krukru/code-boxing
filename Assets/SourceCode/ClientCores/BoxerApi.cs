using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Assets.SourceCode.Boxers;
using Assets.SourceCode.Boxers.Attacks;

namespace Assets.SourceCode.ClientCores {
    class BoxerApi {

        private Boxer boxer;

        public BoxerApi(Boxer boxer) {
            this.boxer = boxer;
        }
        public void Attack(AbstractAttack attack) {
            boxer.Attack(attack);
        }

        private void Delay(int milis) {
            Thread.Sleep(milis);
        }

        private void ResolveAccumulatedStun() {

        }
    }
}
