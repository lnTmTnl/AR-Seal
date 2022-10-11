using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoTextureMix : MonoBehaviour
{

    public Texture2D t1;
    public Texture2D t2; //µ×²¿ÎÆÀí

    // Start is called before the first frame update
    void Start()
    {
        for (int x = 0; x < t1.width; x++)
        {
            for (int y = 0; y < t1.height; y++)

            {
                if(t1.GetPixel(x,y) != Color.white)
                {
                    t2.SetPixel(t2.width - x, t2.height - y, t1.GetPixel(x, y));
                }
                
            }

        }
        t2.Apply();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
