using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.SourceCode.Boxers;
using Assets.SourceCode.Boxers.Attacks;
using UnityEngine;

namespace Assets.SourceCode.Controllers {
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
            else if (Input.GetKeyDown(KeyCode.A)) {
                System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(matchController.redBoxer.Attack));
                t.Start();
                //matchController.redBoxer.Attack(Attacks.Jab);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5)) {
                matchController.redBoxer.AttackLanded(Attacks.Jab);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha6)) {
                matchController.redBoxer.Attack(Attacks.Jab);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha7)) {
                matchController.redBoxer.Attack(Attacks.Jab);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha8)) {
                matchController.redBoxer.Attack(Attacks.Jab);
            }
        }
    }
}
