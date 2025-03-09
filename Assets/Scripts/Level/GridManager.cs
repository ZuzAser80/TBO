using System;
using System.Collections.Generic;
using System.IO.Compression;
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
        private List<GridCell> _cells = new List<GridCell>();
        private LevelClass levelClass;
        private bool hasMoved = false;

        private void Awake() { 
            Instance = this;
            levelClass = GetComponent<LevelClass>();
            cells = GetComponentsInChildren<GridCell>().ToList(); 
            cells.ForEach(x => x.StatsToTake.GenerateRandom(1, 30));
            addDefListeners();
            currentMove = CellState.TIC;
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
                cells.ForEach(x => x.UpdateColor());
                Debug.Log(state + " won!");
                if (state == CellState.TIC) { levelClass.Win(); }
                else { levelClass.Lose(); }
            }
        }

        public void SetClass(int _class) => GetComponent<Player>().playerClass = (Classes)_class;

        private void changeMove() {
            currentMove = currentMove == CellState.TIC ? CellState.TAC : CellState.TIC; 
            aiTurn(); 
        }
        private List<GridCell> getNeighbors(GridCell cell, CellState state) { 
            if (cell == null) return new List<GridCell>() {};
            return cells.FindAll(x => (x.GridPos - cell.GridPos).magnitude <= Math.Sqrt(2) && x.currentState == state); 
        }

        private void aiTurn() {
            if(hasMoved) return;
            //FindAll на FindAll'е и FindAll-ом погоняет
            cells.FindAll(x => x.currentState == CellState.TIC && !x.isProtected).ForEach(x => 
            cells.FindAll(y => (y.GridPos == x.GridPos + x.GridPos - getNeighbors(x, CellState.TIC)[0].GridPos 
            || y.GridPos == x.GridPos - getNeighbors(x, CellState.TIC)[0].GridPos) && y.currentState == CellState.NONE).ForEach(y => _cells.Add(y)));
            if (currentMove != CellState.TAC || _cells.Count() == 0 || hasMoved) { return; }
            _cells = _cells.Where(x => x.currentState == CellState.NONE).ToList();
            if (_cells.Count == 0) { _cells.FindLast(x => x.currentState == CellState.NONE).currentState = CellState.TAC; FinishMove(); }
            var r = _cells[UnityEngine.Random.Range(0, _cells.Count())];            
            r.currentState = CellState.TAC;
            FinishMove();
        }

        public void setCanChangeMove(bool nv) => canChangeMove = nv;

        public void ClearAll() { 
            cells.FindAll(x => x.currentState == CellState.NONE).ForEach(x => x.onDoneClick.RemoveAllListeners());
            addDefListeners();
        }

        public void ResetAll(bool resetScore) {
            levelClass.CurrentScore = resetScore ? 0 : levelClass.CurrentScore;
            cells.ForEach(x => { x.isAvailable = true; x.isProtected = false; x.currentState = CellState.NONE; x.UpdateColor(); });
            currentMove = CellState.TIC;
            Time.timeScale = 1;
            GetComponent<Player>().hasUsed = false;
            cells.ForEach(x => x.StatsToTake.GenerateRandom(1, 30 + levelClass.CurrentScore * 5));
            ClearAll();
        }

        public void Explode(GridCell cell) {
            cells.FindAll(x => x.currentState == CellState.NONE).FindAll(y => (y.GridPos - cell.GridPos).magnitude <= 1).ForEach(z => z.TrySetState(currentMove));
            cell.currentState = CellState.NONE;
            cells.ForEach(x => x.UpdateColor());
        }

        public void FinishMove() { 
            if (Time.timeScale == 0) { return; }
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
