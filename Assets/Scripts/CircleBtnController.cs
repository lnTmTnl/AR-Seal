using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CircleBtnController : MonoBehaviour
{
    private Animator animator;

    [HideInInspector]
    public bool isUp = true;
    public GameObject uiCircle;
    //public Text text;
    // Start is called before the first frame update
    void Start()
    {
        animator = transform.Find("up").GetComponent<Animator>();
        GetComponent<Button>().onClick.AddListener(Click);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Click()
    {
        //text.text = "Clicked";
        if (isUp)
        {
            HideCircle();
        }
        else
        {
            RiseCircle();
        }
        isUp = !isUp;
        //text.text = isHiding.ToString();
    }

    private void HideCircle()
    {
        uiCircle.GetComponent<Animator>().Play("circleHide", 0);
        animator.Play("btnDown2Up", 0);
        SealUIControl();
    }

    private void RiseCircle()
    {
        uiCircle.GetComponent<Animator>().Play("circleRise", 0);
        animator.Play("btnUp2Down", 0);
        SealUIControl();
    }

    private void SealUIControl()
    {
        if (SceneManager.GetSceneByName("SealScene").isLoaded)
        {
            if (isUp)
            {
                if (SealUISituation.situation == SealUISituation.situations.style)
                {
                    GameObject.Find("Scroll").GetComponent<Animator>().Play("scrollAppear", 0);
                    //GameObject.Find("Slider").GetComponent<Animator>().Play("sliderAppear", 0);
                }
                else if(SealUISituation.situation == SealUISituation.situations.carve)
                {
                    GameObject.Find("Draw").GetComponent<Animator>().Play("drawAppear", 0);
                }
            }

            else
            {
                if (SealUISituation.situation == SealUISituation.situations.style)
                {
                    GameObject.Find("Scroll").GetComponent<Animator>().Play("scrollHide", 0);
                    //GameObject.Find("Slider").GetComponent<Animator>().Play("sliderHide", 0);
                }
                else if (SealUISituation.situation == SealUISituation.situations.carve)
                {
                    GameObject.Find("Draw").GetComponent<Animator>().Play("drawHide", 0);
                }
            }
        }
    }
}
