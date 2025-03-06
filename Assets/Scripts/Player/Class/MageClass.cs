using Assets.Scripts.Level;
using UnityEngine;

namespace Assets.Scripts.Players.Class {
    public class MageClass : IClass {
        void IClass.OnUse()
        {
            GridManager.Instance.cells.FindAll(x => x.currentState == CellState.NONE)
            .ForEach(x => x.onDoneClick.AddListener( delegate { GridManager.Instance.Explode(x); GridManager.Instance.ClearAll(); GridManager.Instance.FinishMove(); }));
            GridManager.Instance.FinishMove();
        }
    }
}