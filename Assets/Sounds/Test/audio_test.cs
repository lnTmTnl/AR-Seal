using UnityEngine;
using System.Collections;

public class audio_test : MonoBehaviour
{

    public AudioSource s;
    // Use this for initialization
    void Start()
    {
        // ������ȫ������
        AudioConfiguration audio_config = AudioSettings.GetConfiguration();
        //....�м���������
        AudioSettings.Reset(audio_config);//��������ٷŻ�ȥ
                                          // end 
                                          //�����������д����Ȼ�����Ǿ�ִ����Ч����Ҳ��֪��Ϊɶ�������� 
                                          // this.s.Play();//�����������ֲ����ӳٲ��ţ��������ƿ��������Ϊ���1�룬����X2λ���2��
        //this.s.PlayDelayed(5);//�ӳ�5�벥��
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {//����������
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