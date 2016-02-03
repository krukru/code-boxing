using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Assets.SourceCode.Boxers;
using Assets.SourceCode.Api;
using Assets.SourceCode.Events;
using Assets.SourceCode.Strategies;
using Assets.SourceCode.Threading;
using UnityEngine;

namespace Assets.SourceCode.Controllers {
#pragma warning disable 0649
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

        public Thread redBoxerThread; //revert to private when going production
        public Thread blueBoxerThread;

        public bool debugMode = true;

        private void Start() {
            this.redBoxer = new Boxer(redStrategy, Boxer.Color.RED);
            this.blueBoxer = new Boxer(blueStrategy, Boxer.Color.BLUE);
            SubscribeToBoxerEvents(redBoxer);
            SubscribeToBoxerEvents(blueBoxer);
            redBoxer.opponent = blueBoxer; //remove when going into production
            blueBoxer.opponent = redBoxer;
            if (debugMode == false) {
                ResolveFight(redBoxer, blueBoxer);
            }
        }

        private void SubscribeToBoxerEvents(Boxer boxer) {
            boxer.FightEnded += boxer_FightEnded;
            boxer.AttackStarted += boxer_AttackStarted;
            boxer.AttackReceived += boxer_AttackReceived;
            boxer.StanceChanged += boxer_StanceChanged;
            boxer.StaminaRecovered += boxer_StaminaRecovered;
        }

        void boxer_StaminaRecovered(Boxer sender, EventArgs eventArgs) {
            BoxerVisualsController boxerController = GetController(sender);
            boxerController.UpdateStaminaSlider(sender);
        }


        void boxer_AttackStarted(Boxer attacker, BoxerAttackEventArgs eventArgs) {
            BoxerVisualsController boxerController = GetController(attacker);
            boxerController.StartAttack(attacker, eventArgs.Attack);
        }

        void boxer_AttackReceived(Boxer receiver, BoxerAttackEventArgs eventArgs) {
            BoxerVisualsController boxerController = GetController(receiver);
            boxerController.AttackReceived(receiver);
            if (receiver.IsCastingAttack) {
                blueBoxerThread.Interrupt();
            }
        }

        void boxer_StanceChanged(Boxer sender, EventArgs eventArgs) {
            BoxerVisualsController boxerController = GetController(sender);
            boxerController.SetStance(sender.BoxerStance);
        }

        private void boxer_FightEnded(Boxer sender, EventArgs eventArgs) {
            Debug.Log("Fight ended");
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
        private void ResolveFight(Boxer redBoxer, Boxer blueBoxer) {
            BoxerWorker redBoxerWorker = new BoxerWorker(redBoxer, blueBoxer);
            BoxerWorker blueBoxerWorker = new BoxerWorker(blueBoxer, redBoxer);

            redBoxerThread = new Thread(redBoxerWorker.DoWork);
            blueBoxerThread = new Thread(blueBoxerWorker.DoWork);

            //start the fight in 5 seconds
            DateTime startTime = DateTime.Now.AddSeconds(5);
            redBoxerThread.Start(startTime);
            blueBoxerThread.Start(startTime);
            Debug.Log("Threads started");
        }
    }
}
