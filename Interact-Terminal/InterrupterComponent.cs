using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider))]
public class InterrupterComponent : MonoBehaviour
{
    //Custom Events
    public delegate void OnPressAction();
    public event OnPressAction OnPress;

    //Serialized Attributes
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Outline[] outlines;
    [Header("Debug")] [SerializeField] private bool triggerEnter;

    //
    private RaycastHit hit;
    private bool isTriggered;
    private bool canPress;
    private Camera camera;

    private void Awake()
    {
        camera = Camera.main;
        InteractionManager.OnInteract += Interact;
        foreach (var outline in outlines)
            outline.enabled = false;
    }

    private void OnTriggerEnter(Collider c)
    {
        if (GameTools.CompareLayers(layerMask, c.gameObject.layer))
        {
            isTriggered = true;
        }

        if (triggerEnter)
            Debug.Log(isTriggered);
    }

    private void OnTriggerExit(Collider c)
    {
        if (GameTools.CompareLayers(layerMask, c.gameObject.layer))
        {
            isTriggered = false;
            canPress = false;
            foreach (var outline in outlines)
                outline.enabled = false;
        }

        if (triggerEnter)
            Debug.Log(isTriggered);
    }

    private void Update()
    {
        if (isTriggered)
        {
            if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag("Interruptor Pad"))
                {
                    canPress = true;
                    foreach (var outline in outlines)
                    {
                        if (!outline.enabled)
                            outline.enabled = true;
                    }
                }
                else
                {
                    foreach (var outline in outlines)
                    {
                        if (!outline.enabled)
                            outline.enabled = false;
                    }
                }
            }
        }
    }

    public void Interact()
    {
        if (canPress)
        {
            OnPress?.Invoke();
        }
    }
}
