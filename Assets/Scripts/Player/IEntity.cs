using System;
using Assets.Scripts.Players;
using UnityEngine.Events;

namespace Assets.Scripts.Players {
    public interface IEntity {
        public Stats CurrentStats {get; set;}
        public void ApplyStats(Stats stats);
    }
}