using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.SourceCode.Fighters.Attacks {
    class Jab : AbstractAttack {
        public override int FullDamage {
            get { return 5; }
        }

        public override int BlockedDamage {
            get { return 0; }
        }

        public override int StaminaCost {
            get { return 15; }
        }

        public override int CastTimeInMs {
            get { return 600; }
        }

        public override int StunDurationInMs {
            get { return 0; }
        }

        public override bool IsDodgeable {
            get { return true; }
        }
    }
}
