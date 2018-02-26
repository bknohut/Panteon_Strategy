using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropHandler : MonoBehaviour//, IDropHandler
{
    private GameObject item;
    private Color defaultColor;
    private SpriteRenderer tileRenderer;

    private void Start()
    {
        tileRenderer = transform.GetComponent<SpriteRenderer>();
        defaultColor = tileRenderer.color;
    }
    private void OnMouseOver()
    {
        tileRenderer.color = Color.black;
    }
    private void OnMouseExit()
    {
        tileRenderer.color = defaultColor;
    }

   /*public void OnDrop( PointerEventData eventData )
    {
        Debug.Log("sad");
        if ( transform.childCount == 0)
        {
            return;
        }
        else
        {
            item = transform.GetChild(0).gameObject;
        }
        if ( !item )
        {
            DragHandler.draggedObject.transform.SetParent(transform);
        }
    }*/
}
