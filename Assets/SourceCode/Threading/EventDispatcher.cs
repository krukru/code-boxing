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

        private Queue<BoxerEvent> events = new Queue<BoxerEvent>();
        private Boxer sender;
        private EventArgs args;


        private void Update() {
            if (events.Count > 0) {
                BoxerEvent boxerEvent = events.Dequeue();
                boxerEvent.EventHandler.DynamicInvoke(boxerEvent.Sender, boxerEvent.EventArgs);
            }
        }

        public void AddEvent(BoxerEvent boxerEvent) {
            events.Enqueue(boxerEvent);
        }
    }
}
