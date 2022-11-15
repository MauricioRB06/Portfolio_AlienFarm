
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// MIT License < Please Read LICENSE.md >
// 
// The Purpose Of This Script Is:
//
//  Set the behavior of the player.
//
// Documentation and References:
//
//  Unity Awake: https://docs.unity3d.com/ScriptReference/MonoBehaviour.Awake.html
//  Unity FixedUpdate: https://docs.unity3d.com/ScriptReference/MonoBehaviour.FixedUpdate.html
//  Unity OnEnable: https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnEnable.html
//  Unity OnDisable: https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnDisable.html
//  Unity InputSystem: https://docs.unity3d.com/Manual/com.unity.inputsystem.html
// 
// -----------------------------
// Last Update: 14/11/2022 By MauricioRB06

using Managers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    // Components required for this script work.
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Player Movement Settings")] [Space(5)]
        [Tooltip("Set the movement speed for the player.")]
        [SerializeField] [Range(1.0f,10.0f)] private float speed = 5.0f;
        [Tooltip("Set the acceleration with which the speed of movement will change for the player.")]
        [SerializeField] [Range(1.0f,10.0f)] private float acceleration = 3.0f;
        [Space(15)]
        
        [Header("Player Shoot Settings")] [Space(5)]
        [Tooltip("Place here the point from which the projectiles will be fired.")]
        [SerializeField] private Transform proyectileLaunchPoint;
        [Tooltip("Place here the projectiles that will be fired randomly.")]
        [SerializeField] private GameObject[] proyectiles;
        [Tooltip("Place here the AudioClips that will be played randomly every time a projectile is fired.")]
        [SerializeField] private AudioClip[] shootSounds;
        
        private PlayerInputActions _playerInputActions;
        private Rigidbody2D _playerRigidBody2D;
        private float _speedMultiplier;
        private float _horizontalInput;
        private float _verticalInput;
        private bool _playerIsMoving;
        
        private void Awake()
        {
            _playerInputActions = new PlayerInputActions();
            _playerRigidBody2D = GetComponent<Rigidbody2D>();
        }
        
        private void OnEnable()
        {
            _playerInputActions.Enable();
            _playerInputActions.Player.Move.started += OnMoveStart;
            _playerInputActions.Player.Move.canceled += OnMoveStop;
            _playerInputActions.Player.Fire.canceled += OnFire;
            _playerInputActions.Player.Pause.started += OnPause;
        }

        private void OnDisable()
        {
            _playerInputActions.Disable();
            _playerInputActions.Player.Move.started -= OnMoveStart;
            _playerInputActions.Player.Move.canceled -= OnMoveStop;
            _playerInputActions.Player.Fire.canceled -= OnFire;
            _playerInputActions.Player.Pause.started -= OnPause;
        }

        private void FixedUpdate()
        {
            UpdateSpeedMultiplier();
            
            _verticalInput = speed * _speedMultiplier * _playerInputActions.Player.Move.ReadValue<Vector2>().y;
            _horizontalInput = speed * _speedMultiplier * _playerInputActions.Player.Move.ReadValue<Vector2>().x;
            
            _playerRigidBody2D.velocity = new Vector2(_horizontalInput, _verticalInput);
        }

        private void UpdateSpeedMultiplier()
        {
            switch (_playerIsMoving)
            {
                case true when _speedMultiplier < 1:
                    _speedMultiplier += Time.deltaTime * acceleration;
                    break;
                
                case false when _speedMultiplier > 0:
                {
                    _speedMultiplier -= Time.deltaTime * acceleration;
                    if (_speedMultiplier < 0) _speedMultiplier = 0;
                    break;
                }
            }
        }

        private void OnMoveStart(InputAction.CallbackContext ctx) => _playerIsMoving = true;
        
        private void OnMoveStop(InputAction.CallbackContext ctx) => _playerIsMoving = false;

        private void OnFire(InputAction.CallbackContext obj)
        {
            Instantiate(proyectiles[Random.Range(0,proyectiles.Length)], proyectileLaunchPoint.position, Quaternion.identity);
            AudioManager.Instance.PlaySound(shootSounds[Random.Range(0,shootSounds.Length)]);
        }
        
        private static void OnPause(InputAction.CallbackContext obj) => GameManager.Instance.PauseGame();
        
    }
}
