using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FirstVoiceController : MonoBehaviour
{
    //public Text t;
    public GameObject plane;
    public GameObject TouchPromptPanel;

    public AudioSource prompt;

    public AudioSource touchPrompt;
    // Use this for initialization
    void Start()
    {
        Invoke("voicePlay", 1);
        plane.SetActive(false);

        // 声音的全局设置
        AudioConfiguration audio_config = AudioSettings.GetConfiguration();
        //....中间设置内容
        AudioSettings.Reset(audio_config);
        //上面两句必须写，不然下面那句执行无效　 
        // this.s.Play();//如果里面加数字不是延迟播放，而是类似快进，码率为快进1秒，码率X2位快进2秒
        //this.s.PlayDelayed(5);//延迟5秒播放
    }

    void voicePlay()
    {
        this.prompt.Play();
        plane.SetActive(true);
    }


    // Update is called once per frame
    void Update()
    {

        if ((Input.touchCount == 1 && plane.activeSelf) || (Input.touchCount == 1 && TouchPromptPanel.activeSelf))        {
            plane.SetActive(false);
            TouchPromptPanel.SetActive(false);
           
            touchPrompt.Play();
        }




    }
}