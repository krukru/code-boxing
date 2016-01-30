using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.SourceCode.Fighters.Attacks {
    abstract class AbstractAttack {
        internal AbstractAttack() { }

        public abstract int FullDamage { get; }
        public abstract int BlockedDamage { get; }
        public abstract int StaminaCost { get; }
        public abstract int CastTimeInMs { get; }
        public abstract int StunDurationInMs { get; }
        public abstract bool IsDodgeable { get; }
    }
}
