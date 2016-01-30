using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.SourceCode.Fighters;
using Assets.SourceCode.Fighters.Attacks;

namespace Assets.SourceCode.ClientCores {
    class FighterApi {

        private Fighter fighter;

        public FighterApi(Fighter fighter) {
            this.fighter = fighter;
        }
        public void Attack(AbstractAttack attack) {
            fighter.Attack(attack);
        }
    }
}
