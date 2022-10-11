using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARRaycastManager))]
public class MoveObject : MonoBehaviour
{
    public Camera arCamera;
    private Vector2 touchPosition = default;
    private ARRaycastManager arRaycastManager;
    private bool onTouchHold = false;
    private List<ARRaycastHit> hits ;



    // Start is called before the first frame update
    void Start()
    {
        hits = new List<ARRaycastHit>();
        arRaycastManager = GetComponent<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (StepTwoController.yinPlane.activeSelf)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                touchPosition = touch.position;

                if (touch.phase == TouchPhase.Began)
                {
                    Ray ray = arCamera.ScreenPointToRay(touch.position);
                    RaycastHit hitObject;

                    if (Physics.Raycast(ray, out hitObject))
                    {
                        if (hitObject.transform.name.Contains("YinPlane(Clone)") )
                        {
                            onTouchHold = true;

                        }
                    }
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    onTouchHold = false;

                }
            }


            if (arRaycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;
               
                if (onTouchHold)
                {
                    StepTwoController.yinPlane.transform.position = hitPose.position;
                    StepTwoController.yinPlane.transform.rotation = hitPose.rotation;
                }
            }
        }
    }
}
