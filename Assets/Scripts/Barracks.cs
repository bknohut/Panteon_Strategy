using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : Building
{
    public int a;
	// Use this for initialization
	protected override void Start ()
    {
        base.Start();
        splash = transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
    }
    protected override void OnMouseDown()
    {
        base.OnMouseDown();

        spawn.gameObject.SetActive(true);
        spawn.GetComponent<SpawnButton>().spawnerBarracks = transform.gameObject;

        buildingName.text = "BARRACKS";
    }
    public GameObject getSpawnLocation()
    {
        Vector2 spawnpoint = downLeftTileIndex;
        GameObject[,] tileMap = GameManager.instance.tilemap;

        //first candidate grid
        spawnpoint.x--;
        spawnpoint.y--;

        for( int i = (int)spawnpoint.x; i < (int)spawnpoint.x + 6; i++ )
        {
            int j = (int)spawnpoint.y;
            if ( isGridAvailable(i, j) )
            {
                if( tileMap[i, j].GetComponent<Ground>().isFree)
                {
                    return tileMap[i, j];
                }
            }
        }
        spawnpoint.x += 5;
        for( int j = (int)spawnpoint.y; j < (int)spawnpoint.y + 6; j++ )
        {
            int i = (int)spawnpoint.x;
            if (isGridAvailable(i, j))
            {
                if (tileMap[i, j].GetComponent<Ground>().isFree)
                {
                    return tileMap[i, j];
                }
            }
        }
        spawnpoint.y += 5;
        for (int i = (int)spawnpoint.x; i > (int)spawnpoint.x - 6; i--)
        {
            int j = (int)spawnpoint.y;
            if (isGridAvailable(i, j))
            {
                if (tileMap[i, j].GetComponent<Ground>().isFree)
                {
                    return tileMap[i, j];
                }
            }
        }
        spawnpoint.x -= 5;
        for (int j = (int)spawnpoint.y; j > (int)spawnpoint.y - 6; j--)
        {
            int i = (int)spawnpoint.x;
            if (isGridAvailable(i, j))
            {
                if (tileMap[i, j].GetComponent<Ground>().isFree)
                {
                    return tileMap[i, j];
                }
            }
        }
        return null;
    }
    private bool isGridAvailable(int i, int j)
    {
        if (i >= GameManager.instance.gridWidth || j >= GameManager.instance.gridHeight)
        {
            return false;
        }
        else if (i < 0 || j < 0)
        {
            return false;
        }
        return true;
    }

}
