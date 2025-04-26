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
    
    private const string JsonFileName = "choices-database.json";
    private List<CardData> _cardDatabase;
    
    public void Awake()
    {
        _cardDatabase = JsonConvert.DeserializeObject<List<CardData>>(File.ReadAllText(JsonFileName));
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
        
        image.sprite = Resources.Load<Sprite>("path/to/image_without_extension");
        
        var card = _cardDatabase[Random.Range(0, _cardDatabase.Count)];
        choice1.ChoiceData = card.choices1;
        choice2.ChoiceData = card.choices2;
        choice3.ChoiceData = card.choices3;
    } 
}