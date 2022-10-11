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

    public static GameObject prefabIns;//选择模型
    public static GameObject yinPlane;
    public static List<SealIntroductions> introductions;

    //public Button testBtn;
    private float distance = 2.0f;


    //时刻让印章显示在屏幕中间
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
        introductions.Add(new SealIntroductions("寿山石—水坑石—鱼脑冻",
            "寿山石因产于福建省福州市北郊寿山乡而得名。在元末明初用作印材。水坑石产于离寿山乡1.5公里的坑头，由于矿体地下水丰富，矿石受其浸蚀，多显透明状，富有光泽，俗称“冻石”。水坑石以透明度高、肌理莹洁者为上品，其中以红及黄色最为罕见，明色胜暗色。鱼脑冻产于水晶洞，半透半浑，又称羊脂玉，石质显得特别凝腻脂润，隐含云、水团状纹或波浪纹，如煮熟鱼脑，不如水晶冻莹澈，石色亦非雪白。",
            "适中", "较高", "适中", "适中",
            "寿山石是生产练习章的主要原料之一。总体而言雕刻难度不大，且使用质地一般的寿山石制作的练习章价格不高，适合新手入门选用。高品质的寿山鱼脑冻价格较高，建议有一定基础且有收藏需求者选用。"));

        introductions.Add(new SealIntroductions("寿山石—水坑石", "寿山石因产于福建省福州市北郊寿山乡而得名。在元末明初用作印材。水坑石产于离寿山乡1.5公里的坑头，由于矿体地下水丰富，矿石受其浸蚀，多显透明状，富有光泽，俗称“冻石”。水坑石以透明度高、肌理莹洁者为上品，其中以红及黄色最为罕见，明色胜暗色。",
            "适中", "较高", "适中", "适中",
            "寿山石是生产练习章的主要原料之一。总体而言雕刻难度不大，且使用质地一般的寿山石制作的练习章价格不高，适合新手入门选用。高品质的水坑石价格较高，建议有一定基础且有收藏需求者选用。"));

        introductions.Add(new SealIntroductions("青田石", "青田石,产于浙江省青田县，成矿时期距今约1.4亿年。青田石是最早被用作印材的石料。周亮工《印人传》载:“(文彭)自得石后，乃不复作牙章。铁中乃索其石满百去，半以属公，半浼公落墨，而使何主臣镌之，于是冻石之名始见于世,艳传四方矣。”",
            "较低", "中等", "易", "较低",
            "青田石由于易于雕刻、价格较低等特点，经常用于生产练习章，适合新手入门选用。但是高品质的青田石价格相对较高，建议有一定基础且有收藏需求者选用。"));

        introductions.Add(new SealIntroductions("巴林石——冻石——古黄白冻", "巴林石，产于内蒙古赤峰市巴林右旗查乾沐沦苏木西北的雅玛吐山一带。该石含硅量少，硬度较低，含铝量多而透明度高。抗日战争时期，日本侵略者发现巴林石的价值,便雇劳工开采并加工成印石等运往日本，当时称为“蒙古石”。巴林冻石指的是凡巴林石中透明度较好，即具有亚透明到微透明，不含辰砂，也不具有以黄色为主的地子者，它们是巴林石中产量最丰、品种最多的一类。",
            "较低", "较高", "适中", "适中",
            "巴林冻石相比青田石等价格高一些，雕刻难度也稍微高一些，因此用作练习章相对较少；尤其是品质高的巴林冻石价格极高。总体适合进阶爱好者选用雕刻。"));

        introductions.Add(new SealIntroductions("昌化石——鸡血石", "昌化石浙江省临安市昌化镇玉岩山所产印石之通称。昌化石形成于一亿多年前的中生代，由于火山喷发，熔岩流泻，逐渐形成含纯辰砂的矿物集合体。红色部分即纯辰砂集合体，贯称“鸡血”；不含辰砂的即为普通昌化石。色纯无杂者稀贵，质地纤密，韧而涩刀，少含砂丁及杂质。昌化鸡血石是中国特有的珍贵宝石，具有鸡血般的鲜红色彩和美玉般的天生丽质，历来与珠宝翡翠同样受人珍视，以“国宝”之誉驰名中外。",
            "较大", "较高", "较难", "高",
            "昌化鸡血石是鸡血石中的精品，市场价格普遍较高，不适于新手入门选用，建议较为资深且爱好收藏珍贵石料的篆刻玩家选用。"));

        introductions.Add(new SealIntroductions("青田石", "青田石,产于浙江省青田县，成矿时期距今约1.4亿年。青田石是最早被用作印材的石料。周亮工《印人传》载:“(文彭)自得石后，乃不复作牙章。铁中乃索其石满百去，半以属公，半浼公落墨，而使何主臣镌之，于是冻石之名始见于世,艳传四方矣。”",
            "较低", "中等", "易", "较低",
            "青田石由于易于雕刻、价格较低等特点，经常用于生产练习章，适合新手入门选用。但是高品质的青田石价格相对较高，建议有一定基础且有收藏需求者选用。"));

        introductions.Add(new SealIntroductions("芙蓉玉", "芙蓉玉是一种淡粉红色——深玫瑰色的块状石英晶体，又称“蔷薇石英”。因含微量的锰和钛元素而致色，可切磨产生“猫眼”或“星光”效应。因其透明度较高，可产生强的透射星光。具猫眼效应的,称芙蓉石猫眼;具星光效应的,则称为星光芙蓉石。优质的芙蓉石不仅色深而美观,而且完全透明。在国外,优质的芙蓉石产于巴西,芙蓉石要求其粉红色愈深愈好，透明，无裂纹、无杂质，才可列入优质。",
            "高", "高", "难", "高",
            "芙蓉玉由于主要成分是石英，因此硬度较大，雕刻起来比较费时费力。品质较低的芙蓉玉一般呈现白色，价格相对低一些；高品质的芙蓉玉则一般呈现粉红色，颜色越深价格越高。总体而言芙蓉玉价格高且雕刻难度大，适合资深篆刻爱好者。不过新手也可选购品质并不高的芙蓉玉章料进行雕刻。"));

        introductions.Add(new SealIntroductions("巴林石——鸡血石", "巴林鸡血石产于内蒙古巴林右旗，地处大兴安岭支脉西段的朝鲁吐坝、乌兰坝南麓。其质地细润，透明度好硬度较高，不含“砂钉”，其血状大多呈猪血色并带有血丝和黄肉，以灵性多变著称。鸡血石上的红色按形态可分为片红、条红、斑红、点点、团红等数种。",
            "较高", "较高", "适中", "较高",
            "巴林鸡血石并不像昌化鸡血石那么珍贵，性价比相对来说高一些，适合比较喜爱鸡血石的独特纹理，而又并非资深爱好者，以及预算有限的篆刻爱好者选用。"));

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

        //时刻更新自定义印章位置
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


        //处于自定义印章模式
        if(SealUISituation.situation == SealUISituation.situations.style)
        {
            prefabIns.SetActive(true);
            yinPlane.SetActive(false);
            this.GetComponent<PressSeal>().enabled = false;
            this.GetComponent<ARPlaneManager>().enabled = false;
        }
        //处于雕刻模式
        if(SealUISituation.situation == SealUISituation.situations.carve)
        {
            prefabIns.SetActive(false);
            yinPlane.SetActive(false);
            this.GetComponent<PressSeal>().enabled = false ;
            this.GetComponent<ARPlaneManager>().enabled = false;
        }
        //处于拓印模式
        if(SealUISituation.situation == SealUISituation.situations.press)
        {
            prefabIns.SetActive(false);
            this.GetComponent<PressSeal>().enabled = true;
            this.GetComponent<ARPlaneManager>().enabled = true;
        }
    }
}
