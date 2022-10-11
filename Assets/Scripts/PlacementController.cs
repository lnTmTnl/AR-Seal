using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARRaycastManager))]
public class PlacementController : MonoBehaviour
{
    public Camera mCamera;
    [SerializeField]
    private GameObject placedPrefab;
    private Vector3 screenCenter;
    private float distance = 5f;

    public GameObject PlacedPrefab
    {
        get
        {
            return placedPrefab;
        }

        set
        {
            placedPrefab = value;
        }
    }

    private ARRaycastManager arRaycastManager;
    private ARPlaneManager arPlaneManager;

    private bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if(Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }

        touchPosition = default;
        return false;
    }

    private void Awake()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
        arPlaneManager = GetComponent<ARPlaneManager>();
        isExisted = false;
    }

    void Start()
    {
        screenCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
    }

    void Update()
    {
        if (!TryGetTouchPosition(out Vector2 touchPosition) || isExisted) return;

        if(arRaycastManager.Raycast(screenCenter, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;

            Vector3 mMenu = mCamera.transform.forward.normalized * (distance);
            Vector3 insPosition = mCamera.transform.position + mMenu;
            insPosition.Set(insPosition.x, 0, insPosition.z);

            prefabIns = Instantiate(PlacedPrefab, insPosition, hitPose.rotation);
            prefabIns.AddComponent<ARAnchor>();
            //prefabIns.transform.localRotation = Quaternion.Euler(Vector3.up * 180f);
            //PlacementController.prefabIns.transform.position = new Vector3(0, 0, -2.5f);
            prefabIns.transform.Find("objs").GetComponent<Animator>().Play("expendMuseum", 0);

            isExisted = true;

            /*foreach (var plane in arPlaneManager.trackables)
            {
                plane.gameObject.SetActive(false);
            }*/

        }
    }

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    public static GameObject prefabIns;
    public static bool isExisted;
}
