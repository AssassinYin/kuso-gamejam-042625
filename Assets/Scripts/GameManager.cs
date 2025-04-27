using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Canvas endUI;
    [SerializeField] private Image endImage;
    [SerializeField] private TextMeshProUGUI endText;
    [SerializeField] private List<string> endTexts = new List<string>();
    [SerializeField] private List<Sprite> endImages = new List<Sprite>();
    [SerializeField] private Image bgImage;

    [Header("Date Settings")]
    [SerializeField] private DateHandler dateUpdater;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private string timeTextS;
    [SerializeField] private int finalYear = 2100;
    [SerializeField] private int finalMonth = 12;
    [SerializeField] private int finalDate = 31;
    [SerializeField] private int finalHour = 23;
    [SerializeField] private int finalMin = 59;
    [SerializeField] private int finalSec = 59;
    private string datetime_str;

    private ValueController valueController;

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
            endUI.gameObject.SetActive(true);
            HandleTimeTextInEnd();
            endText.text = endTexts[0];
            endImage.sprite = endImages[0];
        }
        else if (IsGoodEnding())
        {
            isEnd = true;
            endUI.gameObject.SetActive(true);
            HandleTimeTextInEnd();
            endText.text = endTexts[1];
            endImage.sprite = endImages[1];
        }

        bgImage.GetComponent<BlinkImg>().BlinkSpeed = valueController.GetDiscoverabilityRatio() + 0.1f;
    }

    private void HandleTimeTextInEnd()
    {
        string[] dateDiff = dateUpdater.GetDateDiff().Split('/');
        timeText.text = timeTextS.Replace("{Year}", dateDiff[0]).Replace("{Month}", dateDiff[1]);
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
