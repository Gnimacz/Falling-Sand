using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingCell : Cell
{
    public FallingCell(Color color, CellState state, Vector2Int position, ref Cell[,] grid)
    {
        cellProperties.cellColor = color;
        cellProperties.cellState = state;
        cellProperties.cellPosition = position;
        cellProperties.canCellMove = true;
        cellProperties.cellNeighbors = new List<Cell>();
    }

    public override Cell[,] UpdateSolidCell(Cell[,] grid)
    {
        Debug.LogWarning(cellProperties.cellPosition);
        return base.UpdateSolidCell(grid);
    }
}
