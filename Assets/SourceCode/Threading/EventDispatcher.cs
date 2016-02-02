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

        private Queue<BoxerEventHandler> events = new Queue<BoxerEventHandler>();
        private Boxer sender;
        private EventArgs args;

        public void AddEvent(BoxerEventHandler e) {

        }

        private void Update() {
            if (events.Count > 0) {
                BoxerEventHandler e = events.Dequeue();
                e.Invoke(sender, null);
            }
        }

        public void AddEvent(BoxerEventHandler eventHandler, Boxer sender, EventArgs eventArgs) {
            lock (events) {
                this.sender = sender;
                events.Enqueue((a, b) => eventHandler(sender, eventArgs));
            }
        }

        public void AddEvent<T>(BoxerEventHandler<T> eventHandler, Boxer sender, T eventArgs) where T : EventArgs {
            lock (events) {
                this.sender = sender;
                events.Enqueue((a, b) => eventHandler(sender, eventArgs));
            }
            //throw new NotImplementedException();
        }
    }
}
