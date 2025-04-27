using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

enum Type
{
    All, Fund, Authority, Believers, Discoverability
}

public class ButtonController : MonoBehaviour
{
    [SerializeField] private ValueController valueController;
    [SerializeField] private DateHandler dateUpdater;
    [SerializeField] private GameObject btnGroup;
    [SerializeField] private GameObject btnPrefab;
    private AudioSource _audioSource;

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
        SetIconEnabled(Type.All, false);
        ResetImages();
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void SetIconEnabled(Type type, bool isEnabled)
    {
        switch(type)
        {
            case Type.Discoverability:
                discoverabilityImg.GetComponent<BlinkImg>().enabled = isEnabled;
                break;
            case Type.Fund:
                fundsImg.GetComponent<BlinkImg>().enabled = isEnabled;
                fundsImgIcon.GetComponent<BlinkImg>().enabled = isEnabled;
                break;
            case Type.Authority:
                authorityImg.GetComponent<BlinkImg>().enabled = isEnabled;
                authorityImgIcon.GetComponent<BlinkImg>().enabled = isEnabled;
                break;
            case Type.Believers:
                believersImg.GetComponent<BlinkImg>().enabled = isEnabled;
                believersImgIcon.GetComponent<BlinkImg>().enabled = isEnabled;
                break;
            case Type.All:
                discoverabilityImg.GetComponent<BlinkImg>().enabled = isEnabled;
                fundsImg.GetComponent<BlinkImg>().enabled = isEnabled;
                fundsImgIcon.GetComponent<BlinkImg>().enabled = isEnabled;
                authorityImg.GetComponent<BlinkImg>().enabled = isEnabled;
                authorityImgIcon.GetComponent<BlinkImg>().enabled = isEnabled;
                believersImg.GetComponent<BlinkImg>().enabled = isEnabled;
                believersImgIcon.GetComponent<BlinkImg>().enabled = isEnabled;
                break;
        }
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
            btn.AddComponent<ButtonCallback>();
            BindEvents(btn);
            _buttons.Add(btn);
        }
    }

    private void BindEvents(GameObject obj)
    {
        obj.GetComponent<ButtonCallback>().OnPointerEnterButton += OnPointerEnterButton;
        obj.GetComponent<ButtonCallback>().OnPointerExitButton += OnPointerExitButton;
        obj.GetComponent<ButtonCallback>().OnPointerUpButton += OnPointerExitButton;
        obj.GetComponent<ButtonCallback>().OnClick += PlayClickAudio;
        obj.GetComponent<ButtonCallback>().OnClick += valueController.SetEffectData;
        obj.GetComponent<ButtonCallback>().OnClick += dateUpdater.AddMonth;
    }

    private void UnbindEvents(GameObject obj)
    {
        obj.GetComponent<ButtonCallback>().OnPointerEnterButton -= OnPointerEnterButton;
        obj.GetComponent<ButtonCallback>().OnPointerExitButton -= OnPointerExitButton;
        obj.GetComponent<ButtonCallback>().OnPointerUpButton -= OnPointerExitButton;
        obj.GetComponent<ButtonCallback>().OnClick -= PlayClickAudio;
        obj.GetComponent<ButtonCallback>().OnClick -= valueController.SetEffectData;
        obj.GetComponent<ButtonCallback>().OnClick -= dateUpdater.AddMonth;
    }

    private void PlayClickAudio(EffectData data)
    {
        _audioSource.Play();
        ResetImages();
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

    private void OnPointerEnterButton(PointerEventData eventData)
    {
        //Debug.Log("Enter: " + eventData.pointerEnter.gameObject.name);
        try {
            EffectData effect = eventData.pointerEnter.gameObject.GetComponent<ButtonCardData>().effectData;
            if (effect.funds != 0) SetIconEnabled(Type.Fund, true);
            if (effect.authority != 0) SetIconEnabled(Type.Authority, true);
            if (effect.believers != 0) SetIconEnabled(Type.Believers, true);
            if (effect.discoverability != 0) SetIconEnabled(Type.Discoverability, true);
        }
        catch (Exception e) 
        {
            ResetImages();
        }
    }

    private void OnPointerExitButton(PointerEventData eventData)
    {
        //Debug.Log("Exit: " + eventData.pointerEnter.gameObject.name);
        ResetImages();
    }

    private void ResetImages()
    {
        SetIconEnabled(Type.All, false);
        discoverabilityImg.color = new Color(discoverabilityImg.color.r, discoverabilityImg.color.g, discoverabilityImg.color.b, 1);
        fundsImg.color = new Color(fundsImg.color.r, fundsImg.color.g, fundsImg.color.b, 1);
        fundsImgIcon.color = new Color(fundsImgIcon.color.r, fundsImgIcon.color.g, fundsImgIcon.color.b, 1);
        authorityImg.color = new Color(authorityImg.color.r, authorityImg.color.g, authorityImg.color.b, 1);
        authorityImgIcon.color = new Color(authorityImgIcon.color.r, authorityImgIcon.color.g, authorityImgIcon.color.b, 1)   ;
        believersImg.color = new Color(believersImg.color.r, believersImg.color.g, believersImg.color.b, 1);
        believersImgIcon.color = new Color(believersImgIcon.color.r, believersImgIcon.color.g, believersImgIcon.color.b, 1);
    }
}
