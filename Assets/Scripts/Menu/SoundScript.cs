using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour
{
    [SerializeField] private AudioClip selectButton;
    [SerializeField] private AudioClip longSelectButton;
    [SerializeField] private AudioSource sound;

    public void SelectButton()
    {
        if (sound != null)
        {
            sound.clip = selectButton;
            sound.Play();
        }
    }

    public void LongSelectButton()
    {
        if (sound != null)
        {
            sound.clip = longSelectButton;
            sound.Play();
        }
    }
}
