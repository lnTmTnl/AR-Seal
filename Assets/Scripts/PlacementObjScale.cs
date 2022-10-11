using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlacementObjScale : MonoBehaviour
{
    [SerializeField]
    private Slider scaleSlider;
    public Text scaleSliderText;

    [SerializeField]
    private Slider rotateSlider;
    public Text rotateSliderText;

    [SerializeField]
    private Slider xSlider;
    public Text xSliderText;

    [SerializeField]
    private Slider ySlider;
    public Text ySliderText;

    [SerializeField]
    private Slider zSlider;
    public Text zSliderText;

    private bool isChecked = false;

    private void Awake()
    {
        scaleSlider.onValueChanged.AddListener(ScaleChanged);
        rotateSlider.onValueChanged.AddListener(RotateChanged);
        xSlider.onValueChanged.AddListener(XChanged);
        ySlider.onValueChanged.AddListener(YChanged);
        zSlider.onValueChanged.AddListener(ZChanged);
    }

    private void ScaleChanged(float newValue)
    {
        if (PlacementController.prefabIns != null && PlacementController.isExisted)
        {
            PlacementController.prefabIns.transform.localScale = Vector3.one * newValue;
            scaleSliderText.text = scaleSlider.value.ToString();
        }
    }

    private void RotateChanged(float newValue)
    {
        if (PlacementController.prefabIns != null && PlacementController.isExisted)
        {
            //float y = PlacementController.prefabIns.transform.rotation.y;
            float x = PlacementController.prefabIns.GetComponent<Transform>().position.x;
            float y = PlacementController.prefabIns.GetComponent<Transform>().position.y;
            float z = PlacementController.prefabIns.GetComponent<Transform>().position.z;

            PlacementController.prefabIns.transform.localRotation = Quaternion.Euler(Vector3.up * newValue);
            rotateSliderText.text = rotateSlider.value.ToString();

            xSliderText.text = PlacementController.prefabIns.GetComponent<Transform>().position.x.ToString();
            ySliderText.text = PlacementController.prefabIns.GetComponent<Transform>().position.y.ToString();
            zSliderText.text = PlacementController.prefabIns.GetComponent<Transform>().position.z.ToString();

            //PlacementController.prefabIns.transform.position = new Vector3(0, 0, 0);
        }
    }

    private void XChanged(float newValue)
    {
        if (PlacementController.prefabIns != null && PlacementController.isExisted)
        {
            float x = PlacementController.prefabIns.GetComponent<Transform>().position.x;
            float y = PlacementController.prefabIns.GetComponent<Transform>().position.y;
            float z = PlacementController.prefabIns.GetComponent<Transform>().position.z;
            PlacementController.prefabIns.transform.position = new Vector3(newValue, y, z);
            xSliderText.text = xSlider.value.ToString();
        }
    }

    private void YChanged(float newValue)
    {
        if (PlacementController.prefabIns != null && PlacementController.isExisted)
        {
            float x = PlacementController.prefabIns.GetComponent<Transform>().position.x;
            float y = PlacementController.prefabIns.GetComponent<Transform>().position.y;
            float z = PlacementController.prefabIns.GetComponent<Transform>().position.z;
            PlacementController.prefabIns.transform.position = new Vector3(x, newValue, z);
            ySliderText.text = ySlider.value.ToString();
        }
    }

    private void ZChanged(float newValue)
    {
        if (PlacementController.prefabIns != null && PlacementController.isExisted)
        {
            float x = PlacementController.prefabIns.GetComponent<Transform>().position.x;
            float y = PlacementController.prefabIns.GetComponent<Transform>().position.y;
            float z = PlacementController.prefabIns.GetComponent<Transform>().position.z;
            PlacementController.prefabIns.transform.position = new Vector3(x, y, newValue);
            zSliderText.text = zSlider.value.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isChecked && PlacementController.prefabIns != null && PlacementController.isExisted)
        {
            scaleSliderText.text = PlacementController.prefabIns.transform.localScale.ToString();
            rotateSliderText.text = PlacementController.prefabIns.transform.rotation.y.ToString();
            xSliderText.text = PlacementController.prefabIns.GetComponent<Transform>().position.x.ToString();
            ySliderText.text = PlacementController.prefabIns.GetComponent<Transform>().position.y.ToString();
            zSliderText.text = PlacementController.prefabIns.GetComponent<Transform>().position.z.ToString();

            /*scaleSlider.value = PlacementController.prefabIns.transform.localScale.x;
            rotateSlider.value = PlacementController.prefabIns.transform.localRotation.y;
            xSlider.value = PlacementController.prefabIns.GetComponent<Transform>().position.x;
            ySlider.value = PlacementController.prefabIns.GetComponent<Transform>().position.y;
            zSlider.value = PlacementController.prefabIns.GetComponent<Transform>().position.z;*/

            isChecked = true;
        }
    }
}
