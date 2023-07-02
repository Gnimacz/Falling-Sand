using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snad : Cell
{
    public Snad(Color color, CellState state, Vector2Int position)
    {
        cellProperties.cellColor = color;
        cellProperties.cellState = state;
        cellProperties.cellPosition = position;
        cellProperties.canCellMove = true;
        cellProperties.cellNeighbors = new List<Cell>();
    }

    public override void GetCellNeighbors(Cell[,] grid)
    {
        cellProperties.cellNeighbors.Clear();
        int x = cellProperties.cellPosition.x;
        int y = cellProperties.cellPosition.y;
        int width = grid.GetLength(0);
        int height = grid.GetLength(1);
        int randomIndex = UnityEngine.Random.Range(0, cellProperties.cellNeighbors.Count);

        // if (y > 0) //bottom
        // {
        //     cellProperties.cellNeighbors.Add(grid[x, y - 1]);
        // }
        // if (x > 0 && y > 0) //bottom-left
        // {
        //     cellProperties.cellNeighbors.Add(grid[x - 1, y - 1]);
        // }
        // if (x < width - 1 && y > 0) //bottom-right
        // {
        //     cellProperties.cellNeighbors.Add(grid[x + 1, y - 1]);
        // }

        //top
        if(y < height - 1)
        {
           cellProperties.cellNeighbors.Add(grid[x, y + 1]);
        }
        //top-left
        if (x > 0 && y < height - 1)
        {
           cellProperties.cellNeighbors.Add(grid[x - 1, y + 1]);
        }
        //top-right
        if (x < width - 1 && y < height - 1)
        {
           cellProperties.cellNeighbors.Add(grid[x + 1, y + 1]);
        }
        // //left
        // if (x > 0)
        // {
        //    cellProperties.cellNeighbors.Add(grid[x - 1, y]);
        // }
        // //right
        // if (x < width - 1)
        // {
        //    cellProperties.cellNeighbors.Add(grid[x + 1, y]);
        // }


    }
}
