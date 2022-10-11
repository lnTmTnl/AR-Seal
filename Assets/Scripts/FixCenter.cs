using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixCenter : MonoBehaviour
{
    public GameObject obj;
    public Camera arCamera;

    private float distance = 2.0f;

    public Vector3 getInsPosition()
    {
        Vector3 mMenu = arCamera.transform.forward.normalized * distance;
        Vector3 insPosition = arCamera.transform.position + mMenu;
        return insPosition;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (obj.activeSelf)
        {
            obj.transform.position = getInsPosition();
        }
    }
}
