using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Items {
    public class Inventory : MonoBehaviour {
        public List<ItemSlot> items = new List<ItemSlot>();
        public void AddItem(AbstractItem item) {
            if (items.Any(x => x.item == item)) { items.Find(x => x.item == item).Increment(); return; }
            items.First(x => x.item == null || x.Count <= 0).item = item;
            
        }

        public void UpdateAll() => items.ForEach(x => x.UpdateIcon());
    }
}