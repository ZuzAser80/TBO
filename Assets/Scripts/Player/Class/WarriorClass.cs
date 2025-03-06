using Assets.Scripts.Level;
using UnityEngine;

namespace Assets.Scripts.Players.Class {
    public class WarriorClass : IClass {
        void IClass.OnUse()
        {
            GridManager.Instance.setCanChangeMove(false);
            GridManager.Instance.onMoveFinish.AddListener(delegate { GridManager.Instance.setCanChangeMove(true); });
        }
    }
}