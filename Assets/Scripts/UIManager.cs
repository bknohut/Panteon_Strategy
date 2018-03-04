using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// get UI elements
public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public Image informationImage;
    public Button spawn;
    public Text informationText;
    public Text status;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);

    }
}
