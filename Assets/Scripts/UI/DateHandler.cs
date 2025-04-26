using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DateHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dateText;

    private DateTime _oriDate => DateTime.Now; 
    private DateTime _dt;

    private void Awake()
    {
        _dt = _oriDate;
        UpdateUI();
    }

    public void AddMonth(EffectData effectData)
    {
        _dt = _dt.AddMonths(1);
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (!dateText) return;

        dateText.text = _dt.ToString("yyyy/MM");
    }

    public bool IsEraEnd(string dt)
    {
        return _dt > DateTime.Parse(dt);
    }

    public string GetDateDiff()
    {
        int years = _dt.Year - _oriDate.Year;
        int month = _dt.Month - _oriDate.Month;

        if (month < 0)
        {
            years -= 1;
            month += 12;
        }

        return years.ToString("00") + "/" + month.ToString("00");
    }
}
