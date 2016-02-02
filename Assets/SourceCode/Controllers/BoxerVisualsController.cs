using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.SourceCode.Boxers;
using Assets.SourceCode.Boxers.Attacks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.SourceCode.Controllers {
    class BoxerVisualsController : MonoBehaviour {
        public Animator boxerAnimator;
        public Slider hpSlider;
        public Slider staminaSlider;

        private const string STANCE_NORMAL = "StanceNormal";
        private const string STANCE_BLOCK = "StanceBlock";
        private const string STANCE_DODGE = "StanceDodge";
        private const string STANCE_STAGGER = "StanceStagger";

        private const string RECEIVED_ATTACK = "ReceivedAttack";

        public void Attack(AbstractAttack attack) {
            string className = attack.GetType().Name;
            string attackTrigger = String.Format("Attack{0}", className);
            boxerAnimator.SetTrigger(attackTrigger);
        }

        public void SetStance(Boxer.Stance stance) {
            string stanceTrigger;
            switch (stance) {
                case Boxer.Stance.NORMAL:
                    stanceTrigger = STANCE_NORMAL;
                    break;
                case Boxer.Stance.BLOCKING:
                    stanceTrigger = STANCE_BLOCK;
                    break;
                case Boxer.Stance.DODGING:
                    stanceTrigger = STANCE_DODGE;
                    break;
                case Boxer.Stance.STAGGERING:
                    stanceTrigger = STANCE_STAGGER;
                    break;
                default:
                    throw new UnityException("Undefined stance");
            }
            boxerAnimator.SetTrigger(stanceTrigger);
        }

        public void AttackReceived() {
            boxerAnimator.SetTrigger(RECEIVED_ATTACK);
        }
    }
}
