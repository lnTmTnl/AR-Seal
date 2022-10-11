using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestThird : MonoBehaviour
{
    public GameObject obj;
    public Button btn;

    // Start is called before the first frame update
    void Start()
    {
        obj.SetActive(false);
        btn.onClick.AddListener(onBtn);
    }

    public void onBtn()
    {
        if (obj.activeSelf)
        {
            obj.SetActive(false);
        }
        else
        {
            obj.SetActive(true);
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
