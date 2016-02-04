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
    public class Boxer {
        public event BoxerEventHandler<BoxerAttackEventArgs> AttackStarted;
        public event BoxerEventHandler<BoxerAttackEventArgs> AttackReceived;
        public event BoxerEventHandler<BoxerStanceChangedEventArgs> StanceChanged;
        public event BoxerEventHandler<BoxerStaminaChangedEventArgs> StaminaRecovered;

        public event BoxerEventHandler<DebugMessageEventArgs> ShitHappened;

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

        public double HitPoints { get; private set; }
        public int Stamina { get; private set; }
        public bool IsCastingAttack { get; private set; }
        public int StunDuration { get; private set; }
        public bool IsKnockedDown {
            get {
                return HitPoints <= 0;
            }
        }

        private Boxer opponent;

        private DamageResolverService damageService = new DamageResolverService();

        private Stance stanceBeforeStun;

        private volatile bool fightActive;

        public Boxer(AbstractBoxingStrategy strategy, Color color) {
            this.Strategy = strategy;
            this.BoxerColor = color;
            this.HitPoints = MAX_HIT_POINTS;
            this.Stamina = MAX_STAMINA;

            this.Api = new BoxerApi(this);
            strategy.SetBoxer(this);
        }

        public void StartFighting(Boxer opponent) {
            this.opponent = opponent;
            this.fightActive = true;
            int safety = 50;
            while (fightActive) {
                Strategy.Act();
                safety--;
                if (safety < 0) {
                    break;
                }
            }
        }
        public void StopFighting() {
            this.fightActive = false;
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
            catch (ThreadInterruptedException) {
                //attack interrupted
            }
            finally {
                this.IsCastingAttack = false;
            }
        }
        private void AttackLanded(AbstractAttack attack, double attackIntensityFactor) {
            double damage = damageService.GetDamage(this, attack, attackIntensityFactor);
            if (damage > 0) {
                this.HitPoints -= damage;
                Emit(AttackReceived, new BoxerAttackEventArgs(attack));
                int stunDuration = damageService.GetStunDuration(this, attack, attackIntensityFactor);
                if (stunDuration > 0) {
                    AddStun(stunDuration);
                }
            }
        }

        private void AddStun(int stunDuration) {
            if (stunDuration < 0) {
                throw new ArgumentException();
            }
            this.StunDuration += stunDuration;
            if (BoxerStance != Stance.STAGGERING) {
                this.stanceBeforeStun = BoxerStance;
                ChangeStance(Stance.STAGGERING);
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
            if (fightActive && eventHandler != null) {
                EventDispatcher dispatcher = EventDispatcher.Instance;
                BoxerEvent boxerEvent = new BoxerEvent(eventHandler, this);
                dispatcher.AddEvent(boxerEvent);
            }
        }

        private void Emit<T>(BoxerEventHandler<T> eventHandler, T eventArgs) where T : EventArgs {
            if (fightActive && eventHandler != null) {
                EventDispatcher dispatcher = EventDispatcher.Instance;
                BoxerEvent boxerEvent = new BoxerEvent(eventHandler, this, eventArgs);
                dispatcher.AddEvent(boxerEvent);
            }
        }

        public void EmitDebugMessage(string message) {
            InternalEmitDebugMessage(message, DebugMessageEventArgs.SeverityLevel.Info);
        }

        public void EmitDebugMessage(string message, DebugMessageEventArgs.SeverityLevel severity) {
            InternalEmitDebugMessage(message, severity);
        }

        private void InternalEmitDebugMessage(string message, DebugMessageEventArgs.SeverityLevel severity) {
            if (ShitHappened != null) {
                System.Diagnostics.StackFrame frame = new System.Diagnostics.StackFrame(1);
                var callerMethod = frame.GetMethod();
                var methodName = callerMethod.Name;
                EventDispatcher dispatcher = EventDispatcher.Instance;
                DebugMessageEventArgs eventArgs = new DebugMessageEventArgs(methodName, message);
                BoxerEvent boxerEvent = new BoxerEvent(ShitHappened, this, eventArgs);
                dispatcher.AddEvent(boxerEvent);
            }
        }

        public void ClearStun() {
            this.StunDuration = 0;
            ChangeStance(stanceBeforeStun);
        }
    }
}
