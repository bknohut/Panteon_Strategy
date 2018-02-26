using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VerticalScroller : MonoBehaviour {

    private Scrollbar scroll;
    private Queue<GameObject> pool;
    private float oldValue;
    private float difference;

    public GameObject barracksItem;
    public GameObject powerItem;
    public GameObject poolObject;
    public Image content;

    // Use this for initialization
    void Start()
    {   
        scroll = transform.GetComponent<Scrollbar>();
        oldValue = scroll.value;
        pool = new Queue<GameObject>();

        for (int i = 0; i < 12; i++)
        {
            pool.Enqueue(Instantiate(barracksItem,poolObject.transform));
            pool.Enqueue(Instantiate(powerItem,poolObject.transform));
        }

    }
    private void Update()
    {
        difference = oldValue - scroll.value;
        if (difference > 0.2f) // fix
        {
            handleElements();
            //transform.GetComponent<Scrollbar>().value = 0.4923078f;
            oldValue = scroll.value;
        }
        else if (difference < -0.2f)
        {
            handleElements();
            //transform.GetComponent<Scrollbar>().value = 0.4923078f;
            oldValue = scroll.value;
        }
    }

    void handleElements()
    {
        GameObject barracksTmp = pool.Dequeue();
        GameObject powerplantTmp = pool.Dequeue();

        barracksTmp.transform.SetParent(content.transform);
        barracksTmp.SetActive(true);

        powerplantTmp.transform.SetParent(content.transform);
        powerplantTmp.SetActive(true);

        GameObject queuedBarracks = content.transform.GetChild(0).gameObject;
        GameObject queuedPowerplant = content.transform.GetChild(1).gameObject;

        queuedBarracks.SetActive(false);
        queuedBarracks.transform.SetParent(poolObject.transform);
        pool.Enqueue(queuedBarracks);

        queuedPowerplant.SetActive(false);
        queuedPowerplant.transform.SetParent(poolObject.transform);
        pool.Enqueue(queuedPowerplant);
    }
}
