using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class FallingSandPrototype : MonoBehaviour
{
    public int paintBrushRadius = 10;
    public Texture2D texture;
    public Vector2Int texSize = new Vector2Int(256, 256);
    public Vector2Int realTexSize = new Vector2Int(256, 256);

    public Cell[,] cellGrid;
    private Cell lastUpdatedCell;

    private Image image;
    private RectTransform rectTransform;

    private void Awake()
    {
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        cellGrid = new Cell[texSize.x, texSize.y];

        //populate the array with cells
        for (int i = 0; i < texSize.x; i++)
        {
            for (int j = 0; j < texSize.y; j++)
            {
                cellGrid[i, j] = new EmptyCell(Color.white, CellState.Empty, new Vector2Int(i, j));
            }
        }

        texture = new Texture2D(texSize.x, texSize.y);
        texture.filterMode = FilterMode.Point;
        for (int i = 0; i < texture.width; i++)
        {
            for (int j = 0; j < texture.height; j++)
            {
                texture.SetPixel(cellGrid[i, j].cellProperties.cellPosition.x, cellGrid[i, j].cellProperties.cellPosition.y, cellGrid[i, j].cellProperties.cellColor);
            }
        }
        texture.Apply();
        image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        image.rectTransform.sizeDelta = new Vector2(realTexSize.x, realTexSize.y);
        image.sprite.texture.filterMode = FilterMode.Point;
    }

    int testint = 0;
    int testint2 = 0;
    private void Update()
    {
        //Update the cells in the array
        //if (Input.GetKey(KeyCode.Space))
        //{
        Cell[,] cells = new Cell[texSize.x, texSize.y];
        for (int i = 0; i < texSize.x; i++)
        {
            for (int j = 0; j < texSize.y; j++)
            {
                //cellGrid = cellGrid[i, j].UpdateCell(cellGrid);
                cellGrid[i, j].UpdateCell(cellGrid);
            }
        }

        //}

        // Sprite oldSprite = GetComponent<SpriteRenderer>().sprite;
        // Destroy(oldSprite);
        // GetComponent<SpriteRenderer>().sprite = Sprite.Create(texture, new Rect(0, 0, 256, 256), new Vector2(0.5f, 0.5f));
        image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        image.sprite.texture.filterMode = FilterMode.Point;
        rectTransform.sizeDelta = new Vector2(realTexSize.x, realTexSize.y);


        for (int i = 0; i < texSize.x; i++)
        {
            for (int j = 0; j < texSize.y; j++)
            {
                texture.SetPixel(i, j, cellGrid[i, j].cellProperties.cellColor);
            }
        }

        //give a preview of the pixel that will be painted
        if (RectTransformUtility.RectangleContainsScreenPoint(rectTransform, Input.mousePosition, Camera.main))
        {
            //convert mouse position to local position inside the image
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, Input.mousePosition, Camera.main, out localPoint);
            localPoint.x = Remap(localPoint.x, -rectTransform.rect.width / 2, rectTransform.rect.width / 2, 0, texSize.x);
            localPoint.y = Remap(localPoint.y, -rectTransform.rect.height / 2, rectTransform.rect.height / 2, 0, texSize.y);

            //paint the pixels in a radius
            for (int i = 0; i < texSize.x; i++)
            {
                for (int j = 0; j < texSize.y; j++)
                {
                    if (Vector2.Distance(new Vector2(i, j), localPoint) < paintBrushRadius)
                    {
                        texture.SetPixel(i, j, Color.green);
                    }
                }
            }
        }



        if (Input.GetMouseButton(0))
        {
            PaintPixelAtMouse(new FallingCell(Color.yellow, CellState.Solid, Vector2Int.zero, ref cellGrid));
            // ChangePixelAtMousePos();
        }
        if (Input.GetMouseButton(1))
        {
            PaintPixelAtMouse(new EmptyCell(Color.white, CellState.Empty, Vector2Int.zero));
        }


        //change the size of the paint brush
        if (Input.mouseScrollDelta.y > 0)
        {
            paintBrushRadius++;
        }
        else if (Input.mouseScrollDelta.y < 0)
        {
            paintBrushRadius--;
        }

        cellGrid = cellGrid;

        // texture.SetPixel(testint, testint2, Color.red);
        // cellGrid[testint, testint2].UpdateCell(cellGrid);
        // testint++;
        // if (testint >= texSize.x)
        // {
        //     testint = 0;
        //     testint2++;
        // }
        // if (testint2 >= texSize.y)
        // {
        //     testint2 = 0;
        // }
        texture.Apply();
        //reset the updated flag for all cells
        for (int i = 0; i < texSize.x; i++)
        {
            for (int j = 0; j < texSize.y; j++)
            {
                cellGrid[i, j].cellProperties.updated = false;
            }
        }
    }

    //raycast from the mouse position and change the pixel at the hit position to black
    void PaintPixelAtMouse(Cell cellToPaint)
    {
        Cell cellToPlace;

        if (RectTransformUtility.RectangleContainsScreenPoint(rectTransform, Input.mousePosition, Camera.main))
        {
            //convert mouse position to local position inside the image
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, Input.mousePosition, Camera.main, out localPoint);
            localPoint.x = Remap(localPoint.x, -rectTransform.rect.width / 2, rectTransform.rect.width / 2, 0, texSize.x);
            localPoint.y = Remap(localPoint.y, -rectTransform.rect.height / 2, rectTransform.rect.height / 2, 0, texSize.y);

            //paint the pixels in a radius
            if (paintBrushRadius <= 1)
            {
                // cellGrid[(int)localPoint.x, (int)localPoint.y].cellProperties.cellColor = colorToPaint;
                cellToPaint.cellProperties.cellPosition = new Vector2Int((int)localPoint.x, (int)localPoint.y);
                cellGrid[(int)localPoint.x, (int)localPoint.y] = cellToPaint;

            }
            else
            {
                for (int i = 0; i < texSize.x; i++)
                {
                    for (int j = 0; j < texSize.y; j++)
                    {
                        if (Vector2.Distance(new Vector2(i, j), localPoint) < paintBrushRadius)
                        {
                            // texture.SetPixel(i, j, colorToPaint);
                            switch (cellToPaint)
                            {
                                case FallingCell:
                                    cellToPlace = new FallingCell(cellToPaint.cellProperties.cellColor, cellToPaint.cellProperties.cellState, new Vector2Int(i, j), ref cellGrid);
                                    break;
                                case EmptyCell:
                                    cellToPlace = new EmptyCell(cellToPaint.cellProperties.cellColor, cellToPaint.cellProperties.cellState, new Vector2Int(i, j));
                                    break;
                                default:
                                    cellToPlace = new EmptyCell(cellToPaint.cellProperties.cellColor, CellState.Empty, new Vector2Int(i, j));
                                    break;
                            }
                            // cellToPlace = new FallingCell(cellToPaint.cellProperties.cellColor, cellToPaint.cellProperties.cellState, new Vector2Int(i,j), ref cellGrid);
                            // cellToPlace.cellProperties.cellPosition = new Vector2Int(i, j);
                            // cellToPlace.cellProperties.cellColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1);
                            cellGrid[i, j] = cellToPlace;
                        }
                    }
                }
            }
            //paint the pixel
            // texture.SetPixel((int)localPoint.x, (int)localPoint.y, colorToPaint);
            // texture.Apply();
            return;
        }
    }

    public static float Remap(float value, float originalMin, float originalMax, float newMin, float newMax)
    {
        // Clamp the value within the original range
        float clampedValue = Mathf.Clamp(value, originalMin, originalMax);

        // Map the clamped value to the new range
        float mappedValue = newMin + (clampedValue - originalMin) * (newMax - newMin) / (originalMax - originalMin);

        return mappedValue;
    }
}
