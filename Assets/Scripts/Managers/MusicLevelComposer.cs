
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// MIT License < Please Read LICENSE.md >
// 
// The Purpose Of This Script Is:
//
//  Update the Music of the level when it is loaded.
//
// Documentation and References:
//
//  Unity Start: https://docs.unity3d.com/ScriptReference/MonoBehaviour.Start.html
// 
// -----------------------------
// Last Update: 14/11/2022 By MauricioRB06

using UnityEngine;

namespace Managers
{
    public class MusicLevelComposer : MonoBehaviour
    {
        [Header("Level Music")] [Space(5)]
        [Tooltip("Place here the music you want to start playing when the level is loaded.")]
        [SerializeField] private AudioClip musicLevel;
        
        private void Start()
        {
            if (musicLevel == null) return;
            
            AudioManager.Instance.PlayMusic(musicLevel);
            Destroy(gameObject);
        }
        
    }
}
