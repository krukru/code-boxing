using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Assets.SourceCode.Boxers;
using Assets.SourceCode.Boxers.Attacks;
using UnityEngine;

namespace Assets.SourceCode.Controllers {
#pragma warning disable 0649
    class DebugController : MonoBehaviour {
        public MatchController matchController;

        private void Update() {
            if (Input.GetKeyDown(KeyCode.Alpha1)) {
                matchController.redBoxer.ChangeStance(Boxer.Stance.NORMAL);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2)) {
                matchController.redBoxer.ChangeStance(Boxer.Stance.BLOCKING);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3)) {
                matchController.redBoxer.ChangeStance(Boxer.Stance.DODGING);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4)) {
                matchController.redBoxer.ChangeStance(Boxer.Stance.STAGGERING);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5)) {
                matchController.blueBoxer.ChangeStance(Boxer.Stance.NORMAL);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha6)) {
                matchController.blueBoxer.ChangeStance(Boxer.Stance.BLOCKING);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha7)) {
                matchController.blueBoxer.ChangeStance(Boxer.Stance.DODGING);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha8)) {
                matchController.blueBoxer.ChangeStance(Boxer.Stance.STAGGERING);
            }
            else if (Input.GetKeyDown(KeyCode.A)) {
                matchController.redBoxer.Api.Attack(Attacks.Jab);
            }
            else if (Input.GetKeyDown(KeyCode.S)) {
                matchController.redBoxer.Api.Attack(Attacks.Haymaker);
            }
            else if (Input.GetKeyDown(KeyCode.D)) {
                matchController.redBoxer.Api.Attack(Attacks.LiverShot);
            }
            else if (Input.GetKeyDown(KeyCode.F)) {
                matchController.redBoxer.Api.RecoverStamina();
            }
            else if (Input.GetKeyDown(KeyCode.Q)) {
                matchController.blueBoxer.Api.Attack(Attacks.Jab);
            }
            else if (Input.GetKeyDown(KeyCode.W)) {
                matchController.blueBoxer.Api.Attack(Attacks.Haymaker);
            }
            else if (Input.GetKeyDown(KeyCode.E)) {
                matchController.blueBoxer.Api.Attack(Attacks.LiverShot);
            }
            else if (Input.GetKeyDown(KeyCode.R)) {
                matchController.blueBoxer.Api.RecoverStamina();
            }
        }
    }
}
