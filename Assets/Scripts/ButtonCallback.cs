using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCallback : MonoBehaviour
{
    public Action<EffectData> OnClick;
    public ChoiceData ChoiceData { get => _choiceData; set => _choiceData = value; }
    
    private ChoiceData _choiceData;
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(() => OnClick(_choiceData.effect));
    }
    
    private void OnDisable()
    {
        _button.onClick.RemoveListener(() => OnClick(_choiceData.effect));
    }
}