using Assets.Scripts.Level;
using UnityEngine;

namespace Assets.Scripts.Players.Class {
    public class RogueClass : IClass {
        void IClass.OnUse()
        {
            GridManager.Instance.cells.FindAll(x => 
                    x.currentState != GridManager.Instance.currentMove && x.currentState != CellState.NONE).ForEach(x => x.setAvailable(true)); 
            GridManager.Instance.currentMove = CellState.NONE;
        }
    }
}