﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ABMgr : Singleton<ABMgr>
{
    //AB包管理器的目的是让外部更方便的进行资源加载

    //存储主包信息
    private AssetBundle mainAB = null;
    //主包的配置文件
    private AssetBundleManifest mainfest = null;

    //AB包不能够复制加载 复制加载会报错
    //字典 用字典来存储 加载过的AB包
    public Dictionary<string, AssetBundle> abDic = new Dictionary<string, AssetBundle>();

    //AB包存放路径 方便修改
    private string PathUrl
    {
        get
        {
            return Application.streamingAssetsPath + "/";
        }
    }
    private string MainABName
    {
        get
        {
#if UNITY_IOS
            return "IOS";
#elif UNITY_ANDROID
            return "Android";
#else
            return "PC";
#endif
        }
    }

    /// <summary>
    /// 加载AB包
    /// </summary>
    /// <param name="abname"></param>
    public void LoadAB(string abName)
    {
        //加载AB包
        if (mainAB == null)
        {
            mainAB = AssetBundle.LoadFromFile(PathUrl + MainABName);
            mainfest = mainAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        }
        //我们要获取依赖包相关信息
        AssetBundle ab = null;
        string[] strs = mainfest.GetAllDependencies(abName);
        for (int i = 0; i < strs.Length; i++)
        {
            //判断包是否被加载过
            if (!abDic.ContainsKey(strs[i]))
            {
                ab = AssetBundle.LoadFromFile(PathUrl + strs[i]);
                abDic.Add(strs[i], ab);
            }
        }
        //加载资源来源包(主包)
        //如果没加载再加载
        if (!abDic.ContainsKey(abName))
        {
            ab = AssetBundle.LoadFromFile(PathUrl + abName);
            abDic.Add(abName, ab);
        }
    }
    //同步加载 不指定类型
    public Object LoadRes(string abName, string resName)
    {
        //加载AB包
        LoadAB(abName);
        //外部加载资源，判断类型是不是GameObject
        //如果是，直接实例化返回给外部
        Object obj = abDic[abName].LoadAsset(resName);
        if (obj is GameObject)
            return Instantiate(obj);
        else
            return obj;
    }
    //同步加载 指定类型
    public Object LoadRes(string abName, string resName, System.Type type)
    {
        //加载AB包
        LoadAB(abName);
        //外部加载资源，判断类型是不是GameObject
        //如果是，直接实例化返回给外部
        Object obj = abDic[abName].LoadAsset(resName, type);
        if (obj is GameObject)
            return Instantiate(obj);
        else
            return obj;
    }

    //同步加载 根据泛型指定类型
    public T LoadRes<T>(string abName, string resName) where T:Object
    {
        //加载AB包
        LoadAB(abName);
        //外部加载资源，判断类型是不是GameObject
        //如果是，直接实例化返回给外部
        T obj = abDic[abName].LoadAsset<T>(resName);
        if (obj is GameObject)
            return Instantiate(obj);
        else
            return obj;
    }



    //异步加载
    //AB包没有异步，只是从AB包中加载资源时 使用异步
    //根据名字异步加载
    public void LoadResAsync(string abName, string resName,UnityAction<object> callBack)
    {
        StartCoroutine(ReallyLoadResAsync(abName, resName, callBack));
    }

    private IEnumerator ReallyLoadResAsync(string abName, string resName, UnityAction<object> callBack)
    {
        //加载AB包
        LoadAB(abName);
        //外部加载资源，判断类型是不是GameObject
        //如果是，直接实例化返回给外部
        AssetBundleRequest abr = abDic[abName].LoadAssetAsync(resName);
        yield return abr;
        //异步加载结束后 通过委托 传递给外部 外部来使用
        if (abr.asset is GameObject)
            callBack(Instantiate(abr.asset));
        else
            callBack(abr.asset);
    }

    //根据Type异步加载
    public void LoadResAsync(string abName, string resName,System.Type type, UnityAction<object> callBack)
    {
        StartCoroutine(ReallyLoadResAsync(abName, resName, type, callBack));
    }

    private IEnumerator ReallyLoadResAsync(string abName, string resName, System.Type type, UnityAction<object> callBack)
    {
        //加载AB包
        LoadAB(abName);
        //外部加载资源，判断类型是不是GameObject
        //如果是，直接实例化返回给外部
        AssetBundleRequest abr = abDic[abName].LoadAssetAsync(resName, type);
        yield return abr;
        //异步加载结束后 通过委托 传递给外部 外部来使用
        if (abr.asset is GameObject)
            callBack(Instantiate(abr.asset));
        else
            callBack(abr.asset);
    }

    //根据泛型异步加载
    public void LoadResAsync<T>(string abName, string resName, UnityAction<T> callBack) where T : Object
    {
        StartCoroutine(ReallyLoadResAsync<T>(abName, resName, callBack));
    }
    private IEnumerator ReallyLoadResAsync<T>(string abName, string resName, UnityAction<T> callBack)where T:Object
    {
        //加载AB包
        LoadAB(abName);
        //外部加载资源，判断类型是不是GameObject
        //如果是，直接实例化返回给外部
        AssetBundleRequest abr = abDic[abName].LoadAssetAsync(resName);
        yield return abr;
        //异步加载结束后 通过委托 传递给外部 外部来使用
        if (abr.asset is GameObject)
            callBack(Instantiate(abr.asset)as T);
        else
            callBack(abr.asset as T);
    }














    //单个包卸载
    public void UnLoad(string abName)
    {
        if (abDic.ContainsKey(abName))
        {
            abDic[abName].Unload(false);
            abDic.Remove(abName);
        }
    }
    //所有包卸载
    public void ClearAB()
    {
        AssetBundle.UnloadAllAssetBundles(false);
        abDic.Clear();
        mainAB = null;
        mainfest = null;
    }
}
 