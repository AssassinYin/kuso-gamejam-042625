using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class ChoiceManager : MonoBehaviour
{
    [SerializeField] private ButtonCallback choice1, choice2, choice3;
    
    private const string JsonFileName = "choices-database.json";
    private List<CardData> _cardDatabase;
    
    public void Awake()
    {
        _cardDatabase = JsonConvert.DeserializeObject<List<CardData>>(File.ReadAllText(JsonFileName));
        foreach (var cardData in _cardDatabase)
        {
            Debug.Log(cardData.description + "><" + cardData.title);
        }
    }

    public void OnEnable()
    {
        
    }
    
    public void OnDisable()
    {
        
    }

    public void OnLoadNewChoice()
    {
        choice1.ChoiceData = null;
        choice2.ChoiceData = null;
        choice3.ChoiceData = null;
    } 
}