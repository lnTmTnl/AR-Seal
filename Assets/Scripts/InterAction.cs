using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterAction : MonoBehaviour
{
    public GameObject panel1;
    public Button closeBtn;

    public Button ZhanguoBtn;
    public Button JiyuBtn;
    public Button SongliaoBtn;
    public Button WenrenBtn;
    public Button HandaiBtn;
    public Button QinyinBtn;
    public Button WeijinBtn;

    public GameObject SVzhanguo;
    public GameObject SVjiyu;
    public GameObject SVsongliao;
    public GameObject SVwenren;
    public GameObject SVhandai;
    public GameObject SVqinyin;
    public GameObject SVweijin;

    public Sprite ZhanguoTex1;
    public Sprite JiyuTex1;
    public Sprite SongliaoTex1;
    public Sprite WenrenTex1;
    public Sprite HandaiTex1;
    public Sprite QinyinTex1;
    public Sprite WeijinTex1;

    public Sprite ZhanguoTex2;
    public Sprite JiyuTex2;
    public Sprite SongliaoTex2;
    public Sprite WenrenTex2;
    public Sprite HandaiTex2;
    public Sprite QinyinTex2;
    public Sprite WeijinTex2;

    public AudioSource touchPrompt;

    // Start is called before the first frame update
    void Start()
    {
        AudioConfiguration audio_config = AudioSettings.GetConfiguration();
        AudioSettings.Reset(audio_config);

        closeBtn.onClick.AddListener(onCloseBtn);
        ZhanguoBtn.onClick.AddListener(onZhanguoBtn);
        JiyuBtn.onClick.AddListener(onJiyuBtn);
        SongliaoBtn.onClick.AddListener(onSongliaoBtn);
        WenrenBtn.onClick.AddListener(onWenrenBtn);
        HandaiBtn.onClick.AddListener(onHandaiBtn);
        QinyinBtn.onClick.AddListener(onQinyinBtn);
        WeijinBtn.onClick.AddListener(onWeijinBtn);
    }

    private void onCloseBtn()
    {
        touchPrompt.Play();
        panel1.SetActive(false);
    }


    private void onZhanguoBtn()
    {
        touchPrompt.Play();
        hiddenAllSV();
        SVzhanguo.SetActive(true);
        noSelectZi();
        ZhanguoBtn.image.sprite = ZhanguoTex1;
    }

    private void onJiyuBtn()
    {
        touchPrompt.Play();
        hiddenAllSV();
        SVjiyu.SetActive(true);
        noSelectZi();
        JiyuBtn.image.sprite = JiyuTex1;
    }
    private void onSongliaoBtn()
    {
        touchPrompt.Play();
        hiddenAllSV();
        SVsongliao.SetActive(true);
        noSelectZi();
        SongliaoBtn.image.sprite = SongliaoTex1;
    }
    private void onWenrenBtn()
    {
        touchPrompt.Play();
        hiddenAllSV();
        SVwenren.SetActive(true);
        noSelectZi();
        WenrenBtn.image.sprite = WenrenTex1;
    }
    private void onHandaiBtn()
    {
        touchPrompt.Play();
        hiddenAllSV();
        SVhandai.SetActive(true);
        noSelectZi();
        HandaiBtn.image.sprite = HandaiTex1;
    }
    private void onQinyinBtn()
    {
        touchPrompt.Play();
        hiddenAllSV();
        SVqinyin.SetActive(true);
        noSelectZi();
        QinyinBtn.image.sprite = QinyinTex1;
    }
    private void onWeijinBtn()
    {
        touchPrompt.Play();
        hiddenAllSV();
        SVweijin.SetActive(true);
        noSelectZi();
        WeijinBtn.image.sprite = WeijinTex1;
    }

    private void noSelectZi()
    {
        ZhanguoBtn.image.sprite = ZhanguoTex2;
        JiyuBtn.image.sprite = JiyuTex2;
        SongliaoBtn.image.sprite = SongliaoTex2;
        WenrenBtn.image.sprite = WenrenTex2;
        HandaiBtn.image.sprite = HandaiTex2;
        QinyinBtn.image.sprite = QinyinTex2;
        WeijinBtn.image.sprite = WeijinTex2;
    }

    private void hiddenAllSV()
    {
        SVzhanguo.SetActive(false);
        SVjiyu.SetActive(false);
        SVsongliao.SetActive(false);
        SVwenren.SetActive(false);
        SVhandai.SetActive(false);
        SVqinyin.SetActive(false);
        SVweijin.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
