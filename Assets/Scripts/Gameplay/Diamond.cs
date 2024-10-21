using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace ExplosiveMiner.Gameplay
{
    public class Diamond : MonoBehaviour
    {
        [Inject(Id = "DiamondAreaUI")] private GameObject _diamondAreaUI;
        [Inject(Id = "XPSound")] private AudioSource _xpSound;
        [Inject] private Managers.GameManagerController _gameManager;
        
        private Camera _mainCamera;
        private Vector3 _dragOffset;
        private bool _isDragging = false;

        void Start()
        {
            _mainCamera = Camera.main;
        }

        void OnMouseDown()
        {
            if (_gameManager.CanGameInteract)
            {
                StartDragging();
            }
        }

        void OnMouseDrag()
        {
            if (_gameManager.CanGameInteract)
            {
                if (_isDragging) UpdateDragPosition();
            }
        }

        void OnMouseUp()
        {
            StopDragging();
            HandleDrop();
        }

        private void StartDragging()
        {
            _isDragging = true;
            _dragOffset = transform.position - GetMouseWorldPosition();
        }

        private void UpdateDragPosition()
        {
            transform.position = GetMouseWorldPosition() + _dragOffset;
        }

        private void StopDragging()
        {
            _isDragging = false;
        }

        private void HandleDrop()
        {
            if (IsObjectOverUIArea())
            {
                PlayXPSound();
                _gameManager.AddDiamond();
                Destroy(gameObject);
            }
        }

        private void PlayXPSound()
        {
            _xpSound.pitch = Random.Range(0.9f, 1.1f);
            _xpSound.Play();
        }

        private Vector3 GetMouseWorldPosition()
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = _mainCamera.WorldToScreenPoint(transform.position).z;
            return _mainCamera.ScreenToWorldPoint(mousePosition);
        }

        private bool IsObjectOverUIArea()
        {
            var eventData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };

            var raycastResults = new System.Collections.Generic.List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, raycastResults);

            return raycastResults.Exists(result => result.gameObject == _diamondAreaUI);
        }
    }
}