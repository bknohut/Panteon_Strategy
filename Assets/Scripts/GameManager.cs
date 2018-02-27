using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public GameObject[] tilemap;
    public GameObject tiles;
    public GameObject tile;

    public Image informationImage;
    public Button spawn;
    public Text informationText;

    private Vector3 mousePos;
    private Vector3 tilePos;
    

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
        tilemap = new GameObject[390];
        tilePos = new Vector3( -4.814f, 2.627f, 0f);

        int tileId = 0;
        for( int j = 0; j < 15; j++ )
        {   
            for ( int i = 0; i < 26; i++ )
            {
                GameObject tmpTile = Instantiate(tile, tiles.transform);
                tilePos.x += 0.39f;
                tmpTile.transform.position = tilePos;
                tilemap[tileId] = tmpTile;
                tmpTile.GetComponent<Ground>().id = tileId++;

            }
            tilePos.y += 0.39f;
            tilePos.x = -4.814f;

        }
        Camera.main.transform.LookAt(tilemap[194].transform);
    }
    private void Update()
    {
    }
}
