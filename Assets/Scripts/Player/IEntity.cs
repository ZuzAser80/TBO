using System;
using Assets.Scripts.Players;
using UnityEngine.Events;

namespace Assets.Scripts.Players {
    public interface IEntity {
        public int LevelExp {get; set;}
        public int CurrentExp {get; set;}
        public Stats CurrentStats {get; set;}
        public UnityEvent onLevelUp {get; set;}
        public void ApplyStats(Stats stats);
    }
}