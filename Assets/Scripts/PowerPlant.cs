using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPlant : Building
{
    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        splash = transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
        buildingName = "POWER PLANT";
    }
    protected override void OnMouseDown()
    {
        base.OnMouseDown();
        spawn.gameObject.SetActive(false);
    }
}
