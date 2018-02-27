using UnityEngine;
using System.Collections;

public class BarracksProduction : BuildingProduction
{
    public GameObject barracks;
    // Use this for initialization
    public override void Start()
    {
        constructionSize = 16;
        base.Start();
        building = barracks;
    }
}
