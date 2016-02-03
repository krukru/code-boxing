using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.SourceCode.Boxers;

namespace Assets.SourceCode.Events {
    class BoxerStanceChangedEventArgs : EventArgs {
        public Boxer.Stance NewStance { get; private set; }
        public BoxerStanceChangedEventArgs(Boxer.Stance newStance) {
            this.NewStance = newStance;
        }
    }
}
