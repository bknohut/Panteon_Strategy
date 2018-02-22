using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour {

    // Use this for initialization

    protected Sprite splash;
    public Image information;
    public Text buildingName;
    public Button spawn;

	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    protected virtual void OnMouseDown()
    {
        information.gameObject.SetActive(true);
        information.sprite = splash;
    }
}
