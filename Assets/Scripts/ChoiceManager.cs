using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using TMPro;
using UnityEngine.UI;

public class ChoiceManager : MonoBehaviour
{
    [SerializeField] private ValueController valueController;
    [SerializeField] private ButtonController buttonController;
    [SerializeField] private List<ButtonCallback> choices;
    [SerializeField] private TextMeshProUGUI titleText, descriptionText;
    [SerializeField] private Image image;

    [Header("Condition")]
    [SerializeField] private EffectData database2Condition;
    [SerializeField] private EffectData database3Condition;

    private int _walletEventIndex, _shipEventIndex;
    private const string
        JsonFileName1 = "choices-database", JsonFileName2 = "choices-database-2", JsonFileName3 = "choices-database-3",
        WalletEvent = "wallet", ShipEvent = "ship", ExtraEvent = "extra-database";

    private List<CardData> _cardDatabase1,
        _cardDatabase2,
        _cardDatabase3,
        _walletDatabase,
        _shipDatabase,
        _extraDatabase;
    
    public void Awake()
    {
        _walletEventIndex = 0;
        _shipEventIndex = 0;
        
        _cardDatabase1 = JsonConvert.DeserializeObject<List<CardData>>(Resources.Load<TextAsset>(JsonFileName1).text);
        _cardDatabase2 = JsonConvert.DeserializeObject<List<CardData>>(Resources.Load<TextAsset>(JsonFileName2).text);
        _cardDatabase3 = JsonConvert.DeserializeObject<List<CardData>>(Resources.Load<TextAsset>(JsonFileName3).text);
        _walletDatabase = JsonConvert.DeserializeObject<List<CardData>>(Resources.Load<TextAsset>(WalletEvent).text);
        _shipDatabase = JsonConvert.DeserializeObject<List<CardData>>(Resources.Load<TextAsset>(ShipEvent).text);
        _extraDatabase = JsonConvert.DeserializeObject<List<CardData>>(Resources.Load<TextAsset>(ExtraEvent).text);
        
        GetNewChoice();
    }

    private void OnLoadNewChoice(EffectData effectData)
    {
        ClearChoices();
        GetNewChoice();
    }

    private void GetNewChoice()
    {
        var card = decideOnNextCardData();

        titleText.text = card.title;
        descriptionText.text = card.description;
        image.sprite = Resources.Load<Sprite>("CardImage/" + card.thumbnail);

        buttonController.GenerateBtn(card.choices.Count, card.choices);
        choices = buttonController.GetBtnCallbacks();
        for (int i = 0; i < choices.Count; i++)
        {
            choices[i].ChoiceData = card.choices[i];
            choices[i].OnClick += OnLoadNewChoice;
        }
    }

    private CardData decideOnNextCardData()
    {
        if (_shipEventIndex != _shipDatabase.Count && (_walletEventIndex == 0 || _walletEventIndex == _walletDatabase.Count))
        {
            if (_shipEventIndex != 0 || Random.Range(0, 24) == 0)
            {
                return _shipDatabase[_shipEventIndex++];
            }
        }
        
        if (_walletEventIndex != _walletDatabase.Count && (_shipEventIndex == 0 || _shipEventIndex == _shipDatabase.Count))
        {
            if (_walletEventIndex != 0 || Random.Range(0, 10) == 0)
            {
                return _walletDatabase[_walletEventIndex++];
            }
        }

        // TODO: I change the conditions, let it can trigger easy. (by Kalin)
        if (valueController.data.funds > database2Condition.funds || valueController.data.authority > database2Condition.authority || valueController.data.believers > database2Condition.believers)
            if (Random.Range(0, 8) == 0)
                return _cardDatabase3[Random.Range(0, _cardDatabase3.Count)];

        // TODO: I change the conditions, let it can trigger easy. (by Kalin)
        if (valueController.data.funds > database3Condition.funds || valueController.data.authority > database3Condition.authority || valueController.data.believers > database3Condition.believers)
            if (Random.Range(0, 8) == 0)
                return _cardDatabase2[Random.Range(0, _cardDatabase2.Count)];
        
        if (Random.Range(0, 6) == 0)
            return _extraDatabase[Random.Range(0, _extraDatabase.Count)];
        
        return _cardDatabase1[Random.Range(0, _cardDatabase1.Count)];
    }

    private void ClearChoices()
    {
        for (int i = 0; i < choices.Count; i++)
        {
            choices[i].OnClick -= OnLoadNewChoice;
        }
        choices.Clear();
    }
}