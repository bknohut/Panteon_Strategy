using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public static GameObject draggedObject;
    public GameObject box;
    private Vector3 startPos;
    private Transform initialParent;

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
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        RaycastHit hit = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(transform.position);
        if( Physics.Raycast(ray, out hit))
        {
            Instantiate(box, hit.point, box.transform.rotation);
            
            Debug.Log(hit.point);
        }

        draggedObject = null;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        
        if ( transform.parent == initialParent )
        {
            transform.position = startPos;

        }
        
    }


}