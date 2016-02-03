using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Assets.SourceCode.Api;
using Assets.SourceCode.Events;
using Assets.SourceCode.Boxers.Attacks;
using Assets.SourceCode.Strategies;
using Assets.SourceCode.Threading;
using Assets.SourceCode.Services;

namespace Assets.SourceCode.Boxers {
    class Boxer {
        public event BoxerEventHandler<BoxerAttackEventArgs> AttackStarted;
        public event BoxerEventHandler<BoxerAttackEventArgs> AttackReceived;
        public event BoxerEventHandler<BoxerStanceChangedEventArgs> StanceChanged;
        public event BoxerEventHandler<BoxerStaminaChangedEventArgs> StaminaRecovered;
        public event BoxerEventHandler FightEnded;

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

        public const int MAX_HIT_POINTS = 100; //maybe allow this to change as a bonus for the boxer
        public const int MAX_STAMINA = 100; //same as above
        public const int STAMINA_RECOVERY_AMOUNT = 25; //same as above

        public Color BoxerColor { get; private set; }
        public Stance BoxerStance { get; private set; }
        public AbstractBoxingStrategy Strategy { get; private set; }
        public BoxerApi Api { get; private set; }

        public int HitPoints { get; private set; }
        public int Stamina { get; private set; }
        public bool IsCastingAttack { get; private set; }

        public Boxer opponent; //change to private

        private DamageResolverService damageService = new DamageResolverService();

        public Boxer(AbstractBoxingStrategy strategy, Color color) {
            this.Strategy = strategy;
            this.BoxerColor = color;
            this.HitPoints = MAX_HIT_POINTS;
            this.Stamina = MAX_STAMINA;

            this.Api = new BoxerApi(this);
            strategy.Boxer = this;
        }

        public void StartFighting(Boxer opponent) {
            this.opponent = opponent;
            bool fightActive = true;
            int safety = 50;
            while (fightActive) {
                Strategy.Act();
                safety--;
                if (safety < 0) {
                    break;
                }
            }
            if (FightEnded != null) {
                // FightEnded(this, null);
            }
        }

        public void Attack(AbstractAttack attack) {
            this.IsCastingAttack = true;
            double attackIntensityFactor = damageService.GetAttackIntensityFactor(this);
            this.Stamina = Math.Max(0, this.Stamina - attack.StaminaCost);
            Emit(AttackStarted, new BoxerAttackEventArgs(attack));
            int castTime = attack.CastTimeInMs;
            try {
                Thread.Sleep(castTime);
                opponent.AttackLanded(attack, attackIntensityFactor);
            }
            catch (ThreadInterruptedException ex) {
                //attack interrupted
            }
            finally {
                this.IsCastingAttack = false;
            }
        }
        private void AttackLanded(AbstractAttack attack, double attackIntensityFactor) {
            int damage = damageService.GetDamage(this, attack, attackIntensityFactor);
            if (damage > 0) {
                this.HitPoints -= damage;
                Emit(AttackReceived, new BoxerAttackEventArgs(attack));
            }
        }

        public void RecoverStamina() {
            this.Stamina = Math.Min(MAX_STAMINA, Stamina + STAMINA_RECOVERY_AMOUNT);
            Emit(StaminaRecovered, new BoxerStaminaChangedEventArgs(Stamina));
        }

        public void ChangeStance(Stance newStance) {
            this.BoxerStance = newStance;
            Emit(StanceChanged, new BoxerStanceChangedEventArgs(newStance));
        }

        private void Emit(BoxerEventHandler eventHandler) {
            if (eventHandler != null) {
                //eventHandler(this, EventArgs.Empty);
                EventDispatcher dispatcher = EventDispatcher.Instance;
                BoxerEvent boxerEvent = new BoxerEvent(eventHandler, this);
                dispatcher.AddEvent(boxerEvent);
            }
        }

        private void Emit<T>(BoxerEventHandler<T> eventHandler, T eventArgs) where T : EventArgs {
            if (eventHandler != null) {
                //eventHandler(this, eventArgs);
                EventDispatcher dispatcher = EventDispatcher.Instance;
                BoxerEvent boxerEvent = new BoxerEvent(eventHandler, this, eventArgs);
                dispatcher.AddEvent(boxerEvent);
            }
        }
    }
}
