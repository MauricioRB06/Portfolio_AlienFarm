
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// MIT License < Please Read LICENSE.md >
// 
// The Purpose Of This Script Is:
//
//  Set the behavior of the projectiles.
//
// Documentation and References:
//
//  Unity FixedUpdate: https://docs.unity3d.com/ScriptReference/MonoBehaviour.FixedUpdate.html
//  Unity OnCollisionEnter2D: https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnCollisionEnter2D.html
// 
// -----------------------------
// Last Update: 14/11/2022 By MauricioRB06

using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(CircleCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Proyectile : MonoBehaviour
    {
        private void FixedUpdate() => transform.Rotate(Vector3.forward * 5);
        private void OnCollisionEnter2D() => Destroy(gameObject);

    }
}
