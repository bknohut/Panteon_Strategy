using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemplateBuilding : MonoBehaviour
{
    public List<Collider> triggerList = new List<Collider>();
    public int triggerCount;

    public void OnTriggerEnter(Collider other)
    {   
        if (!triggerList.Contains(other))
        {
            triggerList.Add(other);
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (triggerList.Contains(other))
        {
            triggerList.Remove(other);
        }
    }
}
