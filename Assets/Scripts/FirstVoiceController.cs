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

        // ������ȫ������
        AudioConfiguration audio_config = AudioSettings.GetConfiguration();
        //....�м���������
        AudioSettings.Reset(audio_config);
        //�����������д����Ȼ�����Ǿ�ִ����Ч�� 
        // this.s.Play();//�����������ֲ����ӳٲ��ţ��������ƿ��������Ϊ���1�룬����X2λ���2��
        //this.s.PlayDelayed(5);//�ӳ�5�벥��
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