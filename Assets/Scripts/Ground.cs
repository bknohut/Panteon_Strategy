using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ground : MonoBehaviour
{
    public Vector2 index;
    public bool constructionEnable = true;

    private Color defaultColor;
    private SpriteRenderer tileRenderer;

    private void Start()
    {
        tileRenderer = transform.GetComponent<SpriteRenderer>();
        defaultColor = tileRenderer.color;
    }
    /*
    private void OnMouseOver()
    {
        tileRenderer.color = Color.black;
    }
    private void OnMouseExit()
    {
        tileRenderer.color = defaultColor;
    }
    */
    IEnumerator Warning()
    {   
        yield return null;
        transform.GetComponent<SpriteRenderer>().color = Color.red;
    }
    IEnumerator Select()
    {
        yield return null;
        transform.GetComponent<SpriteRenderer>().color = Color.blue;
    }
    IEnumerator ChangeToDefaultColor()
    {
        yield return null;
        transform.GetComponent<SpriteRenderer>().color = defaultColor;
    }
    IEnumerator FlashWarning()
    {   
        for( int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.1f);
            transform.GetComponent<SpriteRenderer>().color = defaultColor;
            yield return new WaitForSeconds(0.1f);
            transform.GetComponent<SpriteRenderer>().color = Color.red;
        }
        transform.GetComponent<SpriteRenderer>().color = defaultColor;

    }

}
