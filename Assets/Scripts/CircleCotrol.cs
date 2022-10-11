using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CircleCotrol : MonoBehaviour
{
    public Text CircleText;
    public Text TopText;
    public float maxAngleRange;
    public List<GameObject> btns;
    public GameObject circleUpBtn;
    public GameObject ARGuidence;
    public GameObject SealGuidence;
    public GameObject MuseumGuidence;

    private bool isUp;
    private int countOfBtns;
    private float angleOfOnce;
    private float rotateDirecrion;
    private float circleHeight;
    //private int GlobalUIController.btnIndex;
    private bool isCircleActive;
    private Animator animator;
    private bool ARGuidenceLoaded;
    private bool SealGuidenceLoaded;
    private bool MuseumGuidenceLoaded;

    [HideInInspector]
    public enum rotateIndexs
    {
        seal = 0,
        ar = 1,
        museum = 2
    }
    // Start is called before the first frame update
    void Start()
    {
        countOfBtns = btns.Count;
        isUp = circleUpBtn.GetComponent<CircleBtnController>().isUp;
        if (SceneManager.GetSceneByName("UIScene").isLoaded == false)
            SceneManager.LoadSceneAsync("UIScene", LoadSceneMode.Additive);

        SceneManager.LoadSceneAsync("ARScene", LoadSceneMode.Additive).completed += _operation =>
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("ARScene"));

        //GetComponent<RectTransform>().rotation = Quaternion.Euler(Vector3.zero);

        angleOfOnce = maxAngleRange / (countOfBtns - 1);

        circleHeight = GetComponent<RectTransform>().rect.height;
        animator = GetComponent<Animator>();
        /*animator.Play("circleRotate", 0, 0.5f);
        animator.speed = 0;*/

        //GlobalUIController.btnIndex = 1;

        foreach(GameObject btn in btns)
        {
            btn.GetComponent<Image>().color = new Color(255, 255, 255, 0.5f);
            btn.transform.Find("dot").GetComponent<Image>().color = new Color(255, 255, 255, 0.5f);

        }
        btns[GlobalUIController.btnIndex].GetComponent<Image>().color = new Color(255, 255, 255, 1);
        btns[GlobalUIController.btnIndex].transform.Find("dot").GetComponent<Image>().color = new Color(255, 255, 255, 1);

        isCircleActive = true;

        ARGuidenceLoaded = false;
        SealGuidenceLoaded = false;
        MuseumGuidenceLoaded = false;
}

    // Update is called once per frame
    void Update()
    {
        isUp = circleUpBtn.GetComponent<CircleBtnController>().isUp;
        if (isUp && isCircleActive && Input.touchCount > 0 && Input.GetTouch(0).position.y < Screen.height * 0.2f && Input.GetTouch(0).phase == TouchPhase.Moved && Mathf.Abs(Input.touches[0].deltaPosition.x) >= Screen.width * 0.05f)
        {
            circleRotate();
        }
    }

    private void ActiveCircle()
    {
        isCircleActive = true;
    }

    private void AnimatorPause()
    {
        animator.SetFloat("Speed", 0);
    }

    public void circleRotate()
    {
        rotateDirecrion = -(Input.touches[0].deltaPosition.x / Mathf.Abs(Input.touches[0].deltaPosition.x));

        switch (GlobalUIController.btnIndex)
        {
            case (int)rotateIndexs.seal:
                if(rotateDirecrion > 0)
                {
                    animator.Play("circleL2C", 0);//L2C
                    GlobalUIController.btnIndex = (int)rotateIndexs.ar;
                    btnsChange();
                }
                break;

            case (int)rotateIndexs.ar:
                if (rotateDirecrion < 0)
                {
                    animator.Play("circleC2L", 0);//C2L
                    GlobalUIController.btnIndex = (int)rotateIndexs.seal;
                    btnsChange();
                }
                else
                {
                    animator.Play("circleC2R", 0);//C2R
                    GlobalUIController.btnIndex = (int)rotateIndexs.museum;
                    btnsChange();
                }
                break;

            case (int)rotateIndexs.museum:
                if (rotateDirecrion < 0)
                {
                    animator.Play("circleR2C", 0);//R2C
                    GlobalUIController.btnIndex = (int)rotateIndexs.ar;
                    btnsChange();
                }
                break;
        }

        Invoke("GuidencesChange", 0.5f);
    }

    private void btnsChange()
    {
        foreach (GameObject btn in btns)
        {
            btn.transform.parent.transform.parent.GetComponent<RectTransform>().Rotate(Vector3.forward, -rotateDirecrion * angleOfOnce);
            btn.GetComponent<Image>().color = new Color(255, 255, 255, 0.5f);
            btn.transform.Find("dot").GetComponent<Image>().color = new Color(255, 255, 255, 0.5f);
        }
        btns[GlobalUIController.btnIndex].GetComponent<Image>().color = new Color(255, 255, 255, 1);
        btns[GlobalUIController.btnIndex].transform.Find("dot").GetComponent<Image>().color = new Color(255, 255, 255, 1);

        switch (GlobalUIController.btnIndex)
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

        Invoke("ScenesChange", 0.2f);

        isCircleActive = false;
        Invoke("ActiveCircle", 0.2f);
    }

    private void GuidencesChange()
    {
        switch (GlobalUIController.btnIndex)
        {
            case (int)rotateIndexs.seal:
                if (!SealGuidence.activeSelf && !SealGuidenceLoaded)
                {
                    SealGuidence.SetActive(true);
                    SealGuidenceLoaded = true;
                }
                if (ARGuidence.activeSelf)
                {
                    ARGuidence.SetActive(false);
                }
                if (MuseumGuidence.activeSelf)
                {
                    MuseumGuidence.SetActive(false);
                }
                break;

            case (int)rotateIndexs.ar:
                if (SealGuidence.activeSelf)
                {
                    SealGuidence.SetActive(false);
                }
                if (!ARGuidence.activeSelf && !ARGuidenceLoaded)
                {
                    ARGuidence.SetActive(true);
                    ARGuidenceLoaded = true;
                }
                if (MuseumGuidence.activeSelf)
                {
                    MuseumGuidence.SetActive(false);
                }
                break;

            case (int)rotateIndexs.museum:
                if (SealGuidence.activeSelf)
                {
                    SealGuidence.SetActive(false);
                }
                if (ARGuidence.activeSelf)
                {
                    ARGuidence.SetActive(false);
                }
                if (!MuseumGuidence.activeSelf && !MuseumGuidenceLoaded)
                {
                    MuseumGuidence.SetActive(true);
                    MuseumGuidenceLoaded = true;
                }
                break;
        }
    }

    private void ScenesChange()
    {
        switch (GlobalUIController.btnIndex)
        {
            case (int)rotateIndexs.seal:
                if (SceneManager.GetSceneByName("SealScene").isLoaded == false)
                {
                    if (SceneManager.GetSceneByName("ARScene").isLoaded)
                    {
                        SceneManager.UnloadSceneAsync("ARScene");
                    }
                    if (SceneManager.GetSceneByName("MuseumScene").isLoaded)
                    {
                        SceneManager.UnloadSceneAsync("MuseumScene");
                    }
                    SceneManager.LoadSceneAsync("SealScene", LoadSceneMode.Additive).completed += _operation =>
                        SceneManager.SetActiveScene(SceneManager.GetSceneByName("SealScene"));
                }
                break;

            case (int)rotateIndexs.ar:
                if (SceneManager.GetSceneByName("ARScene").isLoaded == false)
                {
                    if (SceneManager.GetSceneByName("SealScene").isLoaded)
                    {
                        SceneManager.UnloadSceneAsync("SealScene");
                    } 
                    if (SceneManager.GetSceneByName("MuseumScene").isLoaded)
                    {
                        SceneManager.UnloadSceneAsync("MuseumScene");
                    }
                    SceneManager.LoadSceneAsync("ARScene", LoadSceneMode.Additive).completed += _operation =>
                        SceneManager.SetActiveScene(SceneManager.GetSceneByName("ARScene"));
                }
                break;

            case (int)rotateIndexs.museum:
                if (SceneManager.GetSceneByName("MuseumScene").isLoaded == false)
                {
                    if (SceneManager.GetSceneByName("ARScene").isLoaded)
                    {
                        SceneManager.UnloadSceneAsync("ARScene");
                    }
                    if (SceneManager.GetSceneByName("SealScene").isLoaded)
                    {
                        SceneManager.UnloadSceneAsync("SealScene");
                    }
                    SceneManager.LoadSceneAsync("MuseumScene", LoadSceneMode.Additive).completed += _operation =>
                        SceneManager.SetActiveScene(SceneManager.GetSceneByName("MuseumScene"));
                }
                break;
        }
    }
}
