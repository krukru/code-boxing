using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Assets.SourceCode.Fighters;
using Assets.SourceCode.Strategies;
using Assets.SourceCode.Threading;
using UnityEngine;

namespace Assets.SourceCode {
    class FightController : MonoBehaviour {

        public enum FightResult {
            RED_WON,
            BLUE_WON,
            DRAW,
            MUTUAL_KO,
        }

        private AbstractFighterStrategy redStrategy = new TestStrategy();
        private AbstractFighterStrategy blueStrategy = new DerpStrategy();

        private FighterWorker redFighterWorker;
        private FighterWorker blueFighterWorker;

        private void Start() {
            Fighter redFighter = new Fighter(redStrategy, Fighter.Color.RED);
            Fighter blueFighter = new Fighter(blueStrategy, Fighter.Color.BLUE);
            SubscribeToFighterEvents(redFighter);
            SubscribeToFighterEvents(blueFighter);
            ResolveFight(redFighter, blueFighter);
        }

        private void SubscribeToFighterEvents(Fighter fighter) {
            fighter.FightEnded += fighter_FightEnded;
            fighter.Punched += fighter_Punched;
        }

        private void fighter_Punched(Fighter sender) {
            Debug.Log(String.Format("{0} fighter punched!", sender.FighterColor));
        }

        private void fighter_FightEnded(Fighter sender) {
            Debug.Log("Fight ended");
        }

        private void ResolveFight(Fighter redFighter, Fighter blueFighter) {
            redFighterWorker = new FighterWorker(redFighter, blueFighter);
            blueFighterWorker = new FighterWorker(blueFighter, redFighter);

            Thread redFighterThread = new Thread(redFighterWorker.DoWork);
            Thread blueFighterThread = new Thread(blueFighterWorker.DoWork);

            //start the fight in 5 seconds
            DateTime startTime = DateTime.Now.AddSeconds(5);
            redFighterThread.Start(startTime);
            blueFighterThread.Start(startTime);
            Debug.Log("Threads started");
        }
    }
}
