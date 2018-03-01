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
    protected Text buildingName;
    protected Button spawn;


    protected virtual void Start()
    {
        information = GameManager.instance.informationImage;
        buildingName = GameManager.instance.informationText;
        spawn = GameManager.instance.spawn;
    }
    protected virtual void OnMouseDown()
    {
        information.gameObject.SetActive(true);
        information.sprite = splash;
    }
    
}
