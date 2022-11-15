
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// MIT License < Please Read LICENSE.md >
// 
// The Purpose Of This Script Is:
//
//  Set an access to modify the EventSystem target when there is a change in the user interface and it allows
//  navigating with a GamePad correctly.
//
// Documentation and References:
//
//  Unity EventSystem: https://docs.unity3d.com/es/2021.1/Manual/EventSystem.html
// 
// -----------------------------
// Last Update: 14/11/2022 By MauricioRB06

using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class EventSystemSelector : MonoBehaviour
    {
        
        public void SetSelectedObject(GameObject selectedObject) => EventSystem.current.SetSelectedGameObject(selectedObject);

    }
}
