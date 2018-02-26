using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public GameObject tiles;
    public GameObject tile;
    public GameObject barracks;
    public GameObject powerplant;

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
        tilePos = new Vector3( -4.814f, 2.627f, 0f);

        for ( int i = 0; i < 26; i++ )
        {
            tilePos.y = 2.627f;
            for( int y = 0; y < 15; y++ )
            {
                GameObject tmpTile = Instantiate(tile, tiles.transform);
                tilePos.y += 0.39f;
                tmpTile.transform.position = tilePos;
            }
            tilePos.x += 0.39f;
        }


    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            GameObject tmpBuilding = Instantiate(barracks);
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            tmpBuilding.transform.position = Vector2.Lerp(tmpBuilding.transform.position, mousePos, 0.1f * Time.deltaTime);

        }
    }
}
