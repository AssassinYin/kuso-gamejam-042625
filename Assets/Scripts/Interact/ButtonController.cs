using System;
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
        SetIconEnabled(false);
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void SetIconEnabled(bool isEnabled)
    {
        discoverabilityImg.GetComponent<BlinkImg>().enabled = isEnabled;
        fundsImg.GetComponent<BlinkImg>().enabled = isEnabled;
        fundsImgIcon.GetComponent<BlinkImg>().enabled = isEnabled;
        authorityImg.GetComponent<BlinkImg>().enabled = isEnabled;
        authorityImgIcon.GetComponent<BlinkImg>().enabled = isEnabled;
        believersImg.GetComponent<BlinkImg>().enabled = isEnabled;
        believersImgIcon.GetComponent<BlinkImg>().enabled = isEnabled;
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
        obj.GetComponent<ButtonCallback>().OnClick += PlayClickAudio;
        obj.GetComponent<ButtonCallback>().OnClick += valueController.SetEffectData;
        obj.GetComponent<ButtonCallback>().OnClick += dateUpdater.AddMonth;
    }

    private void UnbindEvents(GameObject obj)
    {
        obj.GetComponent<ButtonCallback>().OnPointerEnterButton -= OnPointerEnterButton;
        obj.GetComponent<ButtonCallback>().OnPointerExitButton -= OnPointerExitButton;
        obj.GetComponent<ButtonCallback>().OnClick -= PlayClickAudio;
        obj.GetComponent<ButtonCallback>().OnClick -= valueController.SetEffectData;
        obj.GetComponent<ButtonCallback>().OnClick -= dateUpdater.AddMonth;
    }

    private void PlayClickAudio(EffectData data)
    {
        _audioSource.Play();
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
        Debug.Log("Enter: " + eventData.pointerEnter.gameObject.name);
    }

    private void OnPointerExitButton(PointerEventData eventData)
    {
        Debug.Log("Exit: " + eventData.pointerEnter.gameObject.name);
    }
}
