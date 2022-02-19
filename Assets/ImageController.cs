using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ImageController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(HttpConnect()); 
    }

    IEnumerator HttpConnect()
    {
        string url = "https://joytas.net/php/man.jpg";
        UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url);
        yield return uwr.SendWebRequest();//SendWebRequest Get通信をしている
        if (uwr.isHttpError || uwr.isNetworkError)
        {
            Debug.Log(uwr.error);
        }
        else
        {
            Texture texture = DownloadHandlerTexture.GetContent(uwr);

            Sprite sp = Sprite.Create(
                (Texture2D)texture,//spriteはアンカーの概念がある Canvasで使うには中心が必要
                new Rect(0, 0, texture.width, texture.height),
                new Vector2(0.5f, 0.5f));//sprite 画像の中心
            sp.name = "man";

            Image image = GetComponent<Image>();

            image.rectTransform.sizeDelta = new Vector2(//sizeDelta rectTransform はwidth,heightを設定できる
                texture.width, texture.height);

            image.sprite = sp;
        }
    }
}
