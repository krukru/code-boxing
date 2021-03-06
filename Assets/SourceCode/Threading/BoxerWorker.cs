﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.SourceCode.Boxers;

namespace Assets.SourceCode.Threading {
    public class BoxerWorker {
        public Boxer Player { get; private set; }
        public Boxer Opponent { get; private set; }

        public BoxerWorker(Boxer player, Boxer opponent) {
            this.Player = player;
            this.Opponent = opponent;
        }

        public void DoWork(object startTimeObject) {
            DateTime startTime = (DateTime)startTimeObject;
            while (DateTime.Now < startTime) {
                //WAAIT FOR IIIT
            }
            Player.StartFighting(Opponent);
        }
    }
}
