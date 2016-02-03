using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.SourceCode.Boxers.Attacks {
    class Attacks {
        public static readonly AbstractAttack Jab = new Jab();
        public static readonly AbstractAttack Haymaker = new Haymaker();
        public static readonly AbstractAttack LiverShot = new LiverShot();
    }
}
