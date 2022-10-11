using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARRaycastManager))]
public class StepTwoController : MonoBehaviour
{
    public Camera arCamera;
    public GameObject prefab;
    public GameObject prefabPlane;
    //public GameObject panelSeletMoudle;

    public static GameObject prefabIns;//ѡ��ģ��
    public static GameObject yinPlane;
    public static List<SealIntroductions> introductions;

    //public Button testBtn;
    private float distance = 2.0f;


    //ʱ����ӡ����ʾ����Ļ�м�
    public Vector3 getInsPosition()
    {
        Vector3 mMenu = arCamera.transform.forward.normalized * distance;
        Vector3 insPosition = arCamera.transform.position + mMenu;
        return insPosition;
    }


    public void prefabInit()
    {
        introductions = new List<SealIntroductions>();

        prefabIns = Instantiate(prefab, getInsPosition(), Quaternion.Euler(-90, 90, 90));
        prefabIns.AddComponent<ARAnchor>();

        yinPlane = Instantiate(prefabPlane);
        yinPlane.SetActive(false);
    }

    public void InitIntroductions()
    {
        introductions.Add(new SealIntroductions("��ɽʯ��ˮ��ʯ�����Զ�",
            "��ɽʯ����ڸ���ʡ�����б�����ɽ�����������Ԫĩ��������ӡ�ġ�ˮ��ʯ��������ɽ��1.5����Ŀ�ͷ�����ڿ������ˮ�ḻ����ʯ�����ʴ������͸��״�����й����׳ơ���ʯ����ˮ��ʯ��͸���ȸߡ�����Ө����Ϊ��Ʒ�������Ժ켰��ɫ��Ϊ��������ɫʤ��ɫ�����Զ�����ˮ��������͸��룬�ֳ���֬��ʯ���Ե��ر�����֬�������ơ�ˮ��״�ƻ����ƣ����������ԣ�����ˮ����Ө����ʯɫ���ѩ�ס�",
            "����", "�ϸ�", "����", "����",
            "��ɽʯ��������ϰ�µ���Ҫԭ��֮һ��������Ե���ѶȲ�����ʹ���ʵ�һ�����ɽʯ��������ϰ�¼۸񲻸ߣ��ʺ���������ѡ�á���Ʒ�ʵ���ɽ���Զ��۸�ϸߣ�������һ�����������ղ�������ѡ�á�"));

        introductions.Add(new SealIntroductions("��ɽʯ��ˮ��ʯ", "��ɽʯ����ڸ���ʡ�����б�����ɽ�����������Ԫĩ��������ӡ�ġ�ˮ��ʯ��������ɽ��1.5����Ŀ�ͷ�����ڿ������ˮ�ḻ����ʯ�����ʴ������͸��״�����й����׳ơ���ʯ����ˮ��ʯ��͸���ȸߡ�����Ө����Ϊ��Ʒ�������Ժ켰��ɫ��Ϊ��������ɫʤ��ɫ��",
            "����", "�ϸ�", "����", "����",
            "��ɽʯ��������ϰ�µ���Ҫԭ��֮һ��������Ե���ѶȲ�����ʹ���ʵ�һ�����ɽʯ��������ϰ�¼۸񲻸ߣ��ʺ���������ѡ�á���Ʒ�ʵ�ˮ��ʯ�۸�ϸߣ�������һ�����������ղ�������ѡ�á�"));

        introductions.Add(new SealIntroductions("����ʯ", "����ʯ,�����㽭ʡ�����أ��ɿ�ʱ�ھ��Լ1.4���ꡣ����ʯ�����类����ӡ�ĵ�ʯ�ϡ���������ӡ�˴�����:��(����)�Ե�ʯ���˲��������¡�����������ʯ����ȥ��������������伹���ī����ʹ��������֮�����Ƕ�ʯ֮��ʼ������,�޴��ķ��ӡ���",
            "�ϵ�", "�е�", "��", "�ϵ�",
            "����ʯ�������ڵ�̡��۸�ϵ͵��ص㣬��������������ϰ�£��ʺ���������ѡ�á����Ǹ�Ʒ�ʵ�����ʯ�۸���Խϸߣ�������һ�����������ղ�������ѡ�á�"));

        introductions.Add(new SealIntroductions("����ʯ������ʯ�����Żư׶�", "����ʯ���������ɹų���а��������Ǭ������ľ������������ɽһ������ʯ�������٣�Ӳ�Ƚϵͣ����������͸���ȸߡ�����ս��ʱ�ڣ��ձ������߷��ְ���ʯ�ļ�ֵ,����͹����ɲ��ӹ���ӡʯ�������ձ�����ʱ��Ϊ���ɹ�ʯ�������ֶ�ʯָ���Ƿ�����ʯ��͸���ȽϺã���������͸����΢͸����������ɰ��Ҳ�������Ի�ɫΪ���ĵ����ߣ������ǰ���ʯ�в�����ᡢƷ������һ�ࡣ",
            "�ϵ�", "�ϸ�", "����", "����",
            "���ֶ�ʯ�������ʯ�ȼ۸��һЩ������Ѷ�Ҳ��΢��һЩ�����������ϰ����Խ��٣�������Ʒ�ʸߵİ��ֶ�ʯ�۸񼫸ߡ������ʺϽ��װ�����ѡ�õ�̡�"));

        introductions.Add(new SealIntroductions("����ʯ������Ѫʯ", "����ʯ�㽭ʡ�ٰ��в���������ɽ����ӡʯ֮ͨ�ơ�����ʯ�γ���һ�ڶ���ǰ�������������ڻ�ɽ�緢��������к�����γɺ�����ɰ�Ŀ��Ｏ���塣��ɫ���ּ�����ɰ�����壬��ơ���Ѫ����������ɰ�ļ�Ϊ��ͨ����ʯ��ɫ��������ϡ���ʵ����ܣ��Ͷ�ɬ�����ٺ�ɰ�������ʡ�������Ѫʯ���й����е����ʯ�����м�Ѫ����ʺ�ɫ�ʺ��������������ʣ��������鱦���ͬ���������ӣ��ԡ�������֮���������⡣",
            "�ϴ�", "�ϸ�", "����", "��",
            "������Ѫʯ�Ǽ�Ѫʯ�еľ�Ʒ���г��۸��ձ�ϸߣ���������������ѡ�ã������Ϊ�����Ұ����ղ����ʯ�ϵ�׭�����ѡ�á�"));

        introductions.Add(new SealIntroductions("����ʯ", "����ʯ,�����㽭ʡ�����أ��ɿ�ʱ�ھ��Լ1.4���ꡣ����ʯ�����类����ӡ�ĵ�ʯ�ϡ���������ӡ�˴�����:��(����)�Ե�ʯ���˲��������¡�����������ʯ����ȥ��������������伹���ī����ʹ��������֮�����Ƕ�ʯ֮��ʼ������,�޴��ķ��ӡ���",
            "�ϵ�", "�е�", "��", "�ϵ�",
            "����ʯ�������ڵ�̡��۸�ϵ͵��ص㣬��������������ϰ�£��ʺ���������ѡ�á����Ǹ�Ʒ�ʵ�����ʯ�۸���Խϸߣ�������һ�����������ղ�������ѡ�á�"));

        introductions.Add(new SealIntroductions("ܽ����", "ܽ������һ�ֵ��ۺ�ɫ������õ��ɫ�Ŀ�״ʯӢ���壬�ֳơ�ǾޱʯӢ������΢�����̺���Ԫ�ض���ɫ������ĥ������è�ۡ����ǹ⡱ЧӦ������͸���Ƚϸߣ��ɲ���ǿ��͸���ǹ⡣��è��ЧӦ��,��ܽ��ʯè��;���ǹ�ЧӦ��,���Ϊ�ǹ�ܽ��ʯ�����ʵ�ܽ��ʯ����ɫ�������,������ȫ͸�����ڹ���,���ʵ�ܽ��ʯ���ڰ���,ܽ��ʯҪ����ۺ�ɫ�������ã�͸���������ơ������ʣ��ſ��������ʡ�",
            "��", "��", "��", "��",
            "ܽ����������Ҫ�ɷ���ʯӢ�����Ӳ�Ƚϴ󣬵�������ȽϷ�ʱ������Ʒ�ʽϵ͵�ܽ����һ����ְ�ɫ���۸���Ե�һЩ����Ʒ�ʵ�ܽ������һ����ַۺ�ɫ����ɫԽ��۸�Խ�ߡ��������ܽ����۸���ҵ���Ѷȴ��ʺ�����׭�̰����ߡ���������Ҳ��ѡ��Ʒ�ʲ����ߵ�ܽ�������Ͻ��е�̡�"));

        introductions.Add(new SealIntroductions("����ʯ������Ѫʯ", "���ּ�Ѫʯ�������ɹŰ������죬�ش����˰���֧�����εĳ�³�°ӡ���������´�����ʵ�ϸ��͸���Ⱥ�Ӳ�Ƚϸߣ�������ɰ��������Ѫ״������Ѫɫ������Ѫ˿�ͻ��⣬�����Զ�����ơ���Ѫʯ�ϵĺ�ɫ����̬�ɷ�ΪƬ�졢���졢�ߺ졢��㡢�ź�����֡�",
            "�ϸ�", "�ϸ�", "����", "�ϸ�",
            "���ּ�Ѫʯ�����������Ѫʯ��ô����Լ۱������˵��һЩ���ʺϱȽ�ϲ����Ѫʯ�Ķ����������ֲ���������ߣ��Լ�Ԥ�����޵�׭�̰�����ѡ�á�"));

    }

    void Start()
    {
        prefabInit();
        InitIntroductions();
        this.GetComponent<PressSeal>().enabled = false;
        this.GetComponent<ARPlaneManager>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        //ʱ�̸����Զ���ӡ��λ��
        if (prefabIns.activeSelf)
        {
            prefabIns.transform.position = getInsPosition();
        }


        //if(SealUISituation.situation == SealUISituation.situations.style && !GameObject.Find("circleBtn").GetComponent<CircleBtnController>().isUp)
        //{
        //    prefabIns.GetComponent<MyRotate>().enabled = false;
        //}else if(SealUISituation.situation == SealUISituation.situations.style && GameObject.Find("circleBtn").GetComponent<CircleBtnController>().isUp)
        //{
        //    prefabIns.GetComponent<MyRotate>().enabled = true;
        //}


        //�����Զ���ӡ��ģʽ
        if(SealUISituation.situation == SealUISituation.situations.style)
        {
            prefabIns.SetActive(true);
            yinPlane.SetActive(false);
            this.GetComponent<PressSeal>().enabled = false;
            this.GetComponent<ARPlaneManager>().enabled = false;
        }
        //���ڵ��ģʽ
        if(SealUISituation.situation == SealUISituation.situations.carve)
        {
            prefabIns.SetActive(false);
            yinPlane.SetActive(false);
            this.GetComponent<PressSeal>().enabled = false ;
            this.GetComponent<ARPlaneManager>().enabled = false;
        }
        //������ӡģʽ
        if(SealUISituation.situation == SealUISituation.situations.press)
        {
            prefabIns.SetActive(false);
            this.GetComponent<PressSeal>().enabled = true;
            this.GetComponent<ARPlaneManager>().enabled = true;
        }
    }
}
