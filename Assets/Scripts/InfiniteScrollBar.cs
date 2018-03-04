using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfiniteScrollBar : MonoBehaviour
{
    private ScrollRect scrollRect;
    private float margin;
    private float offset;
    private List<RectTransform> items = new List<RectTransform>();
    
    // set the listener on OnValueChange of the scroll rect
    void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
        scrollRect.onValueChanged.AddListener(OnScroll);
        // get the children
        for (int i = 0; i < scrollRect.content.childCount; i++)
            items.Add(scrollRect.content.GetChild(i).GetComponent<RectTransform>());
        // set offset
        offset = items[1].GetComponent<RectTransform>().anchoredPosition.y - items[0].GetComponent<RectTransform>().anchoredPosition.y;
        margin = offset * items.Count / 2;
    }
    // set the position of the items when their position exceed the margin
    public void OnScroll(Vector2 pos)
    {
        Vector2 tmpAnchoredPosition;
        for (int i = 0; i < items.Count; i++)
        {
            if (scrollRect.transform.InverseTransformPoint(items[i].gameObject.transform.position).y > -margin)
            {
                tmpAnchoredPosition = items[i].anchoredPosition;
                tmpAnchoredPosition.y += items.Count * offset;
                items[i].anchoredPosition = tmpAnchoredPosition;
                //scrollRect.content.GetChild(items.Count - 1).transform.SetAsFirstSibling(); // to change the order of the objects on hierarchy
            }
            else if (scrollRect.transform.InverseTransformPoint(items[i].gameObject.transform.position).y < margin)
            {
                tmpAnchoredPosition = items[i].anchoredPosition;
                tmpAnchoredPosition.y -= items.Count * offset;
                items[i].anchoredPosition = tmpAnchoredPosition;
                //scrollRect.content.GetChild(0).transform.SetAsLastSibling();
            }
        }
    }
}
