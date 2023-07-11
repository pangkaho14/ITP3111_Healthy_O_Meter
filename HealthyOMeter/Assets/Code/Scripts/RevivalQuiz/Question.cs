using System.Collections.Generic;
using UnityEngine;

// CreateAssetMenu adds a new option to the Menu dropdown list when you right click in the Project window
[CreateAssetMenu(menuName = "Quiz Question", fileName = "New Question")]
public class Question : ScriptableObject
{
    // [TextArea(minLines, maxLines)] allows us to adjust and control the size of the text box in the inspector
    [TextArea(2, 6)] [SerializeField] private string questionText = "Enter new question text here";
    [SerializeField] private List<string> answerChoices = new();
    [SerializeField] private int correctAnswerIndex;

    public string GetQuestionText()
    {
        return questionText;
    }
    
    public int GetAnswerCount()
    {
        return answerChoices.Count;
    }

    public int GetAnswerIndex()
    {
        return correctAnswerIndex;
    }

    public string GetAnswerChoiceText(int index)
    {
        return answerChoices[index];
    }
}
