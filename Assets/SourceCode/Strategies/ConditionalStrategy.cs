using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.SourceCode.Boxers;

namespace Assets.SourceCode.Strategies {
    public class ConditionalStrategy : IComparable<ConditionalStrategy> {

        private Predicate<BoxerStatus> predicate;
        private int priority;
        private Action action;

        public ConditionalStrategy(Predicate<BoxerStatus> predicate, int priority, Action action) {
            this.predicate = predicate;
            this.priority = priority;
            this.action = action;
        }

        public void Act() {
            action.Invoke();
        }

        public bool PredicateSatisfied(BoxerStatus status) {
            return predicate.Invoke(status);
        }

        public int CompareTo(ConditionalStrategy other) {
            return priority.CompareTo(other.priority);
        }
    }
}
