using System.Collections.Generic;
using UnityEngine;

// CreateAssetMenu adds a new option to the Menu dropdown list when you right click in the Project window
[CreateAssetMenu(menuName = "Quiz Question", fileName = "New Question")]
public class Question : ScriptableObject
{
    // [TextArea(minLines, maxLines)] allows us to adjust and control the size of the text box in the inspector
    [TextArea(2, 6)] [SerializeField] private string englishQuestionText = "Enter new question text here";
    
    // dummy question text for testing
    [TextArea(2, 6)] [SerializeField] private string chineseQuestionText = "全麦食品（例如全麦面包，糙米）含有比精致谷物（例如白面包，白米）更多的营养. 真的/ 错的";
    
    [SerializeField] private bool isEnglishLocalization = true;
    [SerializeField] private List<string> englishAnswerChoices = new();
    [SerializeField] private List<string> chineseAnswerChoices = new();
    
    [SerializeField] private int correctAnswerIndex;

    public void SetEnglishLocalization(bool isEnglish)
    {
        isEnglishLocalization = isEnglish;
    }
    
    public string GetQuestionText()
    {
        return isEnglishLocalization ? englishQuestionText : chineseQuestionText;
    }
    
    public int GetAnswerCount()
    {
        return englishAnswerChoices.Count;
    }

    public int GetAnswerIndex()
    {
        return correctAnswerIndex;
    }

    public string GetAnswerChoiceText(int index)
    {
        return isEnglishLocalization ? englishAnswerChoices[index] : chineseAnswerChoices[index];
    }
}
