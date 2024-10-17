using UnityEngine;
using UnityEngine.Events;

public class EscapeKeyHandler : MonoBehaviour
{
    [SerializeField] private UnityEvent unityEvent;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HandleEscape();
        }
    }

    private void HandleEscape()
    {
        unityEvent?.Invoke();
    }
}
