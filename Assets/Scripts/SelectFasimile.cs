using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectFasimile : MonoBehaviour
{
    [SerializeField]
    private Image fasimileImg;
    public List<Button> btns;
    public GameObject quad;
    private bool isFasimile = false;
    private int selectedFasimileIndex = -1;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < btns.Count; i++)
        {
            int temp = i;

            btns[i].onClick.AddListener(()=> { ChangeFasimileImg(temp); });
        }
        /*btns[0].onClick.AddListener(() => { ChangeFasimileImg(0); });
        btns[1].onClick.AddListener(() => { ChangeFasimileImg(1); });
        btns[2].onClick.AddListener(() => { ChangeFasimileImg(2); });
        btns[3].onClick.AddListener(() => { ChangeFasimileImg(3); });
        btns[4].onClick.AddListener(() => { ChangeFasimileImg(4); });*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeFasimileImg(int i)
    {
        if (!isFasimile)
        {
            fasimileImg.sprite = btns[i].GetComponent<Image>().sprite;
            selectedFasimileIndex = i;
            isFasimile = true;
        }
        else
        {
            if(i != selectedFasimileIndex)
            {
                fasimileImg.sprite = btns[i].GetComponent<Image>().sprite;
                selectedFasimileIndex = i;
            }
            else
            {
                fasimileImg.sprite = null;
                selectedFasimileIndex = -1;
                isFasimile = false;
            }
        }
    }

    
}
