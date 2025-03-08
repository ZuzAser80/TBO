using System;
using Assets.Scripts.Players;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Items {
    
    public abstract class AbstractItem : ScriptableObject 
    {
        public virtual Action<IEntity> OnUse { get; set; }
        [TextArea(4, 10)]
        public string Description;
        public string Name;
        public Sprite Sprite;
    }
}

