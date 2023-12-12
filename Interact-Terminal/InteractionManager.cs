using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionManager : MonoBehaviour
{
    public delegate void InteractAction();
    public static event InteractAction OnInteract;
    
    //Event
    // public void Interact(InputAction.CallbackContext ctx)
    // {
    //     if (ctx.performed)
    //         OnInteract?.Invoke();
    // }
    public static void Interact() => OnInteract?.Invoke();
}