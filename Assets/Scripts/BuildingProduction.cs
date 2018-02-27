using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingProduction : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public static GameObject draggedObject;

    private Vector3 startPos;
    private Transform initialParent;
    private GameObject[] constructionArea;
    private bool isConstructable;

    protected GameObject building;
    protected int constructionSize;
    
    
    public virtual void Start()
    {
        constructionArea = new GameObject[constructionSize];
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
        // clear selection
        foreach (GameObject tile in constructionArea)
        {   
            if( tile != null)
            {
                tile.GetComponent<Ground>().StartCoroutine("ChangeToDefaultColor");
            }
        }

        transform.position = Input.mousePosition;
        RaycastHit hit = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // upleftcornerid
        int constructionStartIndex;
        // occupation
        isConstructable = true;

        if ( Physics.Raycast(ray, out hit ))
        {   
            if ( hit.transform.gameObject.name != "grass_tile(Clone)")
            {
                StartCoroutine("ObstacleWarning");
                isConstructable = false;
                for ( int i = 0; i < constructionSize; i++ )
                {
                    constructionArea[i] = null;
                }
                return;
            }
            // id of leftmost tile
            constructionStartIndex = hit.transform.gameObject.GetComponent<Ground>().id;
            
            // barracks
            if( constructionSize == 16 )
            {
                if (constructionStartIndex >= 297)
                {
                    return;
                }
                int k, index = 0;
                GameObject[] tileMap = GameManager.instance.tilemap;
                for ( int j = 0; j < 4; j++ )
                {   
                    k = constructionStartIndex + j * 26;
                    for ( int i = 0; i < 4; i++ )
                    {
                        if( tileMap[k] != null )
                        {   
                            if(tileMap[k].GetComponent<Ground>().constructionEnable == false )
                            {
                                isConstructable = false;
                            }
                            constructionArea[index++] = tileMap[k++];
                            
                        }
                    }
                }

            }
            // powerplant
            else if ( constructionSize == 6)
            {
                if (constructionStartIndex >= 323)
                {
                    return;
                }
                int k, index = 0;
                GameObject[] tileMap = GameManager.instance.tilemap;
                for (int j = 0; j < 3; j++)
                {
                    k = constructionStartIndex + j * 26;
                    for (int i = 0; i < 2; i++)
                    {
                        if (tileMap[k] != null)
                        {
                            if (tileMap[k].GetComponent<Ground>().constructionEnable == false)
                            {
                                isConstructable = false;
                            }
                            constructionArea[index++] = tileMap[k++];
                        }
                    }
                }
            }
            foreach (GameObject tile in constructionArea)
            {
                if (tile != null)
                {
                    if (isConstructable == true)
                    {
                        tile.GetComponent<Ground>().StartCoroutine("Select");
                    }
                    else
                    {
                        tile.GetComponent<Ground>().StartCoroutine("Warning");
                    }
                }
            }
        }
        
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (isConstructable == false)
        {
            StartCoroutine("RejectConstruction");
        }
        else
        {
            foreach (GameObject tile in constructionArea)
            {
                tile.GetComponent<Ground>().StartCoroutine("ChangeToDefaultColor");
            }
            if (constructionSize == 16)
            {
                Vector3 buildingPosition = constructionArea[0].transform.position;
                buildingPosition.x += 0.57f;
                buildingPosition.y += 0.58f;
                Instantiate(building).transform.position = buildingPosition;
            }
            else if (constructionSize == 6)
            {
                Vector3 buildingPosition = constructionArea[0].transform.position;
                buildingPosition.x += 0.1875f;
                buildingPosition.y += 0.4f;
                Instantiate(building).transform.position = buildingPosition;
            }

            foreach (GameObject tile in constructionArea)
            {
                tile.GetComponent<Ground>().constructionEnable = false;
            }
        }

        draggedObject = null;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        
        if ( transform.parent == initialParent )
        {
            transform.position = startPos;

        }
        
    }
    IEnumerator ObstacleWarning()
    {
        yield return null;
    }
    IEnumerator RejectConstruction()
    {   
        yield return null;
        foreach( GameObject tile in constructionArea )
        {   
            if( tile != null )
            {
                tile.GetComponent<Ground>().StartCoroutine("FlashWarning");
            }
        }
    }
}