using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SquareBtnController : MonoBehaviour
{
    private Animator animator;
    public GameObject UISquare;

    [HideInInspector]
    public bool isRight = true;

    public 

    // Start is called before the first frame update
    void Start()
    {
        animator = transform.Find("right").GetComponent<Animator>();
        GetComponent<Button>().onClick.AddListener(Click);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Click()
    {
        if (isRight)
        {
            HideSquare();
        }
        else
        {
            AppearSquare();
        }
        isRight = !isRight;
    }

    private void HideSquare()
    {
        UISquare.GetComponent<Animator>().Play("SquareHide", 0);
        animator.Play("squareBtnR2L", 0);
    }

    private void AppearSquare()
    {
        UISquare.GetComponent<Animator>().Play("SquareAppear", 0);
        animator.Play("squareBtnL2R", 0);
    }
}
