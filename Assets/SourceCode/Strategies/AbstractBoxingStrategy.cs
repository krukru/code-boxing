using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.SourceCode.Boxers;
using Assets.SourceCode.Api;
using System.Collections.ObjectModel;

namespace Assets.SourceCode.Strategies {
    public abstract class AbstractBoxingStrategy {

        private Boxer boxer;

        private ReadOnlyCollection<ConditionalStrategy> conditionalStrategies = null;
        public ReadOnlyCollection<ConditionalStrategy> ConditionalStrategies {
            get {
                if (conditionalStrategies == null) {
                    conditionalStrategies = RegisterConditionalStrategies().AsReadOnly();
                }
                return conditionalStrategies;
            }
        }

        public void SetBoxer(Boxer value) {
            if (value == null) {
                throw new ArgumentNullException("Boxer is null");
            }
            if (boxer != null) {
                throw new InvalidOperationException("Boxer is already set");
            }
            this.boxer = value;
        }

        protected BoxerApi Do { get { return boxer.Api; } }

        protected virtual List<ConditionalStrategy> RegisterConditionalStrategies() {
            //here you can give a set of conditions that will trigger
            //this way, your Act() is just the default Act() and
            //based on what condition triggers, that act will trigger
            return new List<ConditionalStrategy>();
        }

        public abstract void Act();
    }
}
