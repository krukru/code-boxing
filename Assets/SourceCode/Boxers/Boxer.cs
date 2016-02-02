using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Assets.SourceCode.Client;
using Assets.SourceCode.Events;
using Assets.SourceCode.Boxers.Attacks;
using Assets.SourceCode.Strategies;
using Assets.SourceCode.Threading;

namespace Assets.SourceCode.Boxers {
    class Boxer {
        public event BoxerEventHandler<BoxerAttackEventArgs> AttackStarted;
        public event BoxerEventHandler<BoxerAttackEventArgs> AttackReceived;
        public event BoxerEventHandler FightEnded;
        public event BoxerEventHandler StanceChanged;

        public enum Color {
            RED,
            BLUE
        }
        public enum Stance {
            NORMAL,
            BLOCKING,
            DODGING,
            STAGGERING
        }

        private const int MAX_HIT_POINTS = 100;
        private const int MAX_STAMINA = 100;
        private const int STAMINA_RECOVERY_AMOUNT = 25;
        private const double MINIMAL_ATTACK_INTENSITY_FACTOR = 0.1;

        public Color BoxerColor { get; private set; }
        public Stance BoxerStance { get; private set; }
        public AbstractBoxingStrategy Strategy { get; private set; }
        public BoxerApi Api { get; private set; }

        public Boxer opponent; //change to private

        public Boxer(AbstractBoxingStrategy strategy, Color color) {
            this.Strategy = strategy;
            this.BoxerColor = color;

            this.Api = new BoxerApi(this);
            strategy.Boxer = this;
        }

        public void StartFighting(Boxer opponent) {
            this.opponent = opponent;
            bool fightActive = true;
            while (fightActive) {
                Strategy.Act();
                fightActive = false;
            }
            if (FightEnded != null) {
                FightEnded(this, null);
            }
        }

        public void Attack(AbstractAttack attack) {
            Emit(AttackStarted, new BoxerAttackEventArgs(attack));
            int castTime = attack.CastTimeInMs;
            Thread.Sleep(castTime);
            opponent.AttackLanded(attack);
        }
        public void ChangeStance(Stance newStance) {
            this.BoxerStance = newStance;
            Emit(StanceChanged);
        }

        private void Emit(BoxerEventHandler eventHandler) {
            if (eventHandler != null) {
                //eventHandler(this, EventArgs.Empty);
                EventDispatcher dispatcher = EventDispatcher.Instance;
                dispatcher.AddEvent(eventHandler, this, EventArgs.Empty);
            }
        }

        private void Emit<T>(BoxerEventHandler<T> eventHandler, T eventArgs) where T : EventArgs {
            if (eventHandler != null) {
                //eventHandler(this, eventArgs);
                EventDispatcher dispatcher = EventDispatcher.Instance;
                dispatcher.AddEvent(eventHandler, this, eventArgs);
            }
        }

        public void AttackLanded(AbstractAttack attack) {
            Emit(AttackReceived, new BoxerAttackEventArgs(attack));
        }

        internal void Attack(object param) {
            Attack((AbstractAttack)param);
        }
    }
}
