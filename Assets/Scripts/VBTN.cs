using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class VBTN : MonoBehaviour
{

    GameObject virtualButtonObj;
    public GameObject cube;

    // Start is called before the first frame update
    void Start()
    {
        virtualButtonObj = GameObject.Find("VirtualButton");
        virtualButtonObj.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonPressed(VirtualBtnPressed);
        virtualButtonObj.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonReleased(VirtualBtnReleased);
        cube.SetActive(false);
    }

    public void VirtualBtnPressed(VirtualButtonBehaviour vb)
    {
        if (cube.activeSelf)
        {
            cube.SetActive(false);
        }
        else
        {
            cube.SetActive(true);
        }

    }

    public void VirtualBtnReleased(VirtualButtonBehaviour vb)
    {

    }
}
