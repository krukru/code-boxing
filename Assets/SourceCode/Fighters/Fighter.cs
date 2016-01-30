using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Assets.SourceCode.ClientCores;
using Assets.SourceCode.Events;
using Assets.SourceCode.Fighters.Attacks;
using Assets.SourceCode.Strategies;

namespace Assets.SourceCode.Fighters {
    class Fighter {
        public event FighterEventHandler Punched;
        public event FighterEventHandler FightEnded;

        public enum Color {
            RED,
            BLUE
        }
        public enum Stance {
            NORMAL,
            BLOCKING,
            DODGING,
            OPEN /* stance when attacking or staggering */
        }

        private const int MAX_HIT_POINTS = 100;
        private const int MAX_STAMINA = 100;
        public const int STAMINA_RECOVERY_AMOUNT = 25;
        public const double MINIMAL_ATTACK_INTENSITY_FACTOR = 0.1;

        public Color FighterColor { get; private set; }
        public AbstractFighterStrategy Strategy { get; private set; }
        public FighterApi Api { get; private set; }

        private Fighter opponent;

        public Fighter(AbstractFighterStrategy strategy, Color color) {
            this.Strategy = strategy;
            this.FighterColor = color;

            this.Api = new FighterApi(this);
            strategy.Fighter = this;
        }

        public void StartFighting(Fighter opponent) {
            this.opponent = opponent;
            bool fightActive = true;
            while (fightActive) {
                Strategy.Act();
                fightActive = false;
            }
            if (FightEnded != null) {
                FightEnded(this);
            }
        }

        public void Attack(AbstractAttack attack) {
            opponent.AttackLanded(attack);
        }

        private void AttackLanded(AbstractAttack attack) {
            if (Punched != null) {
                Punched(this);
            }
        }
    }
}
