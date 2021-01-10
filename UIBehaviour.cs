using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBehaviour : MonoBehaviour
{
    public GameObject infoContainer;
    // Start is called before the first frame update
    void Start()
    {
        infoContainer = GameObject.Find("UI/InfoContainer");
        infoContainer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
