using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadModels : MonoBehaviour
{

    [SerializeField]
    GameObject LoadingCanvas, MainCanvas;
    [SerializeField]
    Text PersentageText;
    [SerializeField]
    Slider ProgressSlider;

    [SerializeField]
    string assetBundleLink = "https://drive.google.com/uc?export=download&id=1WPJTJ3Ji2KWlB344ueVf9RiReq78AQQs";

    [SerializeField]
    string prefabName;

    [SerializeField]
    Player player;

    bool clearCache = false;
    private GameObject placementARObject = null;
    private AssetBundle assetBundle = null;
    private RuntimeAnimatorController animObject;

    private void Awake()
    {
        MainCanvas.SetActive(false);
        Caching.compressionEnabled = false;

        if (clearCache)
        {
            Caching.ClearCache();
        }
        StartCoroutine(DownloadAndLoad());
        
    }
    private IEnumerator DownloadAndLoad()
    {
        while (!Caching.ready)
        {
            yield return null;
        }
        yield return GetBundle();
        if (!assetBundle)
        {
            Debug.Log("Bundle Failed to Load");
            yield break;
        }
        animObject = assetBundle.LoadAsset<RuntimeAnimatorController>(prefabName);
        ARDebugManager.Instance.LogInfo($"{animObject}");
        player.AnimationController(animObject);
    }

    private IEnumerator GetBundle()
    {
        WWW request = WWW.LoadFromCacheOrDownload(assetBundleLink, 0);
        while (!request.isDone)
        {
            ProgressSlider.value = request.progress;
            string persentateTemp = "" + request.progress * 100;
            string[] strArray = persentateTemp.Split(char.Parse("."));
            PersentageText.text = "Loading..." + strArray[0] + "%";
            yield return null;
        }
        if(request.error == null)
        {
            assetBundle = request.assetBundle;
            LoadingCanvas.SetActive(false);
            MainCanvas.SetActive(true);
            ARDebugManager.Instance.LogInfo($"{assetBundle.name}");
            ARDebugManager.Instance.LogInfo($"{assetBundleLink}");
            Debug.Log("Success!!!");
        }
        else
        {
            Debug.Log("Error"+request.error);
        }
        yield return null;
    }
}
