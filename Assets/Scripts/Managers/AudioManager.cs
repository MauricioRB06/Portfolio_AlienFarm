
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// MIT License < Please Read LICENSE.md >
// 
// The Purpose Of This Script Is:
//
//  Set the behavior of the AudioManager that will manage the sounds in the game.
//
// Documentation and References:
//
//  Unity Awake: https://docs.unity3d.com/ScriptReference/MonoBehaviour.Awake.html
// 
// -----------------------------
// Last Update: 14/11/2022 By MauricioRB06

using UnityEngine;
using UnityEngine.Audio;

namespace Managers
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }
        
        [Header("Audio Mixers")] [Space(5)]
        [Tooltip("Place here the Audio Mixer that will be in charge of the music output channel.")]
        [SerializeField] private AudioMixer musicMixer;
        [Tooltip("Place here the Audio Mixer that will be in charge of the sound effects output channel.")]
        [SerializeField] private AudioMixer sfxMixer;
        [Space(15)]
        
        [Header("Audio Sources")] [Space(5)]
        [Tooltip("Place here the Audio Source that will be in charge of the music output channel.")]
        [SerializeField] private AudioSource musicAudioSource;
        [Tooltip("Place here the Audio Source that will be in charge of the sound effects output channel")]
        [SerializeField] private AudioSource sfxAudioSource;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                
                musicAudioSource.playOnAwake = false;
                musicAudioSource.loop = true;
                sfxAudioSource.playOnAwake = false;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void SetMusicVolume(float volume) => musicMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);

        public void SetSfxVolume(float volume) => sfxMixer.SetFloat("SfxVolume", Mathf.Log10(volume) * 20);

        public void PlaySound(AudioClip clip) => sfxAudioSource.PlayOneShot(clip);

        public void PlayMusic(AudioClip clip)
        {
            musicAudioSource.clip = clip;
            musicAudioSource.Play();
        }

    }
}
