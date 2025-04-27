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

    private int _walletEventIndex, _shipEventIndex;
    private const string
        JsonFileName1 = "choices-database", JsonFileName2 = "choices-database-2", JsonFileName3 = "choices-database-3",
        WalletEvent = "wallet", ShipEvent = "ship";
    private List<CardData> _cardDatabase1, _cardDatabase2, _cardDatabase3, _walletDatabase, _shipDatabase;
    
    public void Awake()
    {
        _walletEventIndex = 0;
        _shipEventIndex = 0;
        
        _cardDatabase1 = JsonConvert.DeserializeObject<List<CardData>>(Resources.Load<TextAsset>(JsonFileName1).text);
        _cardDatabase2 = JsonConvert.DeserializeObject<List<CardData>>(Resources.Load<TextAsset>(JsonFileName2).text);
        _cardDatabase3 = JsonConvert.DeserializeObject<List<CardData>>(Resources.Load<TextAsset>(JsonFileName3).text);
        _walletDatabase = JsonConvert.DeserializeObject<List<CardData>>(Resources.Load<TextAsset>(WalletEvent).text);
        _shipDatabase = JsonConvert.DeserializeObject<List<CardData>>(Resources.Load<TextAsset>(ShipEvent).text);
        
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
        if (_shipEventIndex != _shipDatabase.Count && _walletEventIndex == 0)
        {
            if (_shipEventIndex != 0 || Random.Range(0, 24) == 0)
            {
                return _shipDatabase[_shipEventIndex++];
            }
        }
        
        if (_walletEventIndex != _walletDatabase.Count && _shipEventIndex == 0)
        {
            if (_walletEventIndex != 0 || Random.Range(0, 10) == 0)
            {
                return _walletDatabase[_walletEventIndex++];
            }
        }
        
        if (valueController.data.funds > 70 || valueController.data.authority > 70 || valueController.data.believers > 70)
            if (Random.Range(0, 4) == 0)
                return _cardDatabase3[Random.Range(0, _cardDatabase3.Count)];
        
        if (valueController.data.funds > 50 || valueController.data.authority > 50 || valueController.data.believers > 50)
            if (Random.Range(0, 4) == 0)
                return _cardDatabase2[Random.Range(0, _cardDatabase2.Count)];
        
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