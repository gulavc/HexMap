using UnityEngine;
using UnityEngine.EventSystems;

public class HexGameUI : MonoBehaviour {

    public HexGrid grid;

    HexCell currentCell;

    HexUnit selectedUnit;

    void Update() {
        if (!EventSystem.current.IsPointerOverGameObject()) {
            if (Input.GetMouseButtonDown(0)) {
                DoSelection();
                ShowPossibleMovement();
            }
            else if (selectedUnit) {
                if (Input.GetMouseButtonDown(1)) {
                    DoMove();
                    ShowPossibleMovement();
                }
                else {
                    DoPathfinding();
                }
            }
        }
        /*if (Input.GetKeyDown(KeyCode.R)) {
            if (selectedUnit) {
                selectedUnit.ResetMovement();
                ShowPossibleMovement();
            }
        }*/
    }

    public void SetEditMode(bool toggle) {
        enabled = !toggle;
        grid.ShowUI(!toggle);
        grid.ClearPath();
    }

    bool UpdateCurrentCell() {
        HexCell cell =
            grid.GetCell(Camera.main.ScreenPointToRay(Input.mousePosition));
        if (cell != currentCell) {
            currentCell = cell;
            return true;
        }
        return false;
    }

    void DoSelection() {
        grid.ClearPath();
        grid.ClearShowMovement();
        UpdateCurrentCell();
        if (currentCell) {
            selectedUnit = currentCell.Unit;
        }
    }

    void DoPathfinding() {
        if (UpdateCurrentCell()) {
            if (currentCell && selectedUnit.IsValidDestination(currentCell)) {
                grid.FindPath(selectedUnit.Location, currentCell, selectedUnit.Speed);
            }
            else {
                grid.ClearPath();
            }
        }
    }

    void DoMove() {
        if (grid.HasPath && grid.IsReachable(currentCell)) {
            selectedUnit.Travel(grid.GetPath());
            //selectedUnit.UseMovement(currentCell.Distance);
            grid.ClearPath();
        }
    }

    void ShowPossibleMovement() {
        if(currentCell && currentCell.Unit) {
            grid.ClearShowMovement();
            grid.ShowPossibleMovement(selectedUnit.Location, currentCell, selectedUnit.Speed);
        }
    }
}