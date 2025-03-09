using Assets.Scripts.Level;
using UnityEngine;

namespace Assets.Scripts.Players.Class {
    public class PaladinClass : IClass {
        void IClass.OnUse()
        {
            GridManager.Instance.cells.ForEach(x => x.setAvailable(false));
            GridManager.Instance.cells.FindAll(x => x.currentState == GridManager.Instance.currentMove).ForEach(x => {
                x.setAvailable(true);
                x.onDoneClick.AddListener( delegate { x.Protect(); x.UpdateColor(); GridManager.Instance.ClearAll(); });
            });
        }
    }
}