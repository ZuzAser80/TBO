using System;
using Assets.Scripts.Players;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Items {
    
    public abstract class AbstractItem : ScriptableObject 
    {
        public virtual Action OnUse { get; set; }
        public string Description;
        public Sprite Sprite;
    }
}

