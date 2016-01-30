using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.SourceCode.Fighters;

namespace Assets.SourceCode.Threading {
    class FighterWorker {
        public Fighter Player { get; private set; }
        public Fighter Opponent { get; private set; }

        public FighterWorker(Fighter player, Fighter opponent) {
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
