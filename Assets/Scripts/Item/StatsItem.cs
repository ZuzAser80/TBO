using System;
using Assets.Scripts.Items;
using Assets.Scripts.Players;
using UnityEngine;

namespace Assets.Scripts.Items {
    [CreateAssetMenu(fileName = "StatsItem", menuName = "Data/StatsItem", order = 0)]
    public class StatsItem : AbstractItem
    {
        public override Action OnUse { get => delegate { target.ApplyStats(stats); }; set => OnUse = value; }
        public Stats stats;
        public bool targetedAtEnemy;
        public IEntity target;
    }
}
