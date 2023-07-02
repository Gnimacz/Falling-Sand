using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum CellState
{
    Empty,
    Solid,
    Liquid,
    Gas
}
public struct CellProperties
{
    public Color cellColor;
    public CellState cellState;
    public bool canCellMove;
    public Vector2Int cellPosition;
    public List<Cell> cellNeighbors;
    public bool updated;
    public bool shouldUpdate;
    public int cellID;
}
//a class to hold the minimum amount of data needed to define a cell
[System.Serializable]
public class Cell
{
    public CellProperties cellProperties;

    public Cell()
    {
        cellProperties.cellColor = Color.magenta;
        cellProperties.cellState = CellState.Solid;
        cellProperties.canCellMove = false;
        cellProperties.cellPosition = Vector2Int.zero;
        cellProperties.cellNeighbors = new List<Cell>();
        cellProperties.shouldUpdate = true;
    }
    public Cell(Color color, CellState state, Vector2Int position, bool canMove, ref Cell[,] grid)
    {
        cellProperties.cellColor = color;
        cellProperties.cellState = state;
        cellProperties.cellPosition = position;
        cellProperties.canCellMove = canMove;
        cellProperties.cellNeighbors = new List<Cell>();
        cellProperties.updated = false;
        cellProperties.shouldUpdate = true;
    }

    public virtual void UpdateCellState()
    {
    }

    public virtual Cell[,] UpdateCell(Cell[,] grid)
    {
        if (cellProperties.cellState == CellState.Solid)
        {
            return UpdateSolidCell(grid);
        }
        if (cellProperties.cellState == CellState.Liquid)
        {
            return UpdateLiquidCell(grid);
        }
        else if (cellProperties.cellState == CellState.Gas)
        {
            return UpdateGasCell(grid);
        }
        else
        {
            return grid;
        }
    }

    public virtual Cell[,] UpdateLiquidCell(Cell[,] grid)
    {
        // Debug.LogError("UpdateLiquidCell not implemented");

        // if (!cellProperties.shouldUpdate) return grid;
        // if (cellProperties.updated) return grid;
        // if (!cellProperties.canCellMove) return grid;
        GetCellNeighbors(grid);
        //if the cell below is empty, move down
        for (int i = 0; i < cellProperties.cellNeighbors.Count; i++)
        {
            // if (cellProperties.cellNeighbors[i].cellProperties.cellPosition.y > cellProperties.cellPosition.y) continue;
            if (cellProperties.cellNeighbors[i].cellProperties.cellState == CellState.Empty)
            {
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

                //GetCellNeighbors(grid);
                //if (cellProperties.cellNeighbors.Count > 0)
                //{
                //    int solidNeighborCells = 0;
                //    foreach (Cell updatedNeighbor in cellProperties.cellNeighbors)
                //    {
                //        if (updatedNeighbor.cellProperties.cellState == CellState.Solid)
                //        {
                //            solidNeighborCells++;
                //        }
                //    }
                //    if (solidNeighborCells == cellProperties.cellNeighbors.Count)
                //    {
                //        cellProperties.shouldUpdate = false;
                //    }
                //    else cellProperties.shouldUpdate = true;
                //}
                break;
            }
        }
        cellProperties.updated = true;
        return grid;
    }

    public virtual Cell[,] UpdateGasCell(Cell[,] grid)
    {
        Debug.LogError("UpdateGasCell not implemented");
        return grid;
    }

    public virtual Cell[,] UpdateSolidCell(Cell[,] grid)
    {
        // if (!cellProperties.shouldUpdate) return grid;
        if (cellProperties.updated) return grid;
        // if (!cellProperties.canCellMove) return grid;
        GetCellNeighbors(grid);
        //if the cell below is empty, move down
        for (int i = 0; i < cellProperties.cellNeighbors.Count; i++)
        {
            // if (cellProperties.cellNeighbors[i].cellProperties.cellPosition.y > cellProperties.cellPosition.y) continue;
            if (cellProperties.cellNeighbors[i].cellProperties.cellState != CellState.Solid)
            {
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

                //GetCellNeighbors(grid);
                //if (cellProperties.cellNeighbors.Count > 0)
                //{
                //    int solidNeighborCells = 0;
                //    foreach (Cell updatedNeighbor in cellProperties.cellNeighbors)
                //    {
                //        if (updatedNeighbor.cellProperties.cellState == CellState.Solid)
                //        {
                //            solidNeighborCells++;
                //        }
                //    }
                //    if (solidNeighborCells == cellProperties.cellNeighbors.Count)
                //    {
                //        cellProperties.shouldUpdate = false;
                //    }
                //    else cellProperties.shouldUpdate = true;
                //}
                break;
            }
        }
        cellProperties.updated = true;
        return grid;
    }

    public virtual void GetCellNeighbors(Cell[,] grid)
    {
        cellProperties.cellNeighbors.Clear();
        int x = cellProperties.cellPosition.x;
        int y = cellProperties.cellPosition.y;
        int width = grid.GetLength(0);
        int height = grid.GetLength(1);
        int randomIndex = UnityEngine.Random.Range(0, cellProperties.cellNeighbors.Count);

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
    }
}
