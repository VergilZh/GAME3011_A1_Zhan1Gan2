using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBehaviour : MonoBehaviour
{

    public float x, y;
    public Color color;
    public int score;

    // Start is called before the first frame update
    void Start()
    {
        int rand = Random.Range(0, 4);
        switch (rand)
        {
            case 0:
                color = Color.red;
                score = 500;
                break;
            case 1:
                color = Color.yellow;
                score = 100;
                break;
            case 2:
                color = Color.gray;
                score = 50;
                break;
            case 3:
                color = Color.white;
                score = 0;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
  
    }

    public void showColor()
    {
        GetComponent<SpriteRenderer>().color = color;
    }

    public void Extract()
    {
        FindObjectOfType<GridSpawnBehaviour>().totalPoint += score;
        score /= 2;
        color = Color.black;
        showColor();
    }
}
