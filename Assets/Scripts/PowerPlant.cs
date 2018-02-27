using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPlant : Building
{

    // Use this for initialization
    protected override void Start()
    {
       // splash = transform.GetComponent<SpriteRenderer>().sprite;
    }
    protected override void OnMouseDown()
    {
        base.OnMouseDown();
        spawn.gameObject.SetActive(false);

        buildingName.text = "POWER PLANT";
    }
}
