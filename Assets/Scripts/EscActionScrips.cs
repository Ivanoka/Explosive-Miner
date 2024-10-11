using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class EscActionScrips : MonoBehaviour
{
    [SerializeField] private UnityEvent unityEvent;
    
    private EscInput escInput;

    private void Awake()
    {
        escInput = new EscInput();
    }

    private void OnEnable()
    {
        escInput.Enable();
        escInput.EscMenu.BackMenu.performed += EscAction;
    }

    private void OnDisable()
    {
        escInput.EscMenu.BackMenu.performed -= EscAction;
        escInput.EscMenu.Disable();
    }

    private void EscAction(InputAction.CallbackContext callbackContext)
    {
         unityEvent?.Invoke();
    }
}
