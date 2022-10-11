using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PortallController : MonoBehaviour
{
    public GameObject HiddenWorld;

    private Vector3 camPosition;

    bool front;
    bool otherWorld;
    bool hasCollided;

    bool GetIsFront()
    {
        GameObject myCamera = GameObject.FindGameObjectWithTag("MainCamera");
        Vector3 worldPos = myCamera.transform.position + myCamera.transform.forward * Camera.main.nearClipPlane;
        camPosition = transform.InverseTransformPoint(worldPos);
        return camPosition.y >= 0 ? true : false;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject myCamera = GameObject.FindGameObjectWithTag("MainCamera");
        if (other.transform != myCamera.transform) return;

        front = GetIsFront();
        hasCollided = true;
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject myCamera = GameObject.FindGameObjectWithTag("MainCamera");
        if (other.transform != myCamera.transform) return;

        hasCollided = false;
    }

    void whileCameraColliding()
    {
        if (!hasCollided) return;

        bool isFront = GetIsFront();

        if((isFront && !front) || (front && !isFront))
        {
            otherWorld = !otherWorld;
        }

        front = isFront;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        whileCameraColliding();
    }
}
