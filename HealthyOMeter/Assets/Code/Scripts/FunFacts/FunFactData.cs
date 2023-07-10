using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New FunFact", menuName = "Fun Fact")]
public class FunFactData : ScriptableObject
{
    public string header;   
    public string funFact;
    public Sprite image;

    public string GetFormattedFunFact()
    {
        // Replace line break tags with actual line breaks
        return funFact.Replace("\\n", "\n");
    }
}
