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
    public static void Interact() => OnInteract?.Invoke();
}
