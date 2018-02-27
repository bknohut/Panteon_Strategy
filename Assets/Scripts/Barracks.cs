using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : Building
{
    
	// Use this for initialization
	protected override void Start ()
    {
        base.Start();
        //splash = transform.GetComponent<SpriteRenderer>().sprite;
    }
    protected override void OnMouseDown()
    {
        base.OnMouseDown();

        spawn.gameObject.SetActive(true);
        //spawn.GetComponent<SpawnButton>().spawnLocation = transform.GetChild(0).transform;

        buildingName.text = "BARRACKS";
    }
}
