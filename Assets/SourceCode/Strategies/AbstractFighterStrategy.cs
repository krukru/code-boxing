using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.SourceCode.ClientCores;
using Assets.SourceCode.Boxers;

namespace Assets.SourceCode.Strategies {
    abstract class AbstractFighterStrategy {

        private Boxer boxer;

        public Boxer Boxer {
            get { return boxer; }
            set {
                if (value == null) {
                    throw new ArgumentNullException("Fighter is null");
                }
                if (boxer != null) {
                    throw new InvalidOperationException("Fighter is already set");
                }
                this.boxer = value;
            }
        }

        protected BoxerApi Do { get { return boxer.Api; } }

        public abstract void Act();
    }
}
