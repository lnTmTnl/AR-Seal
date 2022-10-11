using UnityEngine;
using System.Collections;

public class YinRotate : MonoBehaviour
{

    private Vector3 startFingerPos;
    private Vector3 nowFingerPos;
    private float xMoveDistance;
    private float yMoveDistance;
    private int backValue = 0;
    public GameObject obj;
    void Update()
    {
        if (Input.touchCount !=1 || Input.GetTouch(0).position.y < Screen.height / 3)
        {
            return;
        }

        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            //Debug.Log("======��ʼ����=====");  
            startFingerPos = Input.GetTouch(0).position;
        }

        nowFingerPos = Input.GetTouch(0).position;

        if ((Input.GetTouch(0).phase == TouchPhase.Stationary) || (Input.GetTouch(0).phase == TouchPhase.Ended))
        {
            startFingerPos = nowFingerPos;
            //Debug.Log("======�ͷŴ���=====");  
            return;
        }
        //          if (Input.GetTouch(0).phase == TouchPhase.Ended) {  
        //                
        //          }  
        if (startFingerPos == nowFingerPos)
        {
            return;
        }
        xMoveDistance = Mathf.Abs(nowFingerPos.x - startFingerPos.x);
        //yMoveDistance = Mathf.Abs(nowFingerPos.y - startFingerPos.y);

        if (nowFingerPos.x - startFingerPos.x > 0)
        {
            //debug.log("=======����x�Ḻ�����ƶ�=====");  
            backValue = -1; //����x�Ḻ�����ƶ�  
        }
        else
        {
            //debug.log("=======����x���������ƶ�=====");  
            backValue = 1; //����x���������ƶ�  
        }

        if (backValue == -1)
        {
            obj.transform.Rotate(Vector3.up * -1 * Time.deltaTime * 260, Space.World);
        }
        else if (backValue == 1)
        {
            obj.transform.Rotate(Vector3.up * Time.deltaTime * 260, Space.World);
        }



        //    if (xMoveDistance > yMoveDistance)
        //    {
        //        if (nowFingerPos.x - startFingerPos.x > 0)
        //        {
        //            //Debug.Log("=======����X�Ḻ�����ƶ�=====");  
        //            backValue = -1; //����X�Ḻ�����ƶ�  
        //        }
        //        else
        //        {
        //            //Debug.Log("=======����X���������ƶ�=====");  
        //            backValue = 1; //����X���������ƶ�  
        //        }
        //    }
        //    else
        //    {
        //        if (nowFingerPos.y - startFingerPos.y > 0)
        //        {
        //            //Debug.Log("=======����Y���������ƶ�=====");  
        //            backValue = -2; //����Y�Ḻ�����ƶ�  
        //        }
        //        else
        //        {
        //            //Debug.Log("=======����Y�Ḻ�����ƶ�=====");  
        //            backValue = 2; //����Y���������ƶ�  
        //        }

        //    }
        //    if (backValue == -1)
        //    {
        //        obj.transform.Rotate(Vector3.up * -1 * Time.deltaTime * 260, Space.World);
        //    }
        //    else if (backValue == 1)
        //    {
        //        obj.transform.Rotate(Vector3.up * Time.deltaTime * 260, Space.World);
        //    }
        //    else if (backValue == -2)
        //    {
        //        obj.transform.Rotate(Vector3.right * Time.deltaTime * 260, Space.World);
        //    }
        //    else if (backValue == 2)
        //    {
        //        obj.transform.Rotate(Vector3.right * -1 * Time.deltaTime * 260, Space.World);
        //    }

    }
}