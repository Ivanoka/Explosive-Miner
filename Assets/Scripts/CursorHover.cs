using UnityEngine;

public class CursorHover : MonoBehaviour
{
    [SerializeField] private Texture2D customCursor;
    [SerializeField] private Vector2 hotspot;
    [SerializeField] private CursorMode cursorMode = CursorMode.Auto;

    void OnMouseEnter()
    {
        if (GameManager.instance.canGameInteract)
        {
            Cursor.SetCursor(customCursor, hotspot, cursorMode);
        }
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
