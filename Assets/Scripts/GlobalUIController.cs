using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GlobalUIController : MonoBehaviour
{
    public Text topText;
    public GameObject backBtn;
    public GameObject UICircle;
    public GameObject UISquare;
    public GameObject UICircleBtn;
    public GameObject UISquareBtn;
    public GameObject circle;
    public GameObject floatSquare;
    public Text CircleText;
    public Text TopText;
    public List<GameObject> btns;

    [HideInInspector]
    public static int btnIndex;
    public int preIndex;

    private Animator circleAnimator;
    private Animator floatSquareAnimator;
    private float angleOfOnce;
    private float rotateDirecrion;
    private ScreenOrientation preScreenOrientation;
    private enum rotateIndexs
    {
        seal = 0,
        ar = 1,
        museum = 2
    }
    void Start()
    {
        circleAnimator = circle.GetComponent<Animator>();
        floatSquareAnimator = floatSquare.GetComponent<Animator>();
        angleOfOnce = 29 / 2;
        btnIndex = 1;
        preIndex = 1;
        preScreenOrientation = Screen.orientation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Screen.orientation == ScreenOrientation.Landscape)
        {
            topText.GetComponent<Text>().fontSize = 70;
            topText.GetComponent<RectTransform>().sizeDelta = new Vector2(320, 103);
            backBtn.GetComponent<RectTransform>().localScale = new Vector3(1.25f, 1.25f, 1.25f);

            if (UICircle.activeSelf)
            {
                UICircle.SetActive(false);
            }
            if (UICircleBtn.activeSelf)
            {
                UICircleBtn.SetActive(false);
            }

            if (!UISquare.activeSelf)
            {
                UISquare.SetActive(true);
            }
            if (!UISquareBtn.activeSelf)
            {
                UISquareBtn.SetActive(true);
            }

            if(preScreenOrientation != Screen.orientation)
            {
                if (btnIndex == (int)rotateIndexs.seal && preIndex != btnIndex)
                {
                    floatSquareAnimator.Play("squareT", 0);
                    preIndex = (int)rotateIndexs.seal;
                }
                else if (btnIndex == (int)rotateIndexs.ar && preIndex != btnIndex)
                {
                    floatSquareAnimator.Play("squareC", 0);
                    preIndex = (int)rotateIndexs.ar;
                }
                else if (btnIndex == (int)rotateIndexs.museum && preIndex != btnIndex)
                {
                    floatSquareAnimator.Play("squareB", 0);
                    preIndex = (int)rotateIndexs.museum;
                }
                preScreenOrientation = Screen.orientation;
            }
            
        }
        else if(Screen.orientation == ScreenOrientation.Portrait)
        {
            topText.GetComponent<Text>().fontSize = 54;
            topText.GetComponent<RectTransform>().sizeDelta = new Vector2(249, 81);
            backBtn.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

            if (!UICircle.activeSelf)
            {
                UICircle.SetActive(true);
            }
            if (!UICircleBtn.activeSelf)
            {
                UICircleBtn.SetActive(true);
            }

            if (UISquare.activeSelf)
            {
                UISquare.SetActive(false);
            }
            if (UISquareBtn.activeSelf)
            {
                UISquareBtn.SetActive(false);
            }
            if(preScreenOrientation != Screen.orientation)
            {
                if (btnIndex == (int)rotateIndexs.seal && preIndex != btnIndex)
                {
                    rotateDirecrion = (int)rotateIndexs.seal - preIndex;
                    circleAnimator.Play("circleR", 0);
                    preIndex = (int)rotateIndexs.seal;
                    btnsChange();
                }
                else if (btnIndex == (int)rotateIndexs.ar && preIndex != btnIndex)
                {
                    rotateDirecrion = (int)rotateIndexs.ar - preIndex;
                    circleAnimator.Play("circleC", 0);
                    preIndex = (int)rotateIndexs.ar;
                    btnsChange();
                }
                else if (btnIndex == (int)rotateIndexs.museum && preIndex != btnIndex)
                {
                    rotateDirecrion = (int)rotateIndexs.museum - preIndex;
                    circleAnimator.Play("circleL", 0);
                    preIndex = (int)rotateIndexs.museum;
                    btnsChange();
                }
                preScreenOrientation = Screen.orientation;
            }
            /**/
        }
    }

    private void btnsChange()
    {
        foreach (GameObject btn in btns)
        {
            btn.transform.parent.transform.parent.GetComponent<RectTransform>().Rotate(Vector3.forward, -rotateDirecrion * angleOfOnce);
            btn.GetComponent<Image>().color = new Color(255, 255, 255, 0.5f);
            btn.transform.Find("dot").GetComponent<Image>().color = new Color(255, 255, 255, 0.5f);
        }
        btns[btnIndex].GetComponent<Image>().color = new Color(255, 255, 255, 1);
        btns[btnIndex].transform.Find("dot").GetComponent<Image>().color = new Color(255, 255, 255, 1);

        switch (btnIndex)
        {
            case (int)rotateIndexs.seal:
                CircleText.text = "ARÓ¡ÕÂ";
                TopText.text = "ARÓ¡ÕÂ";
                break;

            case (int)rotateIndexs.ar:
                CircleText.text = "ARÉ¨Ãè";
                TopText.text = "ARÉ¨Ãè";
                break;

            case (int)rotateIndexs.museum:
                CircleText.text = "AR²©Îï¹Ý";
                TopText.text = "AR²©Îï¹Ý";
                break;
        }
    }

    private object getResources()
    {
        throw new NotImplementedException();
    }
}
