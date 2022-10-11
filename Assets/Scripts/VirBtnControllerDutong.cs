using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class VirBtnControllerDutong : MonoBehaviour
{
    public GameObject book;
    public Camera arCamera;

    private List<ARRaycastHit> Hits;
    private ARRaycastManager mRaycastManager;
    private float touchTime;
    private bool isNewTouch = default;

    public AudioSource prompt;
    public GameObject ClosePromptPanel;


    // Start is called before the first frame update
    void Start()
    {
        Hits = new List<ARRaycastHit>();
        mRaycastManager = GetComponent<ARRaycastManager>();

        AudioConfiguration audio_config = AudioSettings.GetConfiguration();
        AudioSettings.Reset(audio_config);
    }


    public Vector3 getInsPosition()
    {
        Vector3 mMenu = arCamera.transform.forward.normalized * 0.5f;
        Vector3 insPosition = arCamera.transform.position + mMenu;
        return insPosition;
    }

    // Update is called once per frame
    void Update()
    {



        if (book.activeSelf)
        {
            book.transform.position = getInsPosition();
        }


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
                    book.SetActive(false);
                    prompt.Play();
                    ClosePromptPanel.SetActive(false);
                }
            }
            else
            {
                isNewTouch = false;
            }
        }


        if (book.activeSelf)
        {
            return;
        }


        //点击GIF图显示翻书动画
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {

                Ray ray = arCamera.ScreenPointToRay(touch.position);
                RaycastHit hitObject;
                if (Physics.Raycast(ray, out hitObject))
                {
                    TouchObjectDutong touchObject = hitObject.transform.GetComponent<TouchObjectDutong>();
                    if (touchObject != null)
                    {
                        prompt.Play();
                        ClosePromptPanel.SetActive(true);
                        book.transform.position = getInsPosition();
                        book.transform.rotation = Quaternion.Euler(90, 0, -120);
                        book.SetActive(true);
                        return;
                    }
                }
            }
        }
    }
}
