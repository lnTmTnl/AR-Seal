using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollController : MonoBehaviour
{
    public Vector2 scrollPosition = Vector2.zero;
    public float scrollVelocity = 0f;
    public float timeTouchPhaseEnded = 0f;
    public float inertiaDuration = 0.5f;

    public float contentLR;
    public float contentSpacingX;
    public float cellSizeX;
    public int contentCount;
    public float totalContentWidth;

    public Vector2 lastDeltaPos;

    // Use this for initialization
    void Start()
    {
        contentLR = GetComponent<GridLayoutGroup>().padding.left;
        contentSpacingX = GetComponent<GridLayoutGroup>().spacing.x;
        cellSizeX = GetComponent<GridLayoutGroup>().cellSize.x;
        contentCount = transform.childCount;

        totalContentWidth = contentLR * 2 + contentSpacingX * (contentCount - 1) + cellSizeX * contentCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            scrollPosition.x = 0;
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                if (IsPointerOverGameObject(Input.GetTouch(0).position) && Input.GetTouch(0).position.y < 297)
                    scrollPosition.x = Input.GetTouch(0).deltaPosition.x;
                //lastDeltaPos = Input.GetTouch(0).deltaPosition;
            }
            /*else if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                if (Mathf.Abs(lastDeltaPos.x) > 20.0f)
                {
                    scrollVelocity = (int)(lastDeltaPos.x * 0.5 / Input.GetTouch(0).deltaTime);
                }
                timeTouchPhaseEnded = Time.time;
            }*/
            if(scrollPosition.x != 0)
            {
                if ((scrollPosition.x < 0 && (Screen.width - (scrollPosition.x + GetComponent<RectTransform>().localPosition.x) < totalContentWidth)) ||
                (scrollPosition.x > 0 && (GetComponent<RectTransform>().localPosition.x + scrollPosition.x) < 0))
                {
                    GetComponent<RectTransform>().Translate(scrollPosition.x, 0, 0);
                }
            }
        }
        /*else
        {
            if (scrollVelocity != 0.0f)
            {
                // slow down
                float t = (Time.time - timeTouchPhaseEnded) / inertiaDuration;
                float frameVelocity = Mathf.Lerp(scrollVelocity, 0, t);
                scrollPosition.x += frameVelocity * Time.deltaTime;

                if (t >= inertiaDuration)
                    scrollVelocity = 0;
            }
        }*/
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
