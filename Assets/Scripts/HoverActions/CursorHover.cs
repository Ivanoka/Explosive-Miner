using UnityEngine;
using Zenject;

namespace ExplosiveMiner.HoverActions
{
    public class CursorHover : MonoBehaviour
    {
        [Inject] private Managers.GameManagerController _gameManager;

        [SerializeField] private Texture2D _customCursor;
        [SerializeField] private Vector2 _hotspot;
        [SerializeField] private CursorMode _cursorMode;

        void OnMouseEnter()
        {
            if (_gameManager.CanGameInteract)
            {
                Cursor.SetCursor(_customCursor, _hotspot, _cursorMode);
            }
        }

        void OnMouseExit()
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }
}