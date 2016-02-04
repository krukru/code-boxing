using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.SourceCode.Boxers.Attacks {
    public class LiverShot : AbstractAttack {
        public override int FullDamage {
            get { return 15; }
        }

        public override int BlockedDamage {
            get { return 10; }
        }

        public override int StaminaCost {
            get { return 30; }
        }

        public override int CastTimeInMs {
            get { return 350; }
        }

        public override int StunDurationInMs {
            get { return 100; }
        }

        public override bool IsDodgeable {
            get { return false; }
        }
    }
}
