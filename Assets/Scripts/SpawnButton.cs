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
        GameObject tmpTank = Instantiate(tank, spawnLocation.transform.position, spawnLocation.transform.rotation);
        tmpTank.transform.SetParent(spawnLocation.transform);
        tmpTank.GetComponent<Tank>().index = spawnLocation.GetComponent<Ground>().index;

        spawnLocation.GetComponent<Ground>().isOccupied = true;
        spawnLocation.GetComponent<Ground>().hasUnit = true;
    }

}
