using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Building : MonoBehaviour
{

    // Use this for initialization

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
        Debug.Log("sdaş");
        information.gameObject.SetActive(true);
        information.sprite = splash;
    }
    
}
