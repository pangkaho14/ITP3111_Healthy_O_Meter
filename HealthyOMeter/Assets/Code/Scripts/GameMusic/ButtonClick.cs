using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    [SerializeField] private AudioSource AudioButtonClick;

    public void PlayButtonClick()
    {
        AudioButtonClick.Play();
    }
}
