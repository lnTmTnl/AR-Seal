using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class SealSelect : MonoBehaviour
{

    public Camera arCamera;
    public Text hintText;
    public List<GameObject> seals;
    public List<Material> materials;

    public GameObject modelViewport;
    public GameObject materialViewport;
    public GameObject introductionView;
    private AudioSource touchSound;

    private float distance = 2.0f;
    private bool isIntroduction = false;
    int clickedBtnIndex;

    public Button modelBtn;
    public Button materialBtn;

    public List<Button> modelButtons;
    public List<Button> materialButtons;

    public Color deepColor = new Color((float)132 / 255, (float)132 / 255, (float)132 / 255);
    public Color lightColor = new Color(1, 1, 1);
    public Material deepColorBtnMaterial;
    public Material lightColorBtnMaterial;

    private float Ping = 1;
    private bool IsStart = false;
    private float LastTime = 0;

    public  Vector3 getInsPosition()
    {
        Vector3 mMenu = arCamera.transform.forward.normalized * distance;
        Vector3 insPosition = arCamera.transform.position + mMenu;
        return insPosition;
    }

    public void DisplayModelViewport()
    {
        if (!modelViewport.activeSelf)
        {
            modelViewport.SetActive(true);
        }
        if (materialViewport.activeSelf)
        {
            materialViewport.SetActive(false);
        }

        modelBtn.transform.Find("Text").GetComponent<Text>().color = deepColor;
        modelBtn.transform.Find("Image").GetComponent<Image>().color = lightColor;
        modelBtn.GetComponent<Image>().material = lightColorBtnMaterial;
        materialBtn.transform.Find("Text").GetComponent<Text>().color = lightColor;
        materialBtn.transform.Find("Image").GetComponent<Image>().color = deepColor;
        materialBtn.GetComponent<Image>().material = deepColorBtnMaterial;
    }

    public void DisplayMaterialViewport()
    {
        if (modelViewport.activeSelf)
        {
            modelViewport.SetActive(false);
        }
        if (!materialViewport.activeSelf)
        {
            materialViewport.SetActive(true);
        }

        modelBtn.transform.Find("Text").GetComponent<Text>().color = lightColor;
        modelBtn.transform.Find("Image").GetComponent<Image>().color = deepColor;
        modelBtn.GetComponent<Image>().material = deepColorBtnMaterial;
        materialBtn.transform.Find("Text").GetComponent<Text>().color = deepColor;
        materialBtn.transform.Find("Image").GetComponent<Image>().color = lightColor;
        materialBtn.GetComponent<Image>().material = lightColorBtnMaterial;
    }

    public void onModelBtn(int i)
    {
        touchSound.Play(0);

        Material preBodyAndStatueMaterial = new Material(StepTwoController.prefabIns.transform.Find("statue").GetComponent<MeshRenderer>().material);
        Material preCurveMaterial = new Material(StepTwoController.prefabIns.transform.Find("body").transform.Find("curve").GetComponent<MeshRenderer>().material);
        Quaternion preRotation = StepTwoController.prefabIns.GetComponent<Transform>().rotation;
        Destroy(StepTwoController.prefabIns);
        StepTwoController.prefabIns = Instantiate(seals[i], getInsPosition(), preRotation);
        StepTwoController.prefabIns.transform.Find("body").Find("mainBox").GetComponent<MeshRenderer>().material = preBodyAndStatueMaterial;
        StepTwoController.prefabIns.transform.Find("statue").GetComponent<MeshRenderer>().material = preBodyAndStatueMaterial;
        StepTwoController.prefabIns.transform.Find("body").Find("curve").GetComponent<MeshRenderer>().material = preCurveMaterial;
        StepTwoController.prefabIns.AddComponent<ARAnchor>();

        PaintView.ChangeQuadTexture();
        GameObject.Find("Panel").GetComponent<PaintView>().SetBorder();
    }

    public void onMaterialBtn(int i)
    {
        touchSound.Play(0);

        StepTwoController.prefabIns.transform.Find("statue").GetComponent<MeshRenderer>().material = materials[i];
        StepTwoController.prefabIns.transform.Find("body").Find("mainBox").GetComponent<MeshRenderer>().material = materials[i];
        StepTwoController.prefabIns.transform.Find("body").Find("curve").GetComponent<MeshRenderer>().material = materials[i];
        
        PaintView.ChangeQuadTexture();
        GameObject.Find("Panel").GetComponent<PaintView>().SetBorder();
    }

    public void CloseIntroductionView()
    {
        introductionView.SetActive(false);
    }

    public void LongPressDown(int i)
    {
        IsStart = true;
        clickedBtnIndex = i;
        if (IsStart)
        {
            LastTime = Time.time;
            Debug.Log("长按开始");
        }
        else if (LastTime != 0)
        {
            LastTime = 0;
            Debug.Log("长按取消");
        }
    }

    public void LongPressUp()
    {
        IsStart = false;
        if (IsStart)
        {
            LastTime = Time.time;
            Debug.Log("长按开始");
        }
        else if (LastTime != 0)
        {
            LastTime = 0;
            Debug.Log("长按取消");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        touchSound = GetComponent<AudioSource>();
        modelBtn.onClick.AddListener(DisplayModelViewport);
        materialBtn.onClick.AddListener(DisplayMaterialViewport);

        int modelButtonsLen = modelButtons.Count;
        for (int i = 0; i < modelButtonsLen; i++)
        {
            int temp = i;

            modelButtons[i].onClick.AddListener(() => { onModelBtn(temp); });
        }

        int materialButtonsLen = materialButtons.Count;
        for(int i = 0; i < materialButtonsLen; i++)
        {
            int temp = i;

            materialButtons[i].onClick.AddListener(() => { onMaterialBtn(temp); });

            UnityAction<BaseEventData> callbackDown = new UnityAction<BaseEventData>(delegate { LongPressDown(temp); });
            EventTrigger.Entry entryDown = new EventTrigger.Entry();
            entryDown.eventID = EventTriggerType.PointerDown;
            entryDown.callback.AddListener(callbackDown);
            EventTrigger trigger = materialButtons[i].GetComponent<EventTrigger>();
            trigger.triggers.Add(entryDown);

            UnityAction<BaseEventData> callbackUp = new UnityAction<BaseEventData>(delegate { LongPressUp(); });
            EventTrigger.Entry entryUp = new EventTrigger.Entry();
            entryUp.eventID = EventTriggerType.PointerUp;
            entryUp.callback.AddListener(callbackUp);
            trigger.triggers.Add(entryUp);
        }

        materialViewport.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //touchSound.Play(0);
        if (!isIntroduction)
        {
            for(int i = 0; i < StepTwoController.introductions.Count; i++)
            {
                StepTwoController.introductions[i].Image = materialButtons[i].GetComponent<Image>().sprite;
            }
            isIntroduction = true;
        }

        if (IsStart && Ping > 0 && LastTime > 0 && Time.time - LastTime > Ping)
        {
            Debug.Log("长按触发");
            StepTwoController.introductions[clickedBtnIndex].ShowOnUI(introductionView);
            IsStart = false;
            LastTime = 0;
        }

        if (GameObject.Find("materialBtn").transform.Find("Image").GetComponent<Image>().color == lightColor)
        {
            if (!hintText.gameObject.activeSelf)
            {
                hintText.gameObject.SetActive(true);
            }
        }
        else
        {
            if (hintText.gameObject.activeSelf)
            {
                hintText.gameObject.SetActive(false);
            }
        }
    }
}
