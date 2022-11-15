
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// MIT License < Please Read LICENSE.md >
// 
// The Purpose Of This Script Is:
//
//  Set the behavior of the Main Menu and the game configuration options.
//
// Documentation and References:
//
//  Unity Awake: https://docs.unity3d.com/ScriptReference/MonoBehaviour.Awake.html
//  Unity Start: https://docs.unity3d.com/ScriptReference/MonoBehaviour.Start.html
//  Unity PlayerPrefs: https://docs.unity3d.com/ScriptReference/PlayerPrefs.html
//  Unity AddListener: https://docs.unity3d.com/ScriptReference/Events.UnityEvent.AddListener.html
//  Unity Coroutine: https://docs.unity3d.com/ScriptReference/MonoBehaviour.StartCoroutine.html
// 
// -----------------------------
// Last Update: 14/11/2022 By MauricioRB06

using System.Collections;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        
        [Header("Menu References")] [Space(5)]
        [Tooltip("Place the new game button here.")]
        [SerializeField] private Button buttonNewGame;
        [Tooltip("Place the exit button here.")]
        [SerializeField] private Button buttonExit;
        [Tooltip("Place here the Spanish language button.")]
        [SerializeField] private Button buttonSpanish;
        [Tooltip("Place here the English language button.")]
        [SerializeField] private Button buttonEnglish;
        [Tooltip("Place here the toggle that modifies the ScreenMode.")]
        [SerializeField] private Toggle toggleScreenMode;
        [Tooltip("Place here the Dropdown that modifies the Resolution.")]
        [SerializeField] private TMP_Dropdown dropDownResolution;
        [Tooltip("Place here the Slider that modifies the music volume.")]
        [SerializeField] private Slider sliderMusicVolume;
        [Tooltip("Place here the Slider that modifies the sound effects volume.")]
        [SerializeField] private Slider sliderSfxVolume;
        
        private Resolution[] _resolutionList;

        private void Awake() => Application.targetFrameRate = 60;

        private void Start()
        {
            
            Application.targetFrameRate = 60;
            _resolutionList = Screen.resolutions;
            
            buttonNewGame.onClick.AddListener(NewGame);
            buttonExit.onClick.AddListener(Exit);
            buttonSpanish.onClick.AddListener(Spanish);
            buttonEnglish.onClick.AddListener(English);
            dropDownResolution.onValueChanged.AddListener(delegate { ChangeResolution();});
            toggleScreenMode.onValueChanged.AddListener(delegate { ScreenMode();});
            sliderMusicVolume.onValueChanged.AddListener(delegate { MusicVolume();});
            sliderSfxVolume.onValueChanged.AddListener(delegate { SfxVolume();});

            foreach (var resolution in _resolutionList)
            {
                dropDownResolution.options.Add(new TMP_Dropdown.OptionData(resolution.ToString())); 
            }

            if (!PlayerPrefs.HasKey("DefaultSettings") || PlayerPrefs.GetString("DefaultSettings") != "Default")
            {
                DefaultSettings();
                LoadSettings();
            }
            else
            {
                LoadSettings();
            }
            
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }

        private static void NewGame() => SceneManager.LoadScene(Random.Range(1, 3));
        
        private static void Exit() => Application.Quit();

        private void SfxVolume()
        {
            AudioManager.Instance.SetSfxVolume(sliderSfxVolume.value);
            PlayerPrefs.SetFloat("SfxVolume", sliderSfxVolume.value);
        }

        private void MusicVolume()
        {
            AudioManager.Instance.SetMusicVolume(sliderMusicVolume.value);
            PlayerPrefs.SetFloat("MusicVolume", sliderMusicVolume.value);
        }

        private static void English()
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[0];
            PlayerPrefs.SetInt("Language", 0);
        }

        private static void Spanish()
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[1];
            PlayerPrefs.SetInt("Language", 1);
        }
        
        private void ScreenMode()
        {
            Screen.fullScreen = toggleScreenMode.isOn;
            PlayerPrefs.SetInt("FullScreen", toggleScreenMode.isOn ? 1 : 0);
        }

        private void ChangeResolution()
        {
            if (_resolutionList == null) return;
            
            Screen.SetResolution(_resolutionList[dropDownResolution.value].width, 
                _resolutionList[dropDownResolution.value].height, Screen.fullScreen);
            
            PlayerPrefs.SetInt("Resolution", dropDownResolution.value);
        }

        private void DefaultSettings()
        {
            PlayerPrefs.SetString("DefaultSettings", "Default");
            PlayerPrefs.SetFloat("SfxVolume", 1);
            PlayerPrefs.SetFloat("MusicVolume", 1);
            PlayerPrefs.SetInt("Language", 0);
            PlayerPrefs.SetInt("FullScreen", 1);
            PlayerPrefs.SetInt("Resolution", _resolutionList.Length - 1);
        }

        private static IEnumerator LoadLanguageSettings()
        {
            yield return new WaitForSeconds(0.1f);
            LocalizationSettings.SelectedLocale = 
                LocalizationSettings.AvailableLocales.Locales[PlayerPrefs.GetInt("Language",0)];
        }

        private void LoadSettings()
        {
            var screenMode = PlayerPrefs.GetInt("FullScreen", 1) == 1;
            toggleScreenMode.isOn = screenMode;

            if (_resolutionList != null)
            {
                Screen.SetResolution(_resolutionList[
                        PlayerPrefs.GetInt("Resolution", _resolutionList.Length - 1)].width, 
                    _resolutionList[PlayerPrefs.GetInt("Resolution", _resolutionList.Length - 1)].height,
                    Screen.fullScreen = screenMode);
                
                dropDownResolution.value = PlayerPrefs.GetInt("Resolution", _resolutionList.Length - 1);
            }
            
            sliderMusicVolume.value = PlayerPrefs.GetFloat("MusicVolume", 1);
            sliderSfxVolume.value = PlayerPrefs.GetFloat("SfxVolume", 1);
            
            AudioManager.Instance.SetMusicVolume(sliderMusicVolume.value);
            AudioManager.Instance.SetSfxVolume(sliderSfxVolume.value);
            
            StartCoroutine(LoadLanguageSettings());
        }
        
    }
}
