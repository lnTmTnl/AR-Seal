using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SealUISituation : MonoBehaviour
{
    [HideInInspector]
    public enum situations
    {
        none = 0,
        style = 1,
        carve = 2,
        press = 3
    }
    [HideInInspector]
    static public situations situation; 

    // Start is called before the first frame update
    void Start()
    {
        //situation = situations.none;
        GameObject.Find("styleBtn").GetComponent<Button>().onClick.Invoke();
        //GameObject.Find("Dialog").SetActive(false);
        //GameObject.Find("PressText").SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
