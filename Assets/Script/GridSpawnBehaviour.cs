using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GridSpawnBehaviour : MonoBehaviour
{
    public GameObject gridPrefab;
    public int column, row;
    public float SpaceX, SpaceY;
    public float StartX, StartY;
    public int maxScan, maxExtract;
    public int totalPoint;
    public int mode = 0;
    public Text scoreText;
    public Text scanText;
    public Text extractText;

    List<GameObject> AllGrid = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        SpawnGrid();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePosition.x, mousePosition.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider)
            {
                float x = hit.collider.GetComponent<GridBehaviour>().x;
                float y = hit.collider.GetComponent<GridBehaviour>().y;
                if(mode == 0)
                {
                    if (maxScan > 0)
                    {
                        Scan(x, y);
                        maxScan--;
                    }
                }
                else
                {
                    if (maxExtract > 0)
                    { 
                        Extract(x, y);
                        maxExtract--;
                    }
                }
            }
        }

        scoreText.text = totalPoint.ToString();
        scanText.text = maxScan.ToString();
        extractText.text = maxExtract.ToString();
    }

    public void GameModeScan()
    {
        mode = 0;
    }

    public void GameModeExtract()
    {
        mode = 1;
    }

    void SpawnGrid()
    { 
        for(int x = 0; x < column; x++)
        {
            for(int y = 0; y < row; y++)
            {
                GameObject gridList = Instantiate(gridPrefab, new Vector3(StartX + SpaceX * (x % column), -StartY + SpaceY * (y % row)), Quaternion.identity);
                GridBehaviour gridB = gridList.GetComponent<GridBehaviour>();
                gridB.x = x;
                gridB.y = y;
                AllGrid.Add(gridList);
            }
        }
    }

    void Scan(float x, float y)
    {
        FindGrid(x, y).showColor();
        FindGrid(x-1, y+1).showColor();
        FindGrid(x, y+1).showColor();
        FindGrid(x+1, y+1).showColor();
        FindGrid(x-1, y).showColor();
        FindGrid(x+1, y).showColor();
        FindGrid(x-1, y-1).showColor();
        FindGrid(x, y-1).showColor();
        FindGrid(x+1, y-1).showColor();
    }

    void Extract(float x, float y)
    {
        FindGrid(x, y).Extract();
        FindGrid(x - 1, y + 1).Extract();
        FindGrid(x, y + 1).Extract();
        FindGrid(x + 1, y + 1).Extract();
        FindGrid(x - 1, y).Extract();
        FindGrid(x + 1, y).Extract();
        FindGrid(x - 1, y - 1).Extract();
        FindGrid(x, y - 1).Extract();
        FindGrid(x + 1, y - 1).Extract();
    }

    GridBehaviour FindGrid(float x, float y)
    {
        foreach (GameObject g in AllGrid)
        {
            GridBehaviour grid = g.GetComponent<GridBehaviour>();
            if(grid.x == x && grid.y == y)
            {
                return grid;
            }
        }
        return null;
    }
   
}
