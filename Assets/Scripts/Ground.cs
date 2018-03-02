using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ground : MonoBehaviour
{
    public Vector2 index;
    public bool isFree = true;

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

}
