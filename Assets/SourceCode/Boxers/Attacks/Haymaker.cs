using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.SourceCode.Boxers.Attacks {
    class Haymaker : AbstractAttack {
        public override int FullDamage {
            get { return 50; }
        }

        public override int BlockedDamage {
            get { return 10; }
        }

        public override int StaminaCost {
            get { return 75; }
        }

        public override int CastTimeInMs {
            get { return 1000; }
        }

        public override int StunDurationInMs {
            get { return 1000; }
        }

        public override bool IsDodgeable {
            get { return true; }
        }
    }
}
