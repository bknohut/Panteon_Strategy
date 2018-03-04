using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Building : MonoBehaviour
{

    public Vector2 downLeftTileIndex;

    protected Sprite splash;
    protected Image information;
    protected Text nameArea;
    protected string buildingName;
    protected Button spawn;


    protected virtual void Start()
    {
        information = UIManager.instance.informationImage;
        nameArea = UIManager.instance.informationText;
        spawn = UIManager.instance.spawn;
    }
    protected virtual void OnMouseDown()
    {
        information.gameObject.SetActive(true);
        information.sprite = splash;
        nameArea.text = buildingName;
    }
    protected void OnMouseEnter()
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.green;
    }
    protected void OnMouseExit()
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
    }
    protected IEnumerator Warning(SpriteRenderer spriteRenderer)
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
