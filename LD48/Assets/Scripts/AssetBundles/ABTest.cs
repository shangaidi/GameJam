using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //GameObject obj1 = ABMgr.Instance.LoadRes("model", "Cube",typeof(GameObject)) as GameObject;
        //obj1.transform.position = Vector3.up;

        //GameObject obj2 = ABMgr.Instance.LoadRes<GameObject>("model", "Cube");
        //obj2.transform.position = Vector3.up;


        ABMgr.Instance.LoadResAsync("model", "Cube", (obj1) => 
        {
            (obj1 as GameObject).transform.position = -Vector3.up;
        });


        ABMgr.Instance.LoadResAsync("model", "Cube",typeof(GameObject), (obj2) =>
        {
            (obj2 as GameObject).transform.position = -Vector3.up;
        });

        ABMgr.Instance.LoadResAsync<GameObject>("model", "Cube", (obj3) =>
        {
            obj3.transform.position = -Vector3.up;
        });
        //AssetBundle ab = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + "model");
        ////如果有依赖包 一定要把依赖包也加载 

        ////利用主包获取依赖信息
        ////加载主包 
        //AssetBundle abMain = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + "PC");
        ////加载主包中的固定文件
        //AssetBundleManifest abMainfest = abMain.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        ////从固定文件中 得到依赖信息
        //string[] strs = abMainfest.GetAllDependencies("model");
        ////得到依赖包名
        //for (int i = 0; i < strs.Length; i++)
        //{
        //    Debug.Log(strs[i]);
        //    AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + strs[i]);
        //}
        //GameObject obj = ab.LoadAsset("Cube", typeof(GameObject)) as GameObject;
        //Instantiate(obj);

        #region 同步加载
        /*
        //同步加载
        //加载AB 包
        AssetBundle ab = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + "model");
        //加载AB包中的资源  (用泛型 防止重名)
        //GameObject obj = ab.LoadAsset<GameObject>("Cube");
        GameObject obj = ab.LoadAsset("Cube", typeof(GameObject)) as GameObject;
        Instantiate(obj);
        */
        //卸载所有加载的AB包  会把AB包加载的资源卸载   true 会卸掉场内所有已经加载的
        //AssetBundle.UnloadAllAssetBundles(true);
        //ab.Unload(true);
        #endregion

        //异步加载
        //StartCoroutine(LoadABRes("model","Cube")) ;
    }


    #region 异步加载
    //IEnumerator LoadABRes(string ABName,string ResNmae)
    //{
    //    //加载AB包
    //    AssetBundleCreateRequest abcr = AssetBundle.LoadFromFileAsync(Application.streamingAssetsPath + "/" + ABName);
    //    yield return abcr;
    //    //加载资源
    //    AssetBundleRequest abq = abcr.assetBundle.LoadAssetAsync(ResNmae, typeof(GameObject));
    //    yield return abq;

    //    Instantiate(abq.asset as GameObject);
    //} 
    #endregion

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            AssetBundle.UnloadAllAssetBundles(false);
        }
    }
}
