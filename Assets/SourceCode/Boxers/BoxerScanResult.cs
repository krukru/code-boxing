using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.SourceCode.Boxers {
    public class BoxerStatus {
        public double HitPoints { get; private set; }
        public bool IsAttacking { get; private set; }
        public Boxer.Stance Stance { get; private set; }

        public BoxerStatus(Boxer boxer) {
            LoadStatus(boxer);
        }

        private void LoadStatus(Boxer boxer) {
            this.HitPoints = boxer.HitPoints;
            this.IsAttacking = boxer.IsCastingAttack;
            this.Stance = boxer.BoxerStance;
        }
    }
}
