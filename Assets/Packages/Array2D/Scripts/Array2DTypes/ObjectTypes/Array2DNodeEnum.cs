using UnityEngine;

namespace Array2DEditor {
    [System.Serializable]
    public class Array2DNodeEnum : Array2D<nodeType> {
        [SerializeField]
        CellNodeEnum[] cells = new CellNodeEnum[Consts.defaultGridSize];

        protected override CellRow<nodeType> GetCellRow(int idx) {
            return cells[idx];
        }
    }
}
