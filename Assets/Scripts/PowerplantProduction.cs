using UnityEngine;
using System.Collections;

public class PowerplantProduction : BuildingProduction
{
    public GameObject powerplant;
    public override void Start()
    {
        constructionSize = 6;
        base.Start();
        building = powerplant;
    }
}
