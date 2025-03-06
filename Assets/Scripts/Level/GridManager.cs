using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Players;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Level {
    public class GridManager : MonoBehaviour
    {
        
        public static GridManager Instance { get; private set; }
        public List<GridCell> cells = new List<GridCell>();
        public List<GridCell> canBeBlocked = new List<GridCell>();
        public CellState currentMove = CellState.TIC;
        public UnityEvent onMoveFinish;

        private List<GridCell> temp = new List<GridCell>();
        private bool canChangeMove = true;
        private GridCell _cell;

        private void Awake() { 
            Instance = this;
            cells = GetComponentsInChildren<GridCell>().ToList(); 
            cells.ForEach(x => x.StatsToTake.GenerateRandom(1, 30));
            addDefListeners();
        }

        private void addDefListeners() => cells.ForEach(x => x.onDoneClick.AddListener(FinishMove));

        private void updateCell(CellState state) {
            temp = cells.FindAll(x => x.currentState == state);
            var win = temp.Any(x => 
                (temp.Any(y => x.GridPos - Vector3.down == y.GridPos) && temp.Any(y => x.GridPos - Vector3.up == y.GridPos)) || 
                (temp.Any(y => x.GridPos - Vector3.right == y.GridPos) && temp.Any(y => x.GridPos - Vector3.left == y.GridPos)) || 
                (temp.Any(y => x.GridPos - Vector3.down - Vector3.right == y.GridPos) && temp.Any(y => x.GridPos - Vector3.up - Vector3.left == y.GridPos)) ||
                (temp.Any(y => x.GridPos - Vector3.down - Vector3.left == y.GridPos) && temp.Any(y => x.GridPos - Vector3.up - Vector3.right == y.GridPos))
            );
            if (win) {
                Debug.Log(state + " won!");
            }
        }

        private void changeMove() { currentMove = currentMove == CellState.TIC ? CellState.TAC : CellState.TIC; aiTurn(); }
        private List<GridCell> getNeighbors(GridCell cell) { 
            if (cell == null) return new List<GridCell>() {};
            return cells.FindAll(x => (x.GridPos - cell.GridPos).magnitude <= Math.Sqrt(2) && x.currentState == CellState.NONE); 
        }

        private void aiTurn() {
            _cell = cells.FindAll(x => x.currentState == CellState.TIC).ElementAtOrDefault(
                    UnityEngine.Random.Range(0, cells.Where(x => x.currentState == CellState.TIC).Count() - 1));
            if (currentMove != CellState.TAC || _cell == null || getNeighbors(_cell).Count() == 0) return;
            getNeighbors(_cell)[UnityEngine.Random.Range(0, getNeighbors(_cell).Count())].currentState = CellState.TAC;
            FinishMove();
        }

        public void setCanChangeMove(bool nv) => canChangeMove = nv;

        public void ClearAll() { 
            cells.FindAll(x => x.currentState == CellState.NONE).ForEach(x => x.onDoneClick.RemoveAllListeners());
            addDefListeners();
        }

        public void Explode(GridCell cell) {
            cells.FindAll(x => x.currentState == CellState.NONE).FindAll(y => (y.GridPos - cell.GridPos).magnitude <= 1).ForEach(z => z.TrySetState(currentMove));
            cell.currentState = CellState.NONE;
            cells.ForEach(x => x.UpdateColor());
        }

        public void FinishMove() { 
            updateCell(CellState.TIC);
            updateCell(CellState.TAC);
            cells.ForEach(x => x.UpdateColor());
            cells.ForEach(x => x.setAvailable(false));
            cells.FindAll(x => x.currentState == CellState.NONE).ForEach(x => x.setAvailable(true));
            if(canChangeMove) {
                changeMove();
            } else {
                GetComponent<Player>().canUse = false;
            }
            onMoveFinish.Invoke();
        }
    }
}
