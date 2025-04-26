using System.Collections.Generic;

[System.Serializable]
public class CardData
{
    public string title;
    public string description;
    public string thumbnailPath;
    public List<ChoiceData> choices;
}