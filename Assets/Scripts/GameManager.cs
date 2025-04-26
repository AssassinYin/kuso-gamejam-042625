using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Canvas endUI;
    [SerializeField] private TextMeshProUGUI endText;

    private ValueController valueController;

    private bool isEnd = false;

    private void Awake()
    {
        valueController = GetComponent<ValueController>();
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
        return valueController.IsBadEnding();
    }

    private bool IsGoodEnding()
    {
        return valueController.IsGoodEnding();
    }
}
