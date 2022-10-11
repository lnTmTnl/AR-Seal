using UnityEngine;
using System.Collections;

public class audio_test : MonoBehaviour
{

    public AudioSource s;
    // Use this for initialization
    void Start()
    {
        // 声音的全局设置
        AudioConfiguration audio_config = AudioSettings.GetConfiguration();
        //....中间设置内容
        AudioSettings.Reset(audio_config);//设置完后再放回去
                                          // end 
                                          //上面两句必须写，不然下面那句执行无效，我也不知道为啥　　　　 
                                          // this.s.Play();//如果里面加数字不是延迟播放，而是类似快进，码率为快进1秒，码率X2位快进2秒
        //this.s.PlayDelayed(5);//延迟5秒播放
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {//鼠标左键按下
            if (this.s.isPlaying)
            {
                this.s.Pause();
            }
            else
            {
                this.s.Play();
            }
        }
       
    }
}