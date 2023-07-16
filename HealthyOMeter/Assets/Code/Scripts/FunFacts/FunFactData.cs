using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New FunFact", menuName = "Fun Fact")]
public class FunFactData : ScriptableObject
{
    public string EnglishHeader;
    public string ChineseHeader;
    public string EnglishFunFact;
    public string ChineseFunFact;
    public bool isEnglishLocalization;
    public Sprite image;

    public void SetEnglishLocalization(bool isEnglish)
    {
        isEnglishLocalization = isEnglish;
    }

    public string GetFunFactsText()
    {
        return isEnglishLocalization ? EnglishFunFact : ChineseFunFact;
    }

    public string GetHeaderText()
    {
        return isEnglishLocalization ? EnglishHeader : ChineseHeader;
    }

    public string GetFormattedFunFact(string funFact)
    {
        // Replace line break tags with actual line breaks
        return funFact.Replace("\\n", "\n");
    }
}
