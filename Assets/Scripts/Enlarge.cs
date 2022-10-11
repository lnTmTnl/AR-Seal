using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enlarge : MonoBehaviour
{
    Vector2 oldPos1;
    Vector2 oldPos2;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 2)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved)
            {
                Vector2 temPos1 = Input.GetTouch(0).position;
                Vector2 temPos2 = Input.GetTouch(1).position;

                if (isEnlarge(oldPos1, oldPos2, temPos1, temPos2))
                {
                    float oldScale = transform.localScale.x;
                    float newScale = oldScale * 1.025f;
                    transform.localScale = new Vector3(newScale, newScale, newScale);
                }
                else
                {
                    float oldScale = transform.localScale.x;
                    float newScale = oldScale / 1.025f;
                    transform.localScale = new Vector3(newScale, newScale, newScale);
                }

                oldPos1 = temPos1;
                oldPos2 = temPos2;
            }
        }
    }

    //≈–∂œ ÷ ∆
    bool isEnlarge(Vector2 oPl, Vector2 oP2, Vector2 nP1, Vector2 nP2)
    {
        float length1 = Mathf.Sqrt((oPl.x - oP2.x) * (oPl.x - oP2.x) + (oPl.y - oP2.y) * (oPl.y - oP2.y));
        float length2 = Mathf.Sqrt((nP1.x - nP2.x) * (nP1.x - nP2.x) + (nP1.y - nP2.y) * (nP1.y - nP2.y));

        if (length1 < length2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
