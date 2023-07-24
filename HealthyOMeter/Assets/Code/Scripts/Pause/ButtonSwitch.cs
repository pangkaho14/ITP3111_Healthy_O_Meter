using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

public class ButtonSwitch : MonoBehaviour
{
    public Button button;
    
    public void deactivateButton()
    {
        button.interactable = false;
    }
    
    public void activateButton()
    {
        button.interactable = true;
    }
}
