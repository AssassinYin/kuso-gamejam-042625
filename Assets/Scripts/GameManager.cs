using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Canvas endUI;
    [SerializeField] private TextMeshProUGUI endText;
    [SerializeField] private DateHandler dateUpdater;
    private ValueController valueController;

    [Header("Date Settings")]
    [SerializeField] private int finalYear = 2100;
    [SerializeField] private int finalMonth = 12;
    [SerializeField] private int finalDate = 31;
    [SerializeField] private int finalHour = 23;
    [SerializeField] private int finalMin = 59;
    [SerializeField] private int finalSec = 59;
    private string datetime_str;

    private bool isEnd = false;

    private void Awake()
    {
        valueController = GetComponent<ValueController>();
        datetime_str = finalYear.ToString("0000") + "/" + finalMonth.ToString("00") + "/" + finalDate.ToString("00") + " "
                     + finalHour.ToString("00") + ":" + finalMin.ToString("00") + ":" + finalSec.ToString("00");
        //Debug.Log(datetime_str);
    }

    private void Update()
    {
        if (isEnd) return;

        if (IsBadEnding())
        {
            isEnd = true;
            Debug.Log("Bad End!");
        }
        else if (IsGoodEnding())
        {
            isEnd = true;
            Debug.Log("Good End!");
        }
    }

    private bool IsBadEnding()
    {
        return valueController.IsBadEnding() || dateUpdater.IsEraEnd(datetime_str);
    }

    private bool IsGoodEnding()
    {
        return valueController.IsGoodEnding();
    }
}
