using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyCell : Cell
{
    public EmptyCell( Color color, CellState state, Vector2Int position)
    {
        cellProperties.cellColor = color;
        cellProperties.cellState = state;
        cellProperties.cellPosition = position;
        cellProperties.cellNeighbors = new List<Cell>();
    }

    public override Cell[,] UpdateCell(Cell[,] grid)
    {
        //do nothing
        return grid;
    }
}
