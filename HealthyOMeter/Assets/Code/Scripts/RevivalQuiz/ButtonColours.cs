using UnityEngine;

[CreateAssetMenu(fileName = "ButtonColours", menuName = "Button Colours")]
public class ButtonColours : ScriptableObject
{
    // yellow-ish colour
    [SerializeField] private byte selectedColourRedValue = 255;
    [SerializeField] private byte selectedColourYellowValue = 232;
    [SerializeField] private byte selectedColourBlueValue = 124;
    [SerializeField] private byte selectedColourAlphaValue = 255;
    
    // white colour (same colour as button Normal colour)
    [SerializeField] private byte unselectedColourRedValue = 255;
    [SerializeField] private byte unselectedColourYellowValue = 255;
    [SerializeField] private byte unselectedColourBlueValue = 255;
    [SerializeField] private byte unselectedColourAlphaValue = 255;
    
    public Color GetSelectedColour()
    {
        return new Color32(selectedColourRedValue, selectedColourYellowValue, selectedColourBlueValue, selectedColourAlphaValue);
    }
    
    public Color GetUnselectedColour()
    {
        return new Color32(unselectedColourRedValue, unselectedColourYellowValue, unselectedColourBlueValue, unselectedColourAlphaValue);
    }
}
