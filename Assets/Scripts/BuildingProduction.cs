﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingProduction : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public static GameObject draggedObject;

    protected GameObject building;
    protected Vector2 constructionSize;

    private Vector3 startPos;
    private Transform initialParent;
    private List<SpriteRenderer> coloredTiles;
    private GameObject initialTile;
    private bool isConstructable;
    private int constructableTileCount;

    

    public virtual void Start()
    {
        coloredTiles = new List<SpriteRenderer>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        draggedObject = gameObject;
        startPos = transform.position;
        initialParent = transform.parent;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }
  
    public void OnDrag(PointerEventData eventData)
    {   
        transform.position = Input.mousePosition;
        RaycastHit hit = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        foreach (SpriteRenderer spriteRenderer in coloredTiles)
        {
            spriteRenderer.color = Color.white;
        }

        if (Physics.Raycast(ray, out hit))
        {
            if( hit.transform.gameObject.tag != "Grass")
            {
                IEnumerator coroutine = Warning(hit.transform.GetChild(0).GetComponent<SpriteRenderer>());
                StartCoroutine(coroutine);
                return;
            }
            initialTile = hit.transform.gameObject;
            List<List<GameObject>> chosenGrids = decideGrid(constructionSize, hit.transform.gameObject.GetComponent<Ground>().index);
            coloredTiles = new List<SpriteRenderer>();
            constructableTileCount = chosenGrids[0].Count;

            foreach ( GameObject tile in chosenGrids[0])
            {
                tile.GetComponent<SpriteRenderer>().color = Color.blue;
                coloredTiles.Add(tile.GetComponent<SpriteRenderer>());
            }
            foreach (GameObject tile in chosenGrids[1])
            {
                tile.GetComponent<SpriteRenderer>().color = Color.red;
                coloredTiles.Add(tile.GetComponent<SpriteRenderer>());
            }
        }
        
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        List<GameObject> chosenGrids = getGrid(constructionSize, initialTile.GetComponent<Ground>().index);

        if (constructableTileCount == constructionSize.x * constructionSize.y)
        {
            foreach( GameObject tile in chosenGrids )
            {
                tile.GetComponent<Ground>().isOccupied = true;
            }

            GameObject tmp = Instantiate(building, initialTile.transform.position, initialTile.transform.rotation);
            tmp.GetComponent<Building>().downLeftTileIndex = initialTile.GetComponent<Ground>().index;


        }
        else
        {
            foreach (GameObject tile in chosenGrids)
            {
                tile.GetComponent<Ground>().StartCoroutine("FlashWarning");
            }
        }
        foreach (SpriteRenderer spriteRenderer in coloredTiles)
        {
            spriteRenderer.color = Color.white;
        }


        draggedObject = null;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        
        if ( transform.parent == initialParent )
        {
            transform.position = startPos;

        }
        
    }

    private List<List<GameObject>> decideGrid( Vector2 size, Vector2 tileLocation )
    {
        List<List<GameObject>> result = new List<List<GameObject>>();
        result.Add(new List<GameObject>());
        result.Add(new List<GameObject>());

        for ( int i = (int)tileLocation.x; i < tileLocation.x+size.x; i++ )
        {   
            for ( int j = (int)tileLocation.y; j < tileLocation.y+size.y; j++ )
            {
                if( !isGridAvailable(i, j) )
                {
                    continue;
                }
                // select
                if( !TileManager.instance.tilemap[i,j].GetComponent<Ground>().isOccupied )
                {
                    result[0].Add(TileManager.instance.tilemap[i, j]);
                }
                // reject
                else
                {
                    result[1].Add(TileManager.instance.tilemap[i, j]);
                }
            }
        }
        return result;
    }
    private List<GameObject> getGrid(Vector2 size, Vector2 tileLocation)
    {
        List<GameObject> result = new List<GameObject>();

        for (int i = (int)tileLocation.x; i < tileLocation.x + size.x; i++)
        {
            for (int j = (int)tileLocation.y; j < tileLocation.y + size.y; j++)
            {
                if (!isGridAvailable(i, j))
                {
                    continue;
                }
                result.Add(TileManager.instance.tilemap[i, j]);
            }
        }
        return result;
    }


    private bool isGridAvailable( int i, int j )
    {   
        if ( i >= TileManager.instance.gridWidth || j >= TileManager.instance.gridHeight )
        {
            return false;
        }
        else if(i < 0 || j < 0 )
        {
            return false;
        }
        return true;
    }

    IEnumerator Warning( SpriteRenderer spriteRenderer)
    {
        for (int i = 0; i < 2; i++)
        {
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = Color.white;         
        }
    }
}