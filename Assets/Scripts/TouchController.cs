using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchController : MonoBehaviour
{

    public Camera arCamera;
    public GameObject panel;
    public AudioSource touchPrompt;
    public AudioSource tayinPrompt;


    private GameObject touchYin;
    private GameObject yinHidden;
    private GameObject planeHidden;
    private float onYinTime = default;
    private GameObject media;
    
    
    private bool isInitObj = false;
    private bool isOnPainting = false;


    private Vector2 touchPosition = default;
    


    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(false);
    }



    // Update is called once per frame
    void Update()
    {

        //博物馆是否初始化
        if (PlacementController.prefabIns == null)
        {
            return;
        }


        if (panel.activeSelf)
        {
            if(Screen.orientation == ScreenOrientation.Landscape && panel.GetComponent<Transform>().localScale.x < 1.75)
            {
                panel.GetComponent<Transform>().localScale = new Vector3(1.75f, 1f, 1);
            }
            else if(Screen.orientation == ScreenOrientation.Portrait && panel.GetComponent<Transform>().localScale.x > 0.77)
            {
                panel.GetComponent<Transform>().localScale = new Vector3(0.77f, 0.42f, 1);
            }
            return;
        }

        if (!isInitObj)
        {
            media = PlacementController.prefabIns.transform.Find("objs/mods/newbwg11/Hall Variant/touying/Media").gameObject;
            media.SetActive(false);

            touchYin = PlacementController.prefabIns.transform.Find("objs/mods/newbwg11/Hall Variant/xiaoyinzhang Variant").gameObject;

            yinHidden = PlacementController.prefabIns.transform.Find("objs/mods/newbwg11/Hall Variant/wu/yinHidden").gameObject;
            yinHidden.SetActive(false);

            planeHidden = PlacementController.prefabIns.transform.Find("objs/mods/newbwg11/Hall Variant/wu/planeHidden").gameObject;
            planeHidden.SetActive(false);

            isInitObj = true;
        }

        if (!touchYin.activeSelf && isOnPainting)
        {


            if (Time.time - onYinTime > 3.0f)
            {
                //yinHidden.transform.localPosition = new Vector3(0.1968f, 0.0212f, 1.234f);
                yinHidden.SetActive(false);
                touchYin.SetActive(true);
                isOnPainting = false;
            }
        }



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
                    TouchObject touchObject = hitObject.transform.GetComponent<TouchObject>();
                    if(touchObject != null)
                    {
                        touchPrompt.Play();
                        panel.SetActive(true);
                        return;
                    }


                    TouchObjectYin touchObjectYin = hitObject.transform.GetComponent<TouchObjectYin>();
                    if(touchObjectYin != null)
                    {
                        touchPrompt.Play();
                        touchYin.SetActive(false);
                        return;
                    }

                    TouchObjectPainting touchObjectPaint = hitObject.transform.GetComponent<TouchObjectPainting>();
                    if(touchObjectPaint != null)
                    {
                        if (!touchYin.activeSelf)
                        {
                            tayinPrompt.Play();
                            onYinTime = Time.time;
                            isOnPainting = true;
                            yinOnPaintings();
                            return;
                        }
                        touchPrompt.Play();
                    }

                    TouchObjectMedia touchObjectMedia = hitObject.transform.GetComponent<TouchObjectMedia>();
                    if(touchObjectMedia != null)
                    {
                        touchPrompt.Play();
                        if (media.activeSelf)
                        {
                            media.SetActive(false);
                        }
                        else
                        {
                            media.SetActive(true);
                        }
                    }
                    

                }
            }
        }
    }

    private void yinOnPaintings()
    {

        //yinHiddenClone = Instantiate(yinHidden);
        //yinHiddenClone.transform.parent = PlacementController.prefabIns.transform.Find("组065/组029/Group-1031938-37-220/Group-1031938-38-103");
        //yinHiddenClone.transform.localPosition = new Vector3(0.1968f, 0.0212f, -0.008f);
        //yinHiddenClone.transform.localRotation = Quaternion.Euler(90, 0, 0);


        //GameObject planeHiddenClone = Instantiate(planeHidden);
        //planeHiddenClone.transform.parent = PlacementController.prefabIns.transform.Find("组065/组029/Group-1031938-37-220/Group-1031938-38-103");
        //planeHiddenClone.transform.localPosition = new Vector3(0.1975f, 0.0212f, -0.015f);
        //planeHiddenClone.transform.localRotation = Quaternion.Euler(90, 0, 0);


        //planeHidden.transform.localPosition = new Vector3(0.1975f, 0.0212f, -0.013f);
        //yinHidden.transform.localPosition = new Vector3(0.1968f, 0.0212f, -0.008f);

        yinHidden.SetActive(true);
        planeHidden.SetActive(true);
        
    }

}
