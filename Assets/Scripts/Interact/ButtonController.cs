using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private ValueController valueController;
    [SerializeField] private DateHandler dateUpdater;
    [SerializeField] private GameObject btnGroup;
    [SerializeField] private GameObject btnPrefab;

    [Header("Tip Seetings")]
    [SerializeField] private Image discoverabilityImg;
    [SerializeField] private Image fundsImg;
    [SerializeField] private Image fundsImgIcon;
    [SerializeField] private Image authorityImg;
    [SerializeField] private Image authorityImgIcon;
    [SerializeField] private Image believersImg;
    [SerializeField] private Image believersImgIcon;
    [SerializeField] private float blinkSpeed = 0.5f;

    private List<GameObject> _buttons = new List<GameObject>();

    private void Awake()
    {
        discoverabilityImg.AddComponent<BlinkImg>().BlinkSpeed = blinkSpeed;
        fundsImg.AddComponent<BlinkImg>().BlinkSpeed = blinkSpeed;
        fundsImgIcon.AddComponent<BlinkImg>().BlinkSpeed = blinkSpeed;
        authorityImg.AddComponent<BlinkImg>().BlinkSpeed = blinkSpeed;
        authorityImgIcon.AddComponent<BlinkImg>().BlinkSpeed = blinkSpeed;
        believersImg.AddComponent<BlinkImg>().BlinkSpeed = blinkSpeed;
        believersImgIcon.AddComponent<BlinkImg>().BlinkSpeed = blinkSpeed;

        SetIconEnabled(false);
    }

    private void SetIconEnabled(bool isEnabled)
    {
        discoverabilityImg.enabled = isEnabled;
        fundsImg.enabled = isEnabled;
        fundsImgIcon.enabled = isEnabled;
        authorityImg.enabled = isEnabled;
        authorityImgIcon.enabled = isEnabled;
        believersImg.enabled = isEnabled;
        believersImgIcon.enabled = isEnabled;
    }

    private void BtnInit()
    {
        foreach (var btn in _buttons)
        {
            UnbindEvents(btn);
            Destroy(btn);
        }
        _buttons.Clear();
    }

    public void GenerateBtn(int btnNumber, List<ChoiceData> choiceDatas)
    {
        BtnInit();

        // Dynamic generating buttons
        for (int i = 0; i < btnNumber; i++)
        {
            GameObject btn = Instantiate(btnPrefab, btnGroup.transform);
            btn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = choiceDatas[i].text;
            btn.GetComponent<ButtonCardData>().effectData = choiceDatas[i].effect;
            btn.AddComponent<EventTrigger>();
            btn.AddComponent<ButtonCallback>();
            BindEvents(btn);
            _buttons.Add(btn);
        }
    }

    private void BindEvents(GameObject obj)
    {
        obj.GetComponent<EventTrigger>().OnPointerEnter += ;
        obj.GetComponent<EventTrigger>().OnPointerExit +=;
        obj.GetComponent<ButtonCallback>().OnClick += valueController.SetEffectData;
        obj.GetComponent<ButtonCallback>().OnClick += dateUpdater.AddMonth;
    }

    private void UnbindEvents(GameObject obj)
    {
        obj.GetComponent<EventTrigger>().OnPointerEnter -= ;
        obj.GetComponent<EventTrigger>().OnPointerExit -=;
        obj.GetComponent<ButtonCallback>().OnClick -= valueController.SetEffectData;
        obj.GetComponent<ButtonCallback>().OnClick -= dateUpdater.AddMonth;
    }

    public List<ButtonCallback> GetBtnCallbacks()
    {
        List<ButtonCallback> btnCB = new List<ButtonCallback>();
        
        foreach (var btn in _buttons)
        {
            btnCB.Add(btn.GetComponent<ButtonCallback>());
        }
        return btnCB;
    }
}
