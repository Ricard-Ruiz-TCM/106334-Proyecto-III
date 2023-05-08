using UnityEngine;

namespace Array2DEditor
{
    [System.Serializable]
    public class Array2DNodeEnum : Array2D<gridNodeDiff>
    {
        [SerializeField]
        CellNodeEnum[] cells = new CellNodeEnum[Consts.defaultGridSize];

        protected override CellRow<gridNodeDiff> GetCellRow(int idx)
        {
            return cells[idx];
        }
    }
}
