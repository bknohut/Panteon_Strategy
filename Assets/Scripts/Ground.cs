using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ground : MonoBehaviour, IPQItem<Ground>
{
    public Vector2 index;
    public bool isOccupied = false;
    public bool hasUnit = false;
     
    public Ground parentTile;
    // a* costs
    public int gCost;
    public int hCost;
    public int fCost
    {   
        get
        {
            return gCost + hCost;
        }
    }
    // heap 
    private int priorityQIndex;
    // set as the destination
    protected void OnMouseOver()
    {
        // rightclick
        if (Input.GetMouseButtonDown(1))
        {
            GameManager.instance.destinationIndex = index;
            GameManager.instance.HandleMovement();
        }
    }
    // Flash warning
    IEnumerator FlashWarning()
    {   
        for( int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.1f);
            transform.GetComponent<SpriteRenderer>().color = Color.white;
            yield return new WaitForSeconds(0.1f);
            transform.GetComponent<SpriteRenderer>().color = Color.red;
        }
        transform.GetComponent<SpriteRenderer>().color = Color.white;

    }
    // Interface 
    public int QIndex
    {
        get
        {
            return priorityQIndex;
        }
        set
        {
            priorityQIndex = value;
        }
    }
    public int CompareTo( Ground tile )
    {
        int compared = fCost.CompareTo(tile.fCost);
        if( compared == 0)
        {
            compared = hCost.CompareTo(tile.hCost);
        }
        return -1 * compared;
    }
}
