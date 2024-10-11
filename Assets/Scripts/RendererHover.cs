using UnityEngine;

public class RendererHover : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;

    private void Start()
    {
        meshRenderer.enabled = false;
    }

    private void OnMouseEnter()
    {
        if (GameManager.instance.canGameInteract)
        {
            meshRenderer.enabled = true;
        }
    }

    private void OnMouseExit()
    {
        meshRenderer.enabled = false;
    }
}
