using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveSeal
{
    public static IEnumerator SaveImages(Texture2D texture)
    {
        string path = Application.persistentDataPath;
//#if UNITY_ANDROID
        path = "/sdcard/DCIM/SaveImage"; //设置图片保存到设备的目录.
//#endif
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        string savePath = path + "/" + texture.name + ".png";
        File.WriteAllBytes(savePath, texture.EncodeToPNG());

        Debug.Log(path);
        yield return new WaitForEndOfFrame();
    }
}
