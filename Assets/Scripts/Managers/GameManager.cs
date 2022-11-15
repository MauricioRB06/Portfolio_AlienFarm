
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// MIT License < Please Read LICENSE.md >
// 
// The Purpose Of This Script Is:
//
//  Set the behavior of the GameManager that will manage the games.
//
// Documentation and References:
//
//  Unity Awake: https://docs.unity3d.com/ScriptReference/MonoBehaviour.Awake.html
//  Unity Start: https://docs.unity3d.com/ScriptReference/MonoBehaviour.Start.html
//  Unity Update: https://docs.unity3d.com/ScriptReference/MonoBehaviour.Update.html
//  Unity OnEnable: https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnEnable.html
//  Unity OnDisable: https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnDisable.html
// 
// -----------------------------
// Last Update: 14/11/2022 By MauricioRB06

using Animals;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        
        [Header("Gameplay Prefabs")] [Space(5)]
        [Tooltip("Place the player's spawn point here.")]
        [SerializeField] private GameObject playerSpawnPoint;
        [Tooltip("Place here the spawn points for the animals.")]
        [SerializeField] private GameObject[] animalSpawnPoints;
        [Tooltip("Place here the different Prefabs Available to the Player.")]
        [SerializeField] private GameObject[] playerPrefabs;
        [Tooltip("Place here the different Prefabs for the animals.")]
        [SerializeField] private GameObject[] animalPrefabs;
        [Space(15)]
        
        [Header("UI Settings")] [Space(5)]
        [Tooltip("Place here the text where the elapsed time will be displayed.")]
        [SerializeField] private TextMeshProUGUI cronometerText;
        [Tooltip("Place here the text where the player's score will be displayed.")]
        [SerializeField] private TextMeshProUGUI scoreText;
        [Tooltip("Place here the text where the final score obtained by the player will be shown.")]
        [SerializeField] private TextMeshProUGUI gameOverText;
        [Tooltip("Place here the panel containing the UI that will be displayed during the game.")]
        [SerializeField] private GameObject panelGameplay;
        [Tooltip("Place here the panel containing the UI that will be displayed when the game is paused.")]
        [SerializeField] private GameObject panelPause;
        [Tooltip("Place here the panel containing the UI that will be displayed when the game is over.")]
        [SerializeField] private GameObject panelGameOver;
        [Tooltip("Place here the button to end the game.")]
        [SerializeField] private Button buttonGameOver;
        [Tooltip("Place here the button to resume the game.")]
        [SerializeField] private Button buttonResume;
        
        private float _timer;
        private bool _isPaused;
        private bool _isGameOver;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = false;
            
                Instantiate(playerPrefabs[Random.Range(0,playerPrefabs.Length)],
                    playerSpawnPoint.transform.position, Quaternion.identity);
            
                Instantiate(animalPrefabs[Random.Range(0,animalPrefabs.Length)], 
                    animalSpawnPoints[Random.Range(0,animalSpawnPoints.Length)].transform.position, Quaternion.identity);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        private void OnEnable() => Animal.AnimalDelegate += SpawnAnimal;

        private void OnDisable() => Animal.AnimalDelegate -= SpawnAnimal;

        private void Start() => _timer = 0;

        private void Update()
        {
            if(_isGameOver) return;
            
            _timer += Time.deltaTime;
            
            var minutes = (int)_timer / 60;
            var seconds = (int)_timer % 60;

            cronometerText.text = $"{minutes:00}:{seconds:00}";
            
            if (_timer < 120) return;
            
            _isGameOver = true;
            Time.timeScale = 0;
            panelGameplay.SetActive(false);
            panelGameOver.SetActive(true);
            EventSystem.current.SetSelectedGameObject(buttonGameOver.gameObject);
            Cursor.visible = true;
            
            gameOverText.text = scoreText.text;
        }
        
        private void SpawnAnimal(int score)
        {
            if (_isGameOver) return;
            
            UpdateScore(score);
            
            Instantiate(animalPrefabs[Random.Range(0,animalPrefabs.Length)], 
                animalSpawnPoints[Random.Range(0,animalSpawnPoints.Length)].transform.position, Quaternion.identity);
        }
        
        private void UpdateScore(int score) => scoreText.text = (int.Parse(scoreText.text) + score).ToString();
        
        public void PauseGame()
        {
            if (_isGameOver) return;
            
            if (!_isPaused)
            {
                Time.timeScale = 0;
                panelGameplay.SetActive(false);
                panelPause.SetActive(true);
                EventSystem.current.SetSelectedGameObject(buttonResume.gameObject);
                Cursor.visible = true;
                _isPaused = true;
            }
            else
            {
                Time.timeScale = 1;
                panelGameplay.SetActive(true);
                panelPause.SetActive(false);
                Cursor.visible = false;
                _isPaused = false;
            }
        }

        public void Exit()
        {
            SceneManager.LoadScene(0);
            Destroy(gameObject);
        }
        
    }
}
