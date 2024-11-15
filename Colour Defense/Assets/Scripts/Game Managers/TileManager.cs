using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TileManager : MonoBehaviour
{
    public GameObject pathPrefab;
    public GameObject cellPrefab;

    public GameObject background;

    public List<List<GameObject>> gridCells;

    private float halfBackgroundHeight;
    private float halfBackgroundWidth;
    private float hexheight;
    private float threeQuaterHexWidth;
    private int maxHeight;
    private int maxWidth;

    private Vector2[] axial_direction_vectors;
    // Start is called before the first frame update
    void Start()
    {
        axial_direction_vectors = new Vector2[6];
        axial_direction_vectors[0] = new Vector2(1,0);
        axial_direction_vectors[1] = new Vector2(1, 1);
        axial_direction_vectors[2] = new Vector2(0, 1);
        axial_direction_vectors[3] = new Vector2(-1, 0);
        axial_direction_vectors[4] = new Vector2(-1,-1);
        axial_direction_vectors[5] = new Vector2(0,-1);

        gridCells = new List<List<GameObject>>();


        halfBackgroundHeight = background.GetComponent<SpriteRenderer>().size.y/2;
        halfBackgroundWidth = background.GetComponent<SpriteRenderer>().size.x/2;
        //Debug.Log("BackgroundHeight: " +  halfBackgroundHeight);
        //Debug.Log("BackgroundWidth: " + halfBackgroundWidth);
        hexheight = cellPrefab.GetComponent<SpriteRenderer>().size.y;
        threeQuaterHexWidth = cellPrefab.GetComponent<SpriteRenderer>().size.x*3/4 + 0.01F;
        //Debug.Log("HexHeight: " + hexheight);
        //Debug.Log("HexWidth: " + threeQuaterHexWidth);
        CreateHexTileMap();
        CreatePath();
    }

    private void CreateHexTileMap()
    {
        int vertical = 0;
        int horizontal = 0;
        //Debug.Log("width " + (halfBackgroundHeight * 2 / hexheight) + " height " + (halfBackgroundWidth * 2 / threeQuaterHexWidth));
        for (float i = -halfBackgroundHeight; i < halfBackgroundHeight; i += hexheight, vertical++)
        {
            horizontal = 0;
            for (float j = -halfBackgroundWidth; j < halfBackgroundWidth; j += threeQuaterHexWidth, horizontal++)
            {
                // create a new object at (j,i) with parent being this gameobject
                gridCells.Add(new List<GameObject>());
                GameObject tempHex = Instantiate(cellPrefab, new Vector3(j, i, 0), Quaternion.identity, transform);
                if (horizontal % 2 != 0)
                {
                    tempHex.transform.position = new Vector3(j, i + hexheight / 2, 0); 
                }

                tempHex.GetComponent<HexCell>().AddIndex(Offset_to_axial( new Vector2(horizontal, vertical)));
                gridCells[horizontal].Add(tempHex);
                
            }
        }
        maxHeight = vertical;
        maxWidth = horizontal;
    }

    private Vector2 Offset_to_axial(Vector2 hex)
    {
        float q = hex.x;
        float r = hex.y + ((hex.x + (hex.x%2)) / 2);
        return new Vector2(q, r);
    }

    private Vector2 Axial_to_Offset(Vector2 hex)
    {
        float col = hex.x;
        float row = hex.y - ((hex.x + (hex.x % 2)) / 2);
        return new Vector2(col, row);
    }
    
    public bool AddObjectToEmptyGrid(GameObject obj, HexCell cell)
    {
        if(cell.full)
        {
            return false;
        }
        else
        {
            obj.transform.SetParent(cell.transform, false);
            obj.transform.position = cell.transform.position;
            cell.objectInCell = obj;
            cell.full = true;

            return true;
        }
    }

    public void AddToPathLists(HexCell cell, towerinteraction tower)
    {
        List<Vector2> points = ReturnListOfAllPointsInRange(cell.gridIndex, (int)tower.range);

        for (int i = 0; i < points.Count; i++)
        {
            // Debug.Log(points[i]);
            Vector2 offset = Axial_to_Offset(points[i]);
            if(offset.x >= 0 && offset.y >= 0 && offset.x < maxWidth && offset.y < maxHeight)
            {
                GameObject currentCell = gridCells[(int)offset.x][(int)offset.y];
                //currentCell.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0.5f);
                tower.cellsInRange.Add(currentCell);
                if(currentCell.transform.childCount > 1)
                {
                    Point point = currentCell.transform.GetChild(1).gameObject.GetComponent<Point>();

                    if (point != null)
                    {
                        point.AddTower(tower);
                    }
                }
            }
            
        }
    }

    public void RemoveFromPathLists(HexCell cell, towerinteraction tower)
    {

        List<GameObject> points = tower.cellsInRange;

        for (int i = 0; i < points.Count; i++)
        {
            // Debug.Log(points[i]);
            if (points[i].transform.childCount > 1)
            {
                Point point = points[i].transform.GetChild(1).gameObject.GetComponent<Point>();

                if (point != null)
                {
                    point.RemoveTower(tower);
                }
            }
        }
    }

    private List<Vector2> ReturnListOfAllPointsInRange(Vector2 cell, int radius)
    {
        // Debug.Log(radius);
        List<Vector2> points = new() {cell};
        
        
        Vector2 currentCell = cell;
        //Debug.Log("starting cell");
        //Debug.Log(currentCell);
        // walk each ring or the radius
        for(int currentRadius = 1; currentRadius <= radius; currentRadius++)
        {
            //Debug.Log(currentRadius);
            currentCell = Axial_add(currentCell, axial_direction_vectors[4]);
            //Debug.Log("after moving down and left");
            //Debug.Log(currentCell);
            // change direction
            for (int direction = 0; direction < 6; direction++)
            {

                // step muliple time depending on the radius
                for(int step = 0; step < currentRadius; step++)
                {
                    points.Add(currentCell);
                    currentCell = Axial_neighbor(currentCell, direction);
                    //Debug.Log("after moving in direction" + direction);
                    //Debug.Log(currentCell);
                }
            }
        }

        return points;
    }

    private Vector2 Axial_add(Vector2 vec1, Vector2 vec2)
    {
        return vec1 + vec2;
    }

    private Vector2 Axial_neighbor(Vector2 vec, int direction)
    {
        return Axial_add(vec, axial_direction_vectors[direction]);
    }
    

    private void CreatePath()
    {
        PutPathOnCell(6, 9, "AA");
        PutPathOnCell(5, 8, "AB");
        PutPathOnCell(4, 7, "AC");
        PutPathOnCell(3, 6, "AD");
        PutPathOnCell(2, 5, "AE");
        PutPathOnCell(2, 4, "AF");
        PutPathOnCell(3, 4, "AG");
        PutPathOnCell(4, 4, "AH");
        PutPathOnCell(5, 4, "AI");
        PutPathOnCell(6, 5, "AJ");
        PutPathOnCell(7, 6, "AK");
        PutPathOnCell(8, 7, "AL");
        PutPathOnCell(9, 8, "AM");
        PutPathOnCell(10, 9, "AN");
        PutPathOnCell(11, 10, "AO");
        PutPathOnCell(12, 11, "AP");
        PutPathOnCell(13, 12, "AQ");
        PutPathOnCell(13, 13, "AR");
        PutPathOnCell(13, 14, "AS");
        PutPathOnCell(12, 14, "AT");
        PutPathOnCell(12, 15, "AU");
        PutPathOnCell(11, 15, "AV");
        PutPathOnCell(10, 15, "AW");
        PutPathOnCell(9, 15, "AX");
        PutPathOnCell(8, 15, "AY");
        PutPathOnCell(7, 14, "AZ");
        PutPathOnCell(6, 14, "BA");
        PutPathOnCell(5, 13, "BB");
        PutPathOnCell(4, 13, "BC");
        PutPathOnCell(3, 12, "BD");
        PutPathOnCell(2, 11, "BE");
        PutPathOnCell(1, 10, "BF");
        PutPathOnCell(0, 9, "BG");
        PutPathOnCell(0, 8, "BH");
    }

    private void PutPathOnCell(int x, int y, string name)
    {
        Vector2 vec = new Vector2(x, y);

        vec = Axial_to_Offset(vec);
        GameObject cell = gridCells[(int)vec.x][(int)vec.y];
        HexCell hexCell = cell.GetComponent<HexCell>();
        GameObject point = Instantiate(pathPrefab);
        point.name = name;

        AddObjectToEmptyGrid(point, hexCell);

        cell.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);

    }
}
