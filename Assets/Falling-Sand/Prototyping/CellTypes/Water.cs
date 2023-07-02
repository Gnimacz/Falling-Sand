using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : Cell
{
    int dir = 0;
    public Water(Color color, CellState state, Vector2Int position)
    {
        cellProperties.cellColor = color;
        cellProperties.cellState = state;
        cellProperties.cellPosition = position;
        cellProperties.canCellMove = true;
        cellProperties.cellNeighbors = new List<Cell>();
        dir = UnityEngine.Random.Range(0, 2);
        // cellProperties.cellColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
    }

    public override Cell[,] UpdateCell(Cell[,] grid)
    {
        // grid = base.UpdateCell(grid);
        // if (cellProperties.cellPosition.x >= grid.GetLength(0) - 1)
        // {
        //     dir = 1;
        // }
        // if (cellProperties.cellPosition.x <= 0)
        // {
        //     dir = 0;
        // }
        // GetCellNeighbors(grid);
        // foreach (Cell neighboringCell in cellProperties.cellNeighbors)
        // {
        //     if (neighboringCell.cellProperties.cellState != CellState.Empty
        //     && neighboringCell.cellProperties.cellPosition.y == cellProperties.cellPosition.y)
        //     {
        //         cellProperties.updated = false;
        //         if (dir == 0)
        //         {
        //             dir = 1;
        //         }
        //         else
        //         {
        //             dir = 0;
        //         }
        //     }
        // }
        // return grid;

        GetCellNeighbors(grid);
        if (cellProperties.cellPosition.x >= grid.GetLength(0) - 1)
        {
            dir = 0;
        }
        if (cellProperties.cellPosition.x <= 0)
        {
            dir = 1;
        }
        foreach (Cell neighboringCell in cellProperties.cellNeighbors)
        {
            if ((neighboringCell.cellProperties.cellState != CellState.Empty || neighboringCell.cellProperties.cellState != CellState.Liquid)
            && neighboringCell.cellProperties.cellPosition.y == cellProperties.cellPosition.y)
            {
                if (dir == 0)
                {
                    dir = 1;
                }
                else
                {
                    dir = 0;
                }
            }
        }
        for (int i = 0; i < cellProperties.cellNeighbors.Count; i++)
        {
            // if (cellProperties.cellNeighbors[i].cellProperties.cellPosition.y > cellProperties.cellPosition.y) continue;
            if (cellProperties.cellNeighbors[i].cellProperties.cellState == CellState.Empty)
            {
                if (dir == 0 &&
                cellProperties.cellNeighbors[i].cellProperties.cellPosition.x > cellProperties.cellPosition.x
                && cellProperties.cellNeighbors[i].cellProperties.cellPosition.y == cellProperties.cellPosition.y)
                {
                    continue;
                }
                else if (dir == 1 &&
                cellProperties.cellNeighbors[i].cellProperties.cellPosition.x < cellProperties.cellPosition.x
                && cellProperties.cellNeighbors[i].cellProperties.cellPosition.y == cellProperties.cellPosition.y)
                {
                    continue;
                }

                Cell oldCell = this;
                Cell oldNeighborCell = cellProperties.cellNeighbors[i];

                Vector2Int oldCellPos = cellProperties.cellPosition;
                Vector2Int oldNeighborCellPos = cellProperties.cellNeighbors[i].cellProperties.cellPosition;

                //check each of the neighboring cells, if it's empty, move there
                grid[cellProperties.cellNeighbors[i].cellProperties.cellPosition.x, cellProperties.cellNeighbors[i].cellProperties.cellPosition.y] = this;
                cellProperties.cellPosition = cellProperties.cellNeighbors[i].cellProperties.cellPosition;
                // cellProperties.cellColor = Color.yellow;

                oldNeighborCell.cellProperties.cellPosition = oldCellPos;
                grid[oldCellPos.x, oldCellPos.y] = oldNeighborCell;
                break;
            }
        }
        cellProperties.updated = true;
        return grid;
    }

    public override void GetCellNeighbors(Cell[,] grid)
    {
        cellProperties.cellNeighbors.Clear();
        int x = cellProperties.cellPosition.x;
        int y = cellProperties.cellPosition.y;
        int width = grid.GetLength(0);
        int height = grid.GetLength(1);

        if (y > 0) //bottom
        {
            cellProperties.cellNeighbors.Add(grid[x, y - 1]);
        }
        if (x > 0 && y > 0) //bottom-left
        {
            cellProperties.cellNeighbors.Add(grid[x - 1, y - 1]);
        }
        if (x < width - 1 && y > 0) //bottom-right
        {
            cellProperties.cellNeighbors.Add(grid[x + 1, y - 1]);
        }

        // //top
        // if(y < height - 1)
        // {
        //    cellProperties.cellNeighbors.Add(grid[x, y + 1]);
        // }
        // //top-left
        // if (x > 0 && y < height - 1)
        // {
        //    cellProperties.cellNeighbors.Add(grid[x - 1, y + 1]);
        // }
        // //top-right
        // if (x < width - 1 && y < height - 1)
        // {
        //    cellProperties.cellNeighbors.Add(grid[x + 1, y + 1]);
        // }
        // left
        if (cellProperties.cellPosition.x > 0 /* && cellProperties.cellPosition.x < grid.GetLength(0) - 1 */)
        {
            if (/* dir == 0 &&  */grid[cellProperties.cellPosition.x - 1, cellProperties.cellPosition.y].cellProperties.cellState == CellState.Empty)
            {
                cellProperties.cellNeighbors.Add(grid[cellProperties.cellPosition.x - 1, cellProperties.cellPosition.y]);
            }
        }
        //right
        if (/* cellProperties.cellPosition.x > 0 &&  */cellProperties.cellPosition.x < grid.GetLength(0) - 1)
        {
            if (/* dir == 1 &&  */grid[cellProperties.cellPosition.x + 1, cellProperties.cellPosition.y].cellProperties.cellState == CellState.Empty)
            {
                cellProperties.cellNeighbors.Add(grid[cellProperties.cellPosition.x + 1, cellProperties.cellPosition.y]);
            }
        }


    }
}
