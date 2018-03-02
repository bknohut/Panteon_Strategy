using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnButton : MonoBehaviour
{
    public GameObject tank;
    public GameObject spawnerBarracks;
    
    public void Spawn()
    {
        GameObject spawnLocation = spawnerBarracks.GetComponent<Barracks>().getSpawnLocation();
        if( spawnLocation == null )
        {
            return;
        }
        Instantiate(tank, spawnLocation.transform.position, spawnLocation.transform.rotation);
        spawnLocation.GetComponent<Ground>().isFree = false;
    }

}
