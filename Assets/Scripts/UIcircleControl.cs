using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIcircleControl : MonoBehaviour
{
    public GameObject circle;

    private Animator animator;
    private float circleHeight;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        circleHeight = circle.GetComponent<RectTransform>().rect.height;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
