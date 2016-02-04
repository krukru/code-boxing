using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.SourceCode.Boxers;

namespace Assets.SourceCode.Events {
    public class BoxerStaminaChangedEventArgs : EventArgs {
        public int NewStamina { get; private set; }
        public BoxerStaminaChangedEventArgs(int newStamina) {
            this.NewStamina = newStamina;
        }
    }
}
