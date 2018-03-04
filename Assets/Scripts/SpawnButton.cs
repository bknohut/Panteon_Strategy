using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnButton : MonoBehaviour
{
    public GameObject tank;
    // clicked barracks
    public GameObject spawnerBarracks;
    
    public void Spawn()
    {   
        // get a spawn location from the spawner barracks
        GameObject spawnLocation = spawnerBarracks.GetComponent<Barracks>().getSpawnLocation();
        if( spawnLocation == null )
        {   
            return;
        }
        // spawn tank
        GameObject tmpTank = Instantiate(tank, spawnLocation.transform.position, spawnLocation.transform.rotation);
        tmpTank.transform.SetParent(spawnLocation.transform);
        tmpTank.GetComponent<Tank>().index = spawnLocation.GetComponent<Ground>().index;
        // set the location flags
        spawnLocation.GetComponent<Ground>().isOccupied = true;
        spawnLocation.GetComponent<Ground>().hasUnit = true;
    }

}
