using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class PressSeal : MonoBehaviour
{

    private List<ARRaycastHit> Hits;
    private ARRaycastManager mRaycastManager;
    private GameObject moudleClone;  //复制自定义的印章
    private float touchTime;
    private bool isNewTouch = default;

    public AudioSource yinSet;

    // Start is called before the first frame update
    void Start()
    {
        Hits = new List<ARRaycastHit>();
        mRaycastManager = GetComponent<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
       
        if (StepTwoController.yinPlane.activeSelf)
            return;


        if (Input.touchCount <= 0)
            return;

        if (Input.touchCount == 1)
        {
            var touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                isNewTouch = true;
                touchTime = Time.time;

            }
            else if (touch.phase == TouchPhase.Stationary)
            {
                if (isNewTouch == true && Time.time - touchTime > 1f)
                {
                    isNewTouch = false;

                    if (mRaycastManager.Raycast(touch.position, Hits, TrackableType.PlaneWithinPolygon | TrackableType.PlaneWithinBounds))
                    {
                        var hitPose = Hits[0].pose;
                        if (moudleClone == null)
                        {
                            //float moudleHeight = StepTwoController.prefabIns.GetComponent<Renderer>().bounds.size.z;
                            //固定角度
                            moudleClone = Instantiate(StepTwoController.prefabIns, hitPose.position + new Vector3(0, 0.04f, 0), Quaternion.Euler(-90, 90, 90));
                            //固定大小
                            moudleClone.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                            moudleClone.GetComponent<MyRotate>().enabled = false;
                            moudleClone.GetComponent<Enlarge>().enabled = false;
                            yinSet.Play();
                            moudleClone.SetActive(true);
                            //GameObject yinPlaneClone = Instantiate(StepTwoController.yinPlane, hitPose.position, Quaternion.Euler(0, 0, 0));
                            StepTwoController.yinPlane.transform.localScale = new Vector3(0.0033f, 0.0033f, 0.0033f);
                            StepTwoController.yinPlane.transform.position = hitPose.position;
                            StepTwoController.yinPlane.transform.rotation = Quaternion.Euler(0, 0, 0);
                            StepTwoController.yinPlane.SetActive(true);
                            Destroy(moudleClone, 3.0f);
                        }
                    }
                }
            }
            else
            {
                isNewTouch = false;
            }
        }
    }
}
