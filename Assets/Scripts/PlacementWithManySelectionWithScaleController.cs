using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARRaycastManager))]

public class PlacementWithManySelectionWithScaleController : MonoBehaviour
{
    [SerializeField]
    private GameObject placedPrefab;

    [SerializeField]
    private GameObject welcomePanel;

    [SerializeField]
    private Button dismissButton;

    [SerializeField]
    private Slider scaleSlider;

    [SerializeField]
    private Button toggleOptionsButton;

    [SerializeField]
    private GameObject options;

    [SerializeField]
    private Camera arCamera;
    
    private GameObject placedObject;
    
    private Vector2 touchPosition = default;

    private ARRaycastManager arRaycastManager;

    private bool onTouchHold = false;

    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private PlacementObject lastSelectedObject;

    private GameObject PlacedPrefab
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

    private void Awake()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
        dismissButton.onClick.AddListener(Dismiss);
        scaleSlider.onValueChanged.AddListener(ScaleChanged);
        toggleOptionsButton.onClick.AddListener(ToggleOptions);
    }

    private void ToggleOptions()
    {
        if (options.activeSelf)
        {
            toggleOptionsButton.GetComponentInChildren<Text>().text = "0";
            options.SetActive(false);
        }

        else
        {
            toggleOptionsButton.GetComponentInChildren<Text>().text = "X";
            options.SetActive(true);
        }
    }

    private void Dismiss() => welcomePanel.SetActive(false);

    private void ScaleChanged(float newValue)
    {
        if(lastSelectedObject != null && lastSelectedObject.IsSelected)
        {
            lastSelectedObject.transform.parent.localScale = Vector3.one * newValue;
        }
    }

    void Update()
    {
        if (welcomePanel.activeSelf || options.activeSelf) return;

        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId)) return;

            touchPosition = touch.position;

            if(touch.phase == TouchPhase.Began)
            {
                Ray ray = arCamera.ScreenPointToRay(touch.position);
                RaycastHit hitObject;
                
                if(Physics.Raycast(ray, out hitObject))
                {
                    lastSelectedObject = hitObject.transform.GetComponent<PlacementObject>();
                    if(lastSelectedObject != null)
                    {
                        PlacementObject[] allOtherObjects = FindObjectsOfType<PlacementObject>();
                        foreach(PlacementObject placementObject in allOtherObjects)
                        {
                            if(placementObject != lastSelectedObject)
                            {
                                placementObject.IsSelected = false;
                            }
                            else
                            {
                                placementObject.IsSelected = true;
                            }
                        }
                    }
                }

                if(arRaycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = hits[0].pose;

                    if(lastSelectedObject == null)
                    {
                        lastSelectedObject = Instantiate(PlacedPrefab, hitPose.position, hitPose.rotation).GetComponent<PlacementObject>();
                    }
                }
            }

            if(touch.phase == TouchPhase.Moved)
            {
                if(arRaycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = hits[0].pose;

                    if(lastSelectedObject != null && lastSelectedObject.IsSelected)
                    {
                        lastSelectedObject.transform.parent.position = hitPose.position;
                        lastSelectedObject.transform.parent.rotation = hitPose.rotation;
                    }
                }
            }
        }
    }
}
