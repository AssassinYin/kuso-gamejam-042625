using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueController : MonoBehaviour
{
    //public static ValueController instance;
    public EffectData data;

    private void Start()
    {
        //if (instance == null) instance = this;
    }

    public void SetEffectData(EffectData effectData)
    {
        data.funds += effectData.funds;
        data.discoverability += effectData.discoverability;
        data.authority += effectData.authority;
        data.believers += effectData.believers;
    }
}
