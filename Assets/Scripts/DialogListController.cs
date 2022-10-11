using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogListController : MonoBehaviour
{
    public List<Button> btns;
    public GameObject dialog;
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(BtnClicked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BtnClicked()
    {
        transform.Find("Text").GetComponent<Text>().color = new Color(249f/255f, 59f/255f, 59f/255f, 1);
        for (int i = 0; i < btns.Count; i++)
        {
            btns[i].transform.Find("Text").GetComponent<Text>().color = new Color(45f/255f, 45f/255f, 45/255f, 1);
        }
        switch (transform.Find("Text").GetComponent<Text>().text)
        {
            case "ÑùÊ½Ñ¡Ôñ":
                {
                    if (SealUISituation.situation == SealUISituation.situations.carve && !GameObject.Find("circleBtn").GetComponent<CircleBtnController>().isUp)
                    {
                        GameObject.Find("Draw").GetComponent<Animator>().Play("drawHide", 0);
                        GameObject.Find("Scroll").GetComponent<Animator>().Play("scrollAppear", 0);
                        //GameObject.Find("Slider").GetComponent<Animator>().Play("sliderAppear", 0);
                    }
                    else if (SealUISituation.situation == SealUISituation.situations.press)
                    {
                        GameObject.Find("PressText").GetComponent<Animator>().Play("pressTextHide", 0);
                        if (!GameObject.Find("circleBtn").GetComponent<CircleBtnController>().isUp)
                        {
                            GameObject.Find("Scroll").GetComponent<Animator>().Play("scrollAppear", 0);
                            //GameObject.Find("Slider").GetComponent<Animator>().Play("sliderAppear", 0);
                        }
                    }
                    SealUISituation.situation = SealUISituation.situations.style;
                    break;
                }
                
            case "Ó¡ÕÂµñ¿Ì":
                {
                    if (SealUISituation.situation == SealUISituation.situations.style && !GameObject.Find("circleBtn").GetComponent<CircleBtnController>().isUp)
                    {
                        GameObject.Find("Scroll").GetComponent<Animator>().Play("scrollHide", 0);
                        //GameObject.Find("Slider").GetComponent<Animator>().Play("sliderHide", 0);
                        GameObject.Find("Draw").GetComponent<Animator>().Play("drawAppear", 0);
                    }
                    else if (SealUISituation.situation == SealUISituation.situations.press)
                    {
                        GameObject.Find("PressText").GetComponent<Animator>().Play("pressTextHide", 0);
                        if (!GameObject.Find("circleBtn").GetComponent<CircleBtnController>().isUp)
                        {
                            GameObject.Find("Draw").GetComponent<Animator>().Play("drawAppear", 0);
                        }
                    }
                    SealUISituation.situation = SealUISituation.situations.carve;
                    break;
                }
                
            case "Ó¡ÕÂÍØÓ¡":
                {
                    if (SealUISituation.situation == SealUISituation.situations.style && !GameObject.Find("circleBtn").GetComponent<CircleBtnController>().isUp)
                    {
                        GameObject.Find("Scroll").GetComponent<Animator>().Play("scrollHide", 0);
                        //GameObject.Find("Slider").GetComponent<Animator>().Play("sliderHide", 0);
                    }
                    else if (SealUISituation.situation == SealUISituation.situations.carve && !GameObject.Find("circleBtn").GetComponent<CircleBtnController>().isUp)
                    {
                        GameObject.Find("Draw").GetComponent<Animator>().Play("drawHide", 0);
                    }
                    //GameObject.Find("PressText").SetActive(true);
                    GameObject.Find("PressText").GetComponent<Animator>().Play("pressTextAppear", 0);
                    SealUISituation.situation = SealUISituation.situations.press;
                    break;
                }
        }
        dialog.SetActive(false);
    }
}
