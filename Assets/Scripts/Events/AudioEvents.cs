using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEvents : MonoBehaviour
{
    [SerializeField] private AudioClip _menuBGM;
    [SerializeField] private AudioClip _euilibrioBGM;
    [SerializeField] private AudioClip _lavaBGM;
    [SerializeField] private AudioClip _buttonSFX;

    [SerializeField] private AudioSource _BGM;
    [SerializeField] private AudioSource _CanvasSFX;

    public void ButtonClick()
    {
        _CanvasSFX.clip = _buttonSFX;
        _CanvasSFX.Play();
    }
}
