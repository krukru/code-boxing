using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Assets.SourceCode.Boxers;
using Assets.SourceCode.Strategies;
using Assets.SourceCode.Threading;
using UnityEngine;

namespace Assets.SourceCode.Controllers {
    class MatchController : MonoBehaviour {

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
            Boxer redFighter = new Boxer(redStrategy, Boxer.Color.RED);
            Boxer blueFighter = new Boxer(blueStrategy, Boxer.Color.BLUE);
            SubscribeToFighterEvents(redFighter);
            SubscribeToFighterEvents(blueFighter);
            ResolveFight(redFighter, blueFighter);
        }

        private void SubscribeToFighterEvents(Boxer boxer) {
            boxer.FightEnded += fighter_FightEnded;
            boxer.Punched += fighter_Punched;
        }

        private void fighter_Punched(Boxer sender) {
            Debug.Log(String.Format("{0} fighter punched!", sender.FighterColor));
        }

        private void fighter_FightEnded(Boxer sender) {
            Debug.Log("Fight ended");
        }

        private void ResolveFight(Boxer redBoxer, Boxer blueBoxer) {
            redFighterWorker = new FighterWorker(redBoxer, blueBoxer);
            blueFighterWorker = new FighterWorker(blueBoxer, redBoxer);

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
