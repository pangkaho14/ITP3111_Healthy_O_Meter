using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteFunction : MonoBehaviour
{
    [SerializeField] Image SoundOnIcon;
    [SerializeField] Image SoundOffIcon;
    private bool muted = false;
    // public Color SelectedMuteColor;
    // public Color NonSelectedMuteColor;
    // public Button MuteButton;
    
    // public Color OldColor;
    // public Color NewColor;
    

    // public void ChangeButtonOldColor()
    // {
    //     ColorBlock cb = MuteButton.colors;
    //     cb.normalColor = OldColor;
    //     cb.highlightedColor = OldColor;
    //     cb.pressedColor = OldColor;
    //     MuteButton.colors = cb;
    // }
    // public void ChangeButtonNewColor()
    // {
    //     ColorBlock cb = MuteButton.colors;
    //     cb.normalColor = NewColor;
    //     cb.highlightedColor = NewColor;
    //     cb.pressedColor = NewColor;
    //     MuteButton.colors = cb;
    // }


    // Start is called before the first frame update
    void Start()
    {
        if(!PlayerPrefs.HasKey("muted"))
        {
            PlayerPrefs.SetInt("muted", 0);
            Load();
        }
        else
        {
            Load();
        }
        UpdateButtonIcon();
        AudioListener.pause = muted;
    }

    //Calls mute function upon button click
    public void OnButtonPress()
    {
        if(muted == false)
        {
            muted = true;
            AudioListener.pause = true;
        }
        else
        {
            muted = false;
            AudioListener.pause = false;
        }
        Save();
        UpdateButtonIcon();
    }

    //Changes button icon when clicked
    private void UpdateButtonIcon()
    {
        if(muted == false)
        {
            SoundOnIcon.enabled = true;
            SoundOffIcon.enabled = false;
            // NonSelectedMuteButtonColor();
        }
        else
        {
            SoundOnIcon.enabled = false;
            SoundOffIcon.enabled = true;
            // SelectedMuteButtonColor();
        }
    }

    // public void SelectedMuteButtonColor()
    // {
    //     ColorBlock cb = MuteButton.colors;
    //     cb.normalColor = SelectedMuteColor;
    //     cb.highlightedColor = SelectedMuteColor;
    //     cb.pressedColor = SelectedMuteColor;
    //     MuteButton.colors = cb;
    // }

    // public void NonSelectedMuteButtonColor()
    // {
    //     ColorBlock cb = MuteButton.colors;
    //     cb.normalColor = NonSelectedMuteColor;
    //     cb.highlightedColor = NonSelectedMuteColor;
    //     cb.pressedColor = NonSelectedMuteColor;
    //     MuteButton.colors = cb;
    // }

    private void Load()
    {
        muted = PlayerPrefs.GetInt("muted") == 1;
    }

    private void Save()
    {
        PlayerPrefs.SetInt("muted", muted ? 1 : 0);
    }
}
