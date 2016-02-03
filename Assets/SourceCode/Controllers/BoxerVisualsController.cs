using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.SourceCode.Boxers;
using Assets.SourceCode.Boxers.Attacks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.SourceCode.Controllers {
#pragma warning disable 0649
    class BoxerVisualsController : MonoBehaviour {
        public Animator boxerAnimator;
        public Slider hpSlider;
        public Slider staminaSlider;

        private const string STANCE_NORMAL = "StanceNormal";
        private const string STANCE_BLOCK = "StanceBlock";
        private const string STANCE_DODGE = "StanceDodge";
        private const string STANCE_STAGGER = "StanceStagger";

        private const string RECEIVED_ATTACK = "ReceivedAttack";

        private const string ATTACK_ANIMATION_SPEED = "SpeedMultiplier";

        public void StartAttack(Boxer attacker, AbstractAttack attack) {
            UpdateStaminaSlider(attacker);
            string className = attack.GetType().Name;
            string attackTrigger = String.Format("Attack{0}", className);
            float speedMultiplier = 1000f / attack.CastTimeInMs;
            boxerAnimator.SetFloat(ATTACK_ANIMATION_SPEED, speedMultiplier);
            boxerAnimator.SetTrigger(attackTrigger);
        }
        public void AttackReceived(Boxer receiver) {
            boxerAnimator.SetTrigger(RECEIVED_ATTACK);
            UpdateHpSlider(receiver);
        }

        public void UpdateHpSlider(Boxer boxer) {
            hpSlider.value = boxer.HitPoints;
        }

        public void UpdateStaminaSlider(Boxer boxer) {
            staminaSlider.value = boxer.Stamina;
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
    }
}
