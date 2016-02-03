using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.SourceCode.Boxers;
using Assets.SourceCode.Api;

namespace Assets.SourceCode.Strategies {
    abstract class AbstractBoxingStrategy {

        private Boxer boxer;

        public Boxer Boxer {
            get { return boxer; }
            set {
                if (value == null) {
                    throw new ArgumentNullException("Boxer is null");
                }
                if (boxer != null) {
                    throw new InvalidOperationException("Boxer is already set");
                }
                this.boxer = value;
            }
        }

        protected BoxerApi Do { get { return boxer.Api; } }

        public abstract void Act();
    }
}
