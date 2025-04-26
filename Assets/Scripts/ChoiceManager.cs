using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using TMPro;
using UnityEngine.UI;

public class ChoiceManager : MonoBehaviour
{
    [SerializeField] private ButtonController buttonController;
    [SerializeField] private List<ButtonCallback> choices;
    [SerializeField] private TextMeshProUGUI titleText, descriptionText;
    [SerializeField] private Image image; 
    
    private const string JsonFileName = "Assets/Resources/choices-database.json";
    private List<CardData> _cardDatabase;
    
    public void Awake()
    {
        _cardDatabase = JsonConvert.DeserializeObject<List<CardData>>(File.ReadAllText(JsonFileName));
        foreach (var card in _cardDatabase)
        {
            Debug.Log(card.choices[0].text + " " + card.choices[1].text + " " + card.choices[2].text);
        }
        GetNewChoice();
    }

    private void OnLoadNewChoice(EffectData effectData)
    {
        //titleText.text = _cardDatabase[0].title;
        //descriptionText.text = _cardDatabase[0].description;
        //image.sprite = Resources.Load<Sprite>("Assets/Resources/" + _cardDatabase[0].thumbnailPath);

        ClearChoices();
        GetNewChoice();
    }

    private void GetNewChoice()
    {
        var card = _cardDatabase[Random.Range(0, _cardDatabase.Count)];

        titleText.text = card.title;
        descriptionText.text = card.description;
        image.sprite = Resources.Load<Sprite>("Assets/Resources/" + card.thumbnailPath);

        buttonController.GenerateBtn(card.choices.Count, card.choices);
        choices = buttonController.GetBtnCallbacks();
        for (int i = 0; i < choices.Count; i++)
        {
            choices[i].ChoiceData = card.choices[i];
            choices[i].OnClick += OnLoadNewChoice;
        }
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