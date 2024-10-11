using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageTransparency : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 1f;

    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void OnEnable()
    {
        StartCoroutine(FadeInImage());
    }

    private IEnumerator FadeInImage()
    {
        Color startColor = image.color;
        startColor.a = 0f;
        image.color = startColor;

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;

            float newAlpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);

            image.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);

            yield return null;
        }

        image.color = new Color(startColor.r, startColor.g, startColor.b, 1f);
    }
}
