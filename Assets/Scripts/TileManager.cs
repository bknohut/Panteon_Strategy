using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileManager : MonoBehaviour
{
    public static TileManager instance;
    public GameObject[,] tilemap;
    public GameObject tiles;
    public GameObject tile;

    // grid size
    public int gridHeight = 15;
    public int gridWidth = 15;

    private Vector3 tilePos;
    

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);

        // generate grid
        tilemap = new GameObject[15,15];
        tilePos = new Vector3( -4.814f, 2.627f, 0f);

        for( int j = 0; j < gridHeight; j++ )
        {   
            for ( int i = 0; i < gridWidth; i++ )
            {
                GameObject tmpTile = Instantiate(tile, tiles.transform);
                tilePos.x += 0.39f;
                tmpTile.transform.position = tilePos;
                tilemap[i,j] = tmpTile;
                tmpTile.GetComponent<Ground>().index.x = i;
                tmpTile.GetComponent<Ground>().index.y = j;

            }
            tilePos.y += 0.39f;
            tilePos.x = -4.814f;

        }
        Camera.main.transform.Translate(tilemap[gridWidth/2 -1, gridHeight /  2].transform.position);
    }

}
