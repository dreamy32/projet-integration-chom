using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DoorComponent : MonoBehaviour
{
    [SerializeField] InterrupterComponent[] interrupterComponents;
    private Animator _animator;
    private bool isOpen;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        foreach (var interrupter in interrupterComponents)
        {
            interrupter.OnPress += () =>
            {
                isOpen = !isOpen;
                _animator.SetBool("isOpen", isOpen);
            };
        }
        
    }
}
