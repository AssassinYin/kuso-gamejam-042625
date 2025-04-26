using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using TMPro;
using UnityEngine.UI;

public class ChoiceManager : MonoBehaviour
{
    [SerializeField] private ButtonCallback choice1, choice2, choice3;
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
    }
    
    public void OnEnable()
    {
        choice1.OnClick += OnLoadNewChoice;
        choice2.OnClick += OnLoadNewChoice;
        choice3.OnClick += OnLoadNewChoice;
    }
    
    public void OnDisable()
    {
        choice1.OnClick -= OnLoadNewChoice;
        choice2.OnClick -= OnLoadNewChoice;
        choice3.OnClick -= OnLoadNewChoice;
    }
    
    private void OnLoadNewChoice(EffectData effectData)
    {
        titleText.text = _cardDatabase[0].title;
        descriptionText.text = _cardDatabase[0].description;
        image.sprite = Resources.Load<Sprite>("Assets/Resources/" + _cardDatabase[0].thumbnailPath);
        
        var card = _cardDatabase[Random.Range(0, _cardDatabase.Count)];
        choice1.ChoiceData = card.choices[0];
        choice2.ChoiceData = card.choices[1];
        choice3.ChoiceData = card.choices[2];
    }
}