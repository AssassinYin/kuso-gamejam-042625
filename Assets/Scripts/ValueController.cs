using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueController : MonoBehaviour
{
    public EffectData data;

    [Header("Max Value Settings")]
    [SerializeField] private int maxDiscoverability = 100;
    [SerializeField] private int maxFunds = 100;
    [SerializeField] private int maxAuthority = 100;
    [SerializeField] private int maxBelievers = 100;

    [Header("UI")]
    [SerializeField] private Image discoverabilityImg;
    [SerializeField] private Image fundsImg;
    [SerializeField] private Image authorityImg;
    [SerializeField] private Image believersImg;

    private void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        discoverabilityImg.fillAmount = (float)data.discoverability / maxDiscoverability;
        fundsImg.fillAmount = (float)data.funds / maxFunds;
        authorityImg.fillAmount = (float)data.authority / maxAuthority;
        believersImg.fillAmount = (float)data.believers / maxBelievers;
    }

    public void SetEffectData(EffectData effectData)
    {
        data.funds += effectData.funds;
        data.authority += effectData.authority;
        data.believers += effectData.believers;
        data.discoverability += effectData.discoverability;
        NormalizeValue();
    }

    private void NormalizeValue()
    {
        if (data.funds <= 0) data.funds = 0;
        if (data.authority <= 0) data.authority = 0;
        if (data.believers <= 0) data.believers = 0;
        if (data.discoverability <= 0) data.discoverability = 0;

        if (data.funds >= maxFunds) data.funds = maxFunds;
        if (data.authority >= maxAuthority) data.authority = maxAuthority;
        if (data.believers >= maxBelievers) data.believers = maxBelievers;
        if (data.discoverability >= maxDiscoverability) data.discoverability = maxDiscoverability;
    }
}
