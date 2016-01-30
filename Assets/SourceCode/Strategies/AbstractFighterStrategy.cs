using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.SourceCode.ClientCores;
using Assets.SourceCode.Fighters;

namespace Assets.SourceCode.Strategies {
    abstract class AbstractFighterStrategy {

        private Fighter fighter;

        public Fighter Fighter {
            get { return fighter; }
            set {
                if (value == null) {
                    throw new ArgumentNullException("Fighter is null");
                }
                if (fighter != null) {
                    throw new InvalidOperationException("Fighter is already set");
                }
                this.fighter = value;
            }
        }

        protected FighterApi Do { get { return fighter.Api; } }

        public abstract void Act();
    }
}
