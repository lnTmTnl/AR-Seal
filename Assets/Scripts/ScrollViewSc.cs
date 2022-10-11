using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScrollViewSc : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    //获取组件
    ScrollRect rect;

    private float[] posArray = new float[] { 0, 0.33f, 0.66f, 1.0f };

    private float targetPos;

    private bool isDrag = false;

    private int index = 0;

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDrag = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDrag = false;
        Vector2 pos = rect.normalizedPosition;
        float x = Mathf.Abs(pos.x - posArray[0]);
        for (int i = 0; i < 4; i++)
        {
            float temp = Mathf.Abs(pos.x - posArray[i]);

            if (temp <= x)
            {
                x = temp;
                index = i;


            }

        }
        targetPos = posArray[index];
    }

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<ScrollRect>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDrag)
        {
            rect.horizontalNormalizedPosition = Mathf.Lerp(rect.horizontalNormalizedPosition, targetPos, Time.deltaTime * 15);
        }
    }
}
