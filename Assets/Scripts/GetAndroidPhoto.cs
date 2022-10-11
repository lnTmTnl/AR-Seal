using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.Video;
using UnityEngine.Android;

public class GetAndroidPhoto : MonoBehaviour
{

    public Button button;
    public RawImage rawImage;

    void Start()
    {
        button.onClick.AddListener(OpenLibery);
        Permission.RequestUserPermission(Permission.ExternalStorageRead);
        Permission.RequestUserPermission(Permission.ExternalStorageWrite);
    }

    private void OpenLibery()
    {
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("TakePhoto", Application.persistentDataPath);
    }

    public void GetPhoto(string path)
    {
        Debug.Log("android give path ==> " + path);
        FileGetTex(path);
    }

    private void FileGetTex(string path)
    {
        FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
        fileStream.Seek(0, SeekOrigin.Begin);
        byte[] bye = new byte[fileStream.Length];
        fileStream.Read(bye, 0, bye.Length);
        fileStream.Close();

        Texture2D texture2D = new Texture2D((int)rawImage.rectTransform.rect.width, (int)rawImage.rectTransform.rect.height);
        texture2D.LoadImage(bye);
        rawImage.texture = texture2D;
    }
    public IEnumerator DownloadTexture(string url, RawImage rawImage)//这些都可以
    {
        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture("file://" + url))
        {
            yield return request.SendWebRequest();

            if (request.error == null)
            {
                Texture2D texture = (request.downloadHandler as DownloadHandlerTexture).texture;
                rawImage.texture = texture;
            }
        }
    }
}

