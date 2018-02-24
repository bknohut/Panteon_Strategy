using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VerticalScroller : MonoBehaviour {

    private Scrollbar scroll;
    private Queue<GameObject> pool;

    public GameObject barracksItem, powerItem;
    public Image content;

    // Use this for initialization
    void Start()
    {
        scroll = transform.GetComponent<Scrollbar>();
        pool = new Queue<GameObject>();

        for (int i = 0; i < 6; i++)
        {
            pool.Enqueue(Instantiate(barracksItem));
            pool.Enqueue(Instantiate(powerItem));
        }


    }

    public void AddFromPool()
    {   
        if (scroll.value < 0.34 && scroll.value > 0.33 ) // fix
        {
            GameObject barracksTmp = pool.Dequeue();
            GameObject powerplantTmp = pool.Dequeue();
           
            barracksTmp.transform.parent = content.transform;
            barracksTmp.SetActive(true);

            powerplantTmp.transform.parent = content.transform;
            powerplantTmp.SetActive(true);

            GameObject queuedBarracks = content.transform.GetChild(0).gameObject;
            GameObject queuedPowerplant = content.transform.GetChild(1).gameObject;

            queuedBarracks.SetActive(false);
            queuedBarracks.transform.SetParent(null);
            pool.Enqueue(queuedBarracks);

            queuedPowerplant.SetActive(false);
            queuedPowerplant.transform.SetParent(null);
            pool.Enqueue(queuedPowerplant);
        }
    }
}
