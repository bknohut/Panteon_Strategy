using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnButton : MonoBehaviour
{
    public GameObject tank;
    public Transform spawnLocation;

    public void Spawn()
    {
        Instantiate(tank, spawnLocation.position, spawnLocation.rotation);
    }

}
