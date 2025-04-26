using System.Collections.Generic;

[System.Serializable]
public class CardData
{
    public string title;
    public string description;
    public string thumbnail;
    public List<ChoiceData> choices;
}