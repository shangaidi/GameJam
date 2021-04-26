//========================================Wemake=========================================
//==programmer:
//==date:
//==description:
//==state:
//==rank:0
//========================================Wemake=========================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ClipData{

    public AudioClip audiodata(string name)
    {
        return Resources.Load<AudioClip>("Audios/" + name);
    }
}