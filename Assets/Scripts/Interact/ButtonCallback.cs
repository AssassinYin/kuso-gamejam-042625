using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonCallback : MonoBehaviour
{
    public Action<EffectData> OnClick;
    public Action<PointerEventData> OnPointerEnterButton;
    public Action<PointerEventData> OnPointerExitButton;
    public Action<PointerEventData> OnPointerUpButton;
    public ChoiceData ChoiceData { set => _choiceData = value; }
    
    private ChoiceData _choiceData;
    private Button _button;

    private EventTrigger.Entry _btnEnterEntry;
    private EventTrigger.Entry _btnExitEntry;
    private EventTrigger.Entry _btnUpEntry;
    private EventTrigger _trigger;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _trigger = GetComponent<EventTrigger>();

        _btnEnterEntry = new EventTrigger.Entry { eventID = EventTriggerType.PointerEnter };
        _btnExitEntry = new EventTrigger.Entry { eventID = EventTriggerType.PointerExit };
        _btnUpEntry = new EventTrigger.Entry { eventID = EventTriggerType.PointerUp };
        _trigger.triggers.Add(_btnEnterEntry);
        _trigger.triggers.Add(_btnExitEntry);
        _trigger.triggers.Add(_btnUpEntry);
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(() => OnClick(_choiceData.effect));
        _btnEnterEntry.callback.AddListener((data) => { OnPointerEnterButton((PointerEventData)data); });
        _btnExitEntry.callback.AddListener((data) => { OnPointerExitButton((PointerEventData)data); });
        _btnUpEntry.callback.AddListener((data) => { OnPointerUpButton((PointerEventData)data); });
    }
    
    private void OnDisable()
    {
        _button.onClick.RemoveListener(() => OnClick(_choiceData.effect));
        _btnEnterEntry.callback.RemoveListener((data) => { OnPointerEnterButton((PointerEventData)data); });
        _btnExitEntry.callback.RemoveListener((data) => { OnPointerExitButton((PointerEventData)data); });
        _btnUpEntry.callback.RemoveListener((data) => { OnPointerUpButton((PointerEventData)data); });
    }
}