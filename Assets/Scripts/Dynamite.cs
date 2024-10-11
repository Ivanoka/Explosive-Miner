using System.Collections;
using UnityEngine;

public class Dynamite : MonoBehaviour
{
    [SerializeField] private MeshRenderer objectRenderer;
    [SerializeField] private int blinkCount;
    [SerializeField] private float blinkInterval;
    [SerializeField] private float finalDelay;
    [SerializeField] private Vector3 finalScale;
    [SerializeField] private Material emissionMaterial;
    [SerializeField] private GameObject explosionParticlePrefab;

    private Material originalMaterial;

    private void Awake()
    {
        originalMaterial = objectRenderer.material;
    }

    public IEnumerator BlinkAndDestroy()
    {
        yield return new WaitForSeconds(blinkInterval);

        for (int i = 0; i < blinkCount; i++)
        {
            objectRenderer.material = emissionMaterial;

            yield return new WaitForSeconds(blinkInterval);

            objectRenderer.material = originalMaterial;

            yield return new WaitForSeconds(blinkInterval);
        }

        objectRenderer.material = emissionMaterial;

        yield return ScaleObject(finalScale, finalDelay);

        Instantiate(explosionParticlePrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

    private IEnumerator ScaleObject(Vector3 targetScale, float duration)
    {
        Vector3 startScale = transform.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(startScale, targetScale, elapsedTime / duration);

            yield return null;

            elapsedTime += Time.deltaTime;
        }

        transform.localScale = targetScale;
    }
}
