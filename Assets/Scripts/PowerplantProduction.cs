using UnityEngine;
using System.Collections;

public class PowerplantProduction : BuildingProduction
{
    public GameObject powerplant;
    public override void Start()
    {   
        constructionSize = new Vector2(2,3);
        base.Start();
        building = powerplant;
    }
}
