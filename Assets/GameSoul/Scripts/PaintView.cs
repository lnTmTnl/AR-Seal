//-----------------------------------------------------------------------
// <copyright file="PaintView.cs" company="Codingworks Game Development">
//     Copyright (c) codingworks. All rights reserved.
// </copyright>
// <author> codingworks </author>
// <email> coding2233@163.com </email>
// <time> 2017-12-10 </time>
//-----------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class PaintView : MonoBehaviour
{
    #region 属性


    public static Texture2D wendi;
    public Button btn;
    public Text saveAssignText;

    private static int TextureWidth = 200;
    private static int TextureHight = 200;
    private List<Line> lines;
    private Line tmpLine;
    private List<Vector2> tmpPoints;
    public static GameObject quad;
    public GameObject borderImage;
    public Texture2D border;
    private bool isBorder = false;

    public Color deepColor = new Color((float)132 / 255, (float)132 / 255, (float)132 / 255);
    public Color lightColor = new Color(1, 1, 1);

    //绘图shader&material
    [SerializeField]
    private Shader _paintBrushShader;
    private Material _paintBrushMat;
    //private Material last_paintBrushMat;
    //清理renderTexture的shader&material
    [SerializeField]
    private Shader _clearBrushShader;
    private Material _clearBrushMat;
    //默认笔刷RawImage
    [SerializeField]
    private RawImage _defaultBrushRawImage;
    //默认笔刷&笔刷合集
    [SerializeField]
    private Texture _defaultBrushTex;
    //renderTexture
    private static RenderTexture _renderTex;
    private static RenderTexture last_renderTex;
    private List<RenderTexture> renderTextures;
    //默认笔刷RawImage
    [SerializeField]
    private Image _defaultColorImage;
    //绘画的画布
    [SerializeField]
    private RawImage _paintCanvas;
    //笔刷的默认颜色&颜色合集
    [SerializeField]
    private Color _defaultColor;
    private Color clearColor;
    //笔刷大小的slider
    private Text _brushSizeText;
    //笔刷的大小
    private float _brushSize;
    //屏幕的宽高
    private int _screenWidth;
    private int _screenHeight;

    private float drawAreaWidth;
    private float drawAreaHeight;
    //笔刷的间隔大小
    private float _brushLerpSize;
    //默认上一次点的位置
    private Vector2 _lastPoint;

    //public Text text;
    #endregion

    void Start()
    {
        btn.onClick.AddListener(saveBtn);
        InitData();

    }

    private void Update()
    {
        if(quad == null)
        {
            quad = GameObject.Find("Quad");
            quad.GetComponent<MeshRenderer>().material.mainTexture = null;
        }

        if(wendi == null)
        {
            wendi = TextureToTexture2D(StepTwoController.prefabIns.transform.Find("body").transform.Find("mainBox").GetComponent<MeshRenderer>().material.mainTexture);
            quad.transform.GetComponent<MeshRenderer>().material.mainTexture = TwoTexMix(RenderTo2D(_renderTex), wendi);
        }
        
    }


    #region 外部接口

    public void SetBrushSize(float size)
    {
        _brushSize = size;
        _paintBrushMat.SetFloat("_Size", _brushSize);
    }

    public void SetBrushTexture(Texture texture)
    {
        _defaultBrushTex = texture;
        _paintBrushMat.SetTexture("_BrushTex", _defaultBrushTex);
        _defaultBrushRawImage.texture = _defaultBrushTex;
    }

    public void SetBrushColor(Color color)
    {
        _defaultColor = color;
        _paintBrushMat.SetColor("_Color", _defaultColor);
        _defaultColorImage.color = _defaultColor;
    }
    /// <summary>
    /// 选择颜色
    /// </summary>
    /// <param name="image"></param>
    /*public void SelectColor(Image image)
    {
        SetBrushColor(image.color);
    }*/
    /// <summary>
    /// 选择笔刷
    /// </summary>
    /// <param name="rawImage"></param>
    /*public void SelectBrush(RawImage rawImage)
    {
        SetBrushTexture(rawImage.texture);
    }*/
    /// <summary>
    /// 设置笔刷大小
    /// </summary>
    /// <param name="value"></param>
    public void BrushSizeChanged(Slider slider)
    {
        //  float value = slider.maxValue + slider.minValue - slider.value;
        SetBrushSize(Remap(slider.value, 100.0f, 30.0f));
        if (_brushSizeText == null)
        {
            _brushSizeText = slider.transform.Find("Background/Text").GetComponent<Text>();
        }
        _brushSizeText.text = slider.value.ToString("f2");
    }

    /// <summary>
    /// 拖拽
    /// </summary>
    public void DragUpdate()
    {
        if (_renderTex && _paintBrushMat)
        {

            if (Input.GetMouseButton(0))
            {
                LerpPaint(Input.mousePosition);
                tmpPoints.Add(Input.mousePosition);
            }

        }
    }
    /// <summary>
    /// 拖拽结束
    /// </summary>
    public void DragEnd()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Graphics.CopyTexture(_renderTex, last_renderTex);
            renderTextures.Add(last_renderTex);
            quad.GetComponent<MeshRenderer>().material.mainTexture = last_renderTex;

            _lastPoint = Vector2.zero;
            tmpLine = new Line(_brushSize, tmpPoints);
            lines.Add(tmpLine);
            tmpPoints = new List<Vector2>();

            //Undo();
            //Debug.Log(lines.Count);
            saveBtn();
            SetBorder();
        }
    }

    #endregion

    #region 内部函数

    //初始化数据
    void InitData()
    {
        lines = new List<Line>();
        tmpPoints = new List<Vector2>();
        clearColor = new Color(1, 1, 1, 1);
        renderTextures = new List<RenderTexture>();

        _brushSize = 100.0f;
        _brushLerpSize = (_defaultBrushTex.width + _defaultBrushTex.height) / 2.0f / _brushSize;
        _lastPoint = Vector2.zero;

        if (_paintBrushMat == null)
        {
            UpdateBrushMaterial();
        }
        if (_clearBrushMat == null)
            _clearBrushMat = new Material(_clearBrushShader);
        if (_renderTex == null)
        {
            _screenWidth = Screen.width;
            _screenHeight = Screen.height;



            drawAreaWidth = GetComponent<RectTransform>().rect.width;
            drawAreaHeight = GetComponent<RectTransform>().rect.height;

            _renderTex = RenderTexture.GetTemporary(TextureWidth, TextureHight, 24);
            _paintCanvas.texture = _renderTex;
        }
        Graphics.Blit(null, _renderTex, _clearBrushMat);
        
        
    }

    //更新笔刷材质
    private void UpdateBrushMaterial()
    {
        _paintBrushMat = new Material(_paintBrushShader);
        _paintBrushMat.SetTexture("_BrushTex", _defaultBrushTex);
        _paintBrushMat.SetColor("_Color", _defaultColor);
        //_paintBrushMat.mainTexture = _defaultBrushTex;
        _paintBrushMat.SetFloat("_Size", _brushSize);
    }

    //插点
    private void LerpPaint(Vector2 point)
    {
        Paint(point);

        if (_lastPoint == Vector2.zero)
        {
            _lastPoint = point;
            return;
        }

        float dis = Vector2.Distance(point, _lastPoint);
        if (dis > _brushLerpSize)
        {
            Vector2 dir = (point - _lastPoint).normalized;
            int num = (int)(dis / _brushLerpSize);
            for (int i = 0; i < num; i++)
            {
                Vector2 newPoint = _lastPoint + dir * (i + 1) * _brushLerpSize;
                Paint(newPoint);
            }
        }
        _lastPoint = point;
    }

    //画点
    private void Paint(Vector2 point)
    {
        //if (point.x < 0 || point.x > _screenWidth || point.y < 0 || point.y > _screenHeight)
        if (!IsPointerOverGameObject(point))
            return;

        float bottomOffset = (GameObject.Find("Draw").GetComponent<RectTransform>().rect.height - drawAreaHeight) / 2 + GameObject.Find("drawArea").GetComponent<RectTransform>().localPosition.y + GameObject.Find("Draw").GetComponent<RectTransform>().offsetMin.y;

        Vector2 uv = new Vector2((point.x - ((float)_screenWidth - drawAreaWidth) / 2) / drawAreaWidth,
            (point.y - bottomOffset) / drawAreaHeight);

        //GameObject.Find("Draw").GetComponent<RectTransform>().rect.position.y
        //text.text = (bottomOffset).ToString();

        /*last_paintBrushMat = new Material(_paintBrushShader);
        last_paintBrushMat.SetTexture("_BrushTex", _defaultBrushTex);
        last_paintBrushMat.SetColor("_Color", _defaultColor);
        last_paintBrushMat.SetFloat("_Size", _brushSize);
        last_paintBrushMat.SetVector("_UV", _paintBrushMat.GetVector("_UV"));*/

        _paintBrushMat.SetVector("_UV", uv);
        last_renderTex = new RenderTexture(_renderTex);
        Graphics.Blit(_renderTex, _renderTex, _paintBrushMat);




    }
    /// <summary>
    /// 重映射  默认  value 为1-100
    /// </summary>
    /// <param name="value"></param>
    /// <param name="maxValue"></param>
    /// <param name="minValue"></param>
    /// <returns></returns>
    private float Remap(float value, float startValue, float enValue)
    {
        float returnValue = (value - 1.0f) / (100.0f - 1.0f);
        returnValue = (enValue - startValue) * returnValue + startValue;
        return returnValue;
    }

    #endregion

    public void Undo()
    {
        if (renderTextures.Count > 1)
        {
            Graphics.CopyTexture(renderTextures[renderTextures.Count - 2], _renderTex);
            renderTextures.RemoveAt(renderTextures.Count - 2);
            Graphics.CopyTexture(_renderTex, renderTextures[renderTextures.Count - 1]);
            lines.RemoveAt(lines.Count - 1);
            quad.transform.GetComponent<MeshRenderer>().material.mainTexture = TwoTexMix(RenderTo2D(last_renderTex), wendi);
            SetBorder();
        }
        else if (renderTextures.Count == 1)
        {
            InitData();
            SetAndRemoveBorder();
        }
        /*if(lines.Count > 0)
        {
            for (int i = 0; i < lines[lines.Count - 1].Points.Count; i++)
            {
                _paintBrushMat.SetFloat("_Size", lines[lines.Count - 1].Size - 1f);
                _paintBrushMat.SetColor("_Color", clearColor);
                LerpPaint(lines[lines.Count - 1].Points[i]);
            }

            _lastPoint = Vector2.zero;

            lines.RemoveAt(lines.Count - 1);

            _paintBrushMat.SetColor("_Color", _defaultColor);
            foreach (Line line in lines)
            {
                _paintBrushMat.SetFloat("_Size", line.Size);
                foreach(Vector2 point in line.Points)
                {
                    LerpPaint(point);
                }
                _lastPoint = Vector2.zero;
            }

            _paintBrushMat.SetColor("_Color", _defaultColor);
            _paintBrushMat.SetFloat("_Size", _brushSize);
        }*/
    }

    public void Clear()
    {
        InitData();
        quad.transform.GetComponent<MeshRenderer>().material.mainTexture = TwoTexMix(RenderTo2D(_renderTex), wendi);
        if (isBorder)
        {
            SetAndRemoveBorder();
        }
    }

    private bool IsPointerOverGameObject(Vector2 mousePosition)
    {
        //创建一个点击事件
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = mousePosition;
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        //向点击位置发射一条射线，检测是否点击UI
        EventSystem.current.RaycastAll(eventData, raycastResults);
        if (raycastResults.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private static Texture2D RenderTo2D(RenderTexture renderTexture)
    {
        int width = renderTexture.width;
        int height = renderTexture.height;
        Texture2D texture2D = new Texture2D(width, height, TextureFormat.ARGB32, false);
        RenderTexture.active = renderTexture;
        texture2D.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        texture2D.Apply();
        return texture2D;
    }

    public static Texture2D TextureToTexture2D(Texture texture)
    {
        Texture2D texture2D = new Texture2D(texture.width, texture.height, TextureFormat.RGBA32, false);
        RenderTexture currentRT = RenderTexture.active;
        RenderTexture renderTexture = RenderTexture.GetTemporary(texture.width, texture.height, 32);
        Graphics.Blit(texture, renderTexture);

        RenderTexture.active = renderTexture;
        texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture2D.Apply();

        RenderTexture.active = currentRT;
        RenderTexture.ReleaseTemporary(renderTexture);

        return texture2D;
    }


        //t1 _renderTexture
        //t2 wenli
    public static Texture2D TwoTexMix(Texture2D t1, Texture2D t2)
    {
        Texture2D t3 = new Texture2D(TextureWidth, TextureHight);
        
        for (int x = 0; x < t1.width; x++)
        {
            for (int y = 0; y < t1.height; y++)

            {
                if (t1.GetPixel(x, y) == Color.white)
                {
                    t3.SetPixel(x, y, t2.GetPixel(x, y));
                    //t2.SetPixel(t2.width - x, t2.height - y, t1.GetPixel(x, y));
                }
                else
                {
                    t3.SetPixel(x, y, new Color((float)182/255, (float)30 /255, (float)30 /255));
                }

            }

        }
        t3.Apply();
        return t3;
    }

    public static Texture2D black2Red(RenderTexture renderTexture)
    {
        Texture2D t = new Texture2D(TextureWidth, TextureHight);
        Texture2D tt = RenderTo2D(renderTexture);
        for (int x = 0; x < t.width; x++)
        {
            for (int y = 0; y < t.height; y++)

            {
                if (tt.GetPixel(x, y) == Color.white)
                {
                    t.SetPixel(x, y, tt.GetPixel(x, y));
                    //t2.SetPixel(t2.width - x, t2.height - y, t1.GetPixel(x, y));
                }
                else
                {
                    t.SetPixel(x, y, new Color((float)182 / 255, (float)30 / 255, (float)30 / 255));
                }

            }

        }
        t.Apply();
        return t;
    }


    private static void saveBtn()
    {
        quad.transform.GetComponent<MeshRenderer>().material.mainTexture = TwoTexMix(RenderTo2D(_renderTex), wendi);
        StepTwoController.prefabIns.transform.Find("body").transform.Find("curve").GetComponent<MeshRenderer>().material.mainTexture = quad.transform.GetComponent<MeshRenderer>().material.mainTexture;
        StepTwoController.yinPlane.GetComponent<MeshRenderer>().material.mainTexture = black2Red(_renderTex);
    }

    public static void ChangeQuadTexture()
    {
        wendi = TextureToTexture2D(StepTwoController.prefabIns.transform.Find("body").transform.Find("mainBox").GetComponent<MeshRenderer>().material.mainTexture);
        saveBtn();
    }

    public void SetBorder()
    {
        if (isBorder)
        {
            quad.GetComponent<MeshRenderer>().material.mainTexture = PaintView.TwoTexMix(border, PaintView.TextureToTexture2D(quad.GetComponent<MeshRenderer>().material.mainTexture));
            StepTwoController.prefabIns.transform.Find("body").transform.Find("curve").GetComponent<MeshRenderer>().material.mainTexture = quad.transform.GetComponent<MeshRenderer>().material.mainTexture;
            StepTwoController.yinPlane.GetComponent<MeshRenderer>().material.mainTexture = TwoTexMix(border, black2Red(_renderTex));
        }
    }

    public void SetAndRemoveBorder()
    {
        if (!isBorder)
        {
            isBorder = true;
            borderImage.SetActive(true);
            GameObject.Find("borderBtn").GetComponent<Image>().color = lightColor;
            GameObject.Find("borderBtn").transform.Find("Text").GetComponent<Text>().color = deepColor;
            SetBorder();
        }
        else
        {
            isBorder = false;
            borderImage.SetActive(false);
            GameObject.Find("borderBtn").GetComponent<Image>().color = deepColor;
            GameObject.Find("borderBtn").transform.Find("Text").GetComponent<Text>().color = lightColor;
            saveBtn();
        }
    }

    public void SaveImages(Texture2D texture)
    {
        string path = "/sdcard/DCIM/SaveImage/seals"; //设置图片保存到设备的目录
        DateTime now = DateTime.Now;
        string fileName = string.Format("{0}{1}{2}{3}{4}{5}", now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
        //#if UNITY_ANDROID
        //#endif
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        string savePath = path + "/" + fileName + ".png";
        File.WriteAllBytes(savePath, texture.EncodeToPNG());

        saveAssignText.gameObject.SetActive(true);
        saveAssignText.GetComponent<Text>().text = "保存成功 DCIM/SaveImage/seals/" + fileName + ".png";
        Invoke("HideSaveAssignText", 2);
    }

    private void HideSaveAssignText()
    {
        saveAssignText.gameObject.SetActive(false);
    }

    public void OnSaveSealBtn()
    {
        SaveImages(TextureToTexture2D(StepTwoController.yinPlane.GetComponent<MeshRenderer>().material.mainTexture));
    }
}
