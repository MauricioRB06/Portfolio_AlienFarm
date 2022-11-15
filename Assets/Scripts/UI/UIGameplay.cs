
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// MIT License < Please Read LICENSE.md >
// 
// The Purpose Of This Script Is:
//
//  Set the options that the Resume and Exit buttons have in the pause menu of the game.
// 
// -----------------------------
// Last Update: 14/11/2022 By MauricioRB06

using Managers;
using UnityEngine;

namespace UI
{
    public class UIGameplay : MonoBehaviour
    {
        
        public void ResumeGame()
        {
            Time.timeScale = 1;
            GameManager.Instance.PauseGame();
        }
        
        public void ExitGame()
        {
            GameManager.Instance.Exit();
            Time.timeScale = 1;
        }
        
    }
}
