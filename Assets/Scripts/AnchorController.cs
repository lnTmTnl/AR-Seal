using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARAnchorManager))]
public class AnchorController : MonoBehaviour
{
    public Camera mCamera;
    public GameObject spawnPrefab;
    private GameObject spawnedObject = null;
    private ARAnchorManager mARAnchorManager;
    private float distance = 0.5f;
    
    void Start()
    {
        mARAnchorManager = transform.GetComponent<ARAnchorManager>();
    }
    
    void Update()
    {
        if (Input.touchCount == 0) return;
        var touch = Input.GetTouch(0);
        if(spawnedObject == null)
        {
            Vector3 mMenu = mCamera.transform.forward.normalized * distance;
            //Pose mpose = new Pose(mCamera.transform.position + mMenu, mCamera.transform.rotation);
            //ARAnchor mARAnchor = ARAnchor()
            spawnedObject = Instantiate(spawnPrefab, mCamera.transform.position + mMenu, mCamera.transform.rotation);
            spawnedObject.AddComponent<ARAnchor>();
        }
    }
}
