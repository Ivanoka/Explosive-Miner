using UnityEngine;
using UnityEngine.Events;

namespace ExplosiveMiner.InputHandlers
{
    public class EscapeKeyHandler : MonoBehaviour
    {
        [SerializeField] private UnityEvent _unityEvent;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) HandleEscape();
        }

        private void HandleEscape()
        {
            _unityEvent?.Invoke();
        }
    }
}