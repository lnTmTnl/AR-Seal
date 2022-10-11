using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SquareControl : MonoBehaviour
{
    private Animator floatSquareAnimator;
    private float moveDirecrion;
    //private int btnIndex;
    private bool isSquareActive;

    public GameObject floatSquare;

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
        floatSquareAnimator = floatSquare.GetComponent<Animator>();
        isSquareActive = true;
        //GlobalUIController.btnIndex = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPointerOverGameObject(Input.GetTouch(0).position) && isSquareActive && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved && Mathf.Abs(Input.touches[0].deltaPosition.y) >= Screen.height * 0.05f)
        {
            squareMove();
        }
    }

    private void ActiveSquare()
    {
        isSquareActive = true;
    }

    private void squareMove()
    {
        moveDirecrion = -(Input.touches[0].deltaPosition.y / Mathf.Abs(Input.touches[0].deltaPosition.y));

        switch (GlobalUIController.btnIndex)
        {
            case (int)rotateIndexs.seal:
                if (moveDirecrion > 0)
                {
                    floatSquareAnimator.Play("squareT2C", 0);//L2C
                    GlobalUIController.btnIndex = (int)rotateIndexs.ar;
                }
                break;

            case (int)rotateIndexs.ar:
                if (moveDirecrion < 0)
                {
                    floatSquareAnimator.Play("squareC2T", 0);//C2L
                    GlobalUIController.btnIndex = (int)rotateIndexs.seal;
                }
                else
                {
                    floatSquareAnimator.Play("squareC2B", 0);//C2R
                    GlobalUIController.btnIndex = (int)rotateIndexs.museum;
                }
                break;

            case (int)rotateIndexs.museum:
                if (moveDirecrion < 0)
                {
                    floatSquareAnimator.Play("squareB2C", 0);//R2C
                    GlobalUIController.btnIndex = (int)rotateIndexs.ar;
                }
                break;
        }

        Invoke("ScenesChange", 0.2f);

        isSquareActive = false;
        Invoke("ActiveSquare", 0.2f);

        //Invoke("GuidencesChange", 0.5f);
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

    private bool IsPointerOverGameObject(Vector2 mousePosition)
    {
        //创建一个点击事件
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = mousePosition;
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        //向点击位置发射一条射线，检测是否点击UI
        EventSystem.current.RaycastAll(eventData, raycastResults);
        if (raycastResults.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
