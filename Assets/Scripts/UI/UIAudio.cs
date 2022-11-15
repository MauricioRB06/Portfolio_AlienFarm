
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// MIT License < Please Read LICENSE.md >
// 
// The Purpose Of This Script Is:
//
//  Generate an access method to play sounds in the user interface using the AudioManager.
// 
// -----------------------------
// Last Update: 14/11/2022 By MauricioRB06

using Managers;
using UnityEngine;

namespace UI
{
    public class UIAudio : MonoBehaviour
    {
        [Header("UI Audio Clips")] [Space(5)]
        [Tooltip("Place here the AudioClip that will be played when a Button is clicked.")]
        [SerializeField] private AudioClip buttonClick;
        [Tooltip("Place here the AudioClip that will be played when a Button is hovered.")]
        [SerializeField] private AudioClip buttonHover;
        [Tooltip("Place here the AudioClip that will be played when a Button is pressed or Selected.")]
        [SerializeField] private AudioClip buttonPress;
        
        public void PlayButtonClick() => AudioManager.Instance.PlaySound(buttonClick);
        
        public void PlayButtonHover() => AudioManager.Instance.PlaySound(buttonHover);
        
        public void PlayButtonPress() => AudioManager.Instance.PlaySound(buttonPress);
        
    }
}
