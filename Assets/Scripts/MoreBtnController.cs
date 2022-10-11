using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoreBtnController : MonoBehaviour
{
    public GameObject list;

    private bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(MoreClicked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MoreClicked()
    {
        isActive = !isActive;
        list.SetActive(isActive);
    }
}
