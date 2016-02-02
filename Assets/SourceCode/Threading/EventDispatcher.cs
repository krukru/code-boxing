using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.SourceCode.Boxers;
using Assets.SourceCode.Events;
using UnityEngine;

namespace Assets.SourceCode.Threading {
    class EventDispatcher : MonoBehaviour {

        private static EventDispatcher instance;
        public static EventDispatcher Instance {
            get {
                return instance;
            }
        }

        private void Start() {
            instance = this.GetComponent<EventDispatcher>();
        }

        private Queue<BoxerSimpleEvent> events = new Queue<BoxerSimpleEvent>();
        private Queue<BoxerAttackEvent> events2 = new Queue<BoxerAttackEvent>();
        private Boxer sender;
        private EventArgs args;


        private void Update() {
            if (events.Count > 0) {
                BoxerSimpleEvent boxerEvent = events.Dequeue();
                boxerEvent.EventHandler(boxerEvent.Sender);
            }
            if (events2.Count > 0) {
                BoxerAttackEvent boxerEvent = events2.Dequeue();
                boxerEvent.EventHandler(boxerEvent.Sender,boxerEvent.EventArgs );
            }
        }

        public void AddEvent(BoxerSimpleEvent boxerEvent) {
            events.Enqueue(boxerEvent);
        }

        public void AddEvent(BoxerAttackEvent boxerEvent) {
            events2.Enqueue(boxerEvent);
        }
    }
}
