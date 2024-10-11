using UnityEngine;

public class OutlineHover : MonoBehaviour
{
    [SerializeField] private Material outlineMaterial;
    [SerializeField] private GameObject targetObject;
    private MeshRenderer objectRenderer;
    private Material[] originalMaterials;

    void Start()
    {
        objectRenderer = targetObject.GetComponent<MeshRenderer>();
        originalMaterials = objectRenderer.materials;
    }

    void OnMouseEnter()
    {
        if (GameManager.instance.canGameInteract)
        {
            Material[] newMaterials = new Material[originalMaterials.Length + 1];

            for (int i = 0; i < originalMaterials.Length; i++)
            {
                newMaterials[i] = originalMaterials[i];
            }

            newMaterials[originalMaterials.Length] = outlineMaterial;

            objectRenderer.materials = newMaterials;
        }
    }

    void OnMouseExit()
    {
        objectRenderer.materials = originalMaterials;
    }
}
