using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCallback : MonoBehaviour
{
    public Action<EffectData> OnClick;
    
    private CardData _cardData;
    private ChoiceData _choiceData;
    private EffectData _effectData;
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(() => OnClick(_effectData));
    }
    
    private void OnDisable()
    {
        _button.onClick.RemoveListener(() => OnClick(_effectData));
    } 
}