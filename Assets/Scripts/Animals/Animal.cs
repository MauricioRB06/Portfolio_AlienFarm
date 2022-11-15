
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// MIT License < Please Read LICENSE.md >
// 
// The Purpose Of This Script Is:
//
//  Set the behavior of the animals.
//
// Documentation and References:
//
//  Unity Awake: https://docs.unity3d.com/ScriptReference/MonoBehaviour.Awake.html
//  Unity FixedUpdate: https://docs.unity3d.com/ScriptReference/MonoBehaviour.FixedUpdate.html
//  C# Constructors: https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/constructors
// 
// -----------------------------
// Last Update: 14/11/2022 By MauricioRB06

using System;
using Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Animals
{
    // Components required for this script work.
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class Animal : MonoBehaviour
    {
        
        [Header("Collision Settings")] [Space(5)]
        [Tooltip("Set here the layer where collisions with walls will be detected.")]
        [SerializeField] private LayerMask wallLayerMask;
        [Space(15)]
        
        [Header("Collision Settings")] [Space(5)]
        [Tooltip("Place here the image that will represent that the animal has lost a life.")]
        [SerializeField] private Sprite imageLowLife;
        [Tooltip("Place here the object that represents the animal's life 2.")]
        [SerializeField] private GameObject live2;
        [Tooltip("Place here the object that represents the animal's life 2.")]
        [SerializeField] private GameObject live3;
        [Space(15)]
        
        [Header("Collision Settings")] [Space(5)]
        [Tooltip("Place here the AudioClip that will be played when the animal is captured.")]
        [SerializeField] private AudioClip animalAudio;
        [Space(15)]
        
        [Header("Collision Settings")] [Space(5)]
        [Tooltip("Indicate here the score that the animal will give when captured.")]
        [SerializeField] private int animalScore;
        
        private float _animalSpeed;
        private Rigidbody2D _animalRigidbody2D;
        private bool _animalIsMovingRight;
        private int _lives;
        
        public static event Action<int> AnimalDelegate;
        
        private Animal() { _lives = 3; }
        
        private void Awake()
        {
            _animalRigidbody2D = GetComponent<Rigidbody2D>();
            _animalSpeed = Random.Range(2.0f, 7.0f);
        }
        
        private void FixedUpdate()
        {
            if (_animalIsMovingRight)
            {
                _animalRigidbody2D.velocity = new Vector2(Vector2.left.x * _animalSpeed, _animalRigidbody2D.velocity.y);

                if (Physics2D.Raycast(transform.position, Vector2.left, 1f, wallLayerMask))
                {
                    Flip();
                }
            }
            else
            {
                _animalRigidbody2D.velocity = new Vector2(Vector2.right.x * _animalSpeed, _animalRigidbody2D.velocity.y);

                if (Physics2D.Raycast(transform.position, Vector2.right, 1f, wallLayerMask))
                {
                    Flip();
                }
            }
        }
        
        private void Flip() => _animalIsMovingRight = !_animalIsMovingRight;
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.CompareTag("Proyectile")) return;

            _animalRigidbody2D.AddForce(new Vector2(0f, 6f), ForceMode2D.Impulse);
            _lives--;

            switch (_lives)
            {
                case 2:
                    live3.GetComponent<SpriteRenderer>().sprite = imageLowLife;
                    break;
                
                case 1:
                    live2.GetComponent<SpriteRenderer>().sprite = imageLowLife;
                    break;
                
                case 0:
                    AudioManager.Instance.PlaySound(animalAudio);
                    AnimalDelegate?.Invoke(animalScore);
                    Destroy(gameObject);
                    break;
            }
        }
        
    }
}
