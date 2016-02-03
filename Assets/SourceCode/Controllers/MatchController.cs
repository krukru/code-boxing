using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Assets.SourceCode.Boxers;
using Assets.SourceCode.Events;
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

        public BoxerVisualsController redBoxerVisualsController;
        public BoxerVisualsController blueBoxerVisualsController;

        public Boxer redBoxer;
        public Boxer blueBoxer;

        private AbstractBoxingStrategy redStrategy = new TestStrategy();
        private AbstractBoxingStrategy blueStrategy = new DerpStrategy();

        private BoxerWorker redBoxerWorker;
        private BoxerWorker blueBoxerWorker;

        private void Start() {
            this.redBoxer = new Boxer(redStrategy, Boxer.Color.RED);
            this.blueBoxer = new Boxer(blueStrategy, Boxer.Color.BLUE);
            SubscribeToBoxerEvents(redBoxer);
            SubscribeToBoxerEvents(blueBoxer);
            redBoxer.opponent = blueBoxer;
            blueBoxer.opponent = redBoxer;
            //ResolveFight(redBoxer, blueBoxer);
        }

        private void SubscribeToBoxerEvents(Boxer boxer) {
            boxer.FightEnded += boxer_FightEnded;
            boxer.AttackStarted += boxer_AttackStarted;
            boxer.AttackReceived += boxer_AttackReceived;
            boxer.StanceChanged += boxer_StanceChanged;
        }


        void boxer_AttackStarted(Boxer sender, BoxerAttackEventArgs eventArgs) {
            BoxerVisualsController boxerController = GetController(sender);
            boxerController.StartAttack(sender, eventArgs.Attack);
        }

        void boxer_AttackReceived(Boxer sender, BoxerAttackEventArgs eventArgs) {
            BoxerVisualsController boxerController = GetController(sender);
            boxerController.AttackReceived(sender);
        }

        void boxer_StanceChanged(Boxer sender) {
            BoxerVisualsController boxerController = GetController(sender);
            boxerController.SetStance(sender.BoxerStance);
        }

        private BoxerVisualsController GetController(Boxer sender) {
            switch (sender.BoxerColor) {
                case Boxer.Color.RED:
                    return redBoxerVisualsController;
                case Boxer.Color.BLUE:
                    return blueBoxerVisualsController;
                default:
                    throw new UnityException("Invalid state");
            }
        }

        private void boxer_FightEnded(Boxer sender) {
            Debug.Log("Fight ended");
        }

        private void ResolveFight(Boxer redBoxer, Boxer blueBoxer) {
            redBoxerWorker = new BoxerWorker(redBoxer, blueBoxer);
            blueBoxerWorker = new BoxerWorker(blueBoxer, redBoxer);

            Thread redBoxerThread = new Thread(redBoxerWorker.DoWork);
            Thread blueBoxerThread = new Thread(blueBoxerWorker.DoWork);

            //start the fight in 5 seconds
            DateTime startTime = DateTime.Now.AddSeconds(5);
            redBoxerThread.Start(startTime);
            blueBoxerThread.Start(startTime);
            Debug.Log("Threads started");
        }
    }
}
