using System.Collections;
using UnityEngine;

public class AudioSourceFadeIn : MonoBehaviour
{
    [SerializeField] private float fadeInDuration;
    [SerializeField] private float fadeOutDuration;
    [SerializeField, Range(0.0f, 1.0f)] private float targetVolume; 
    [SerializeField] private bool playOnAwake;

    public AudioSource audioSource;

    private void Start()
    {
        if (playOnAwake)
        {
            Play();
        }
    }

    public void Play()
    {
        audioSource.volume = 0.0f;
        audioSource.Play();

        StartCoroutine(FadeInAudio());
    }

    public void Stop()
    {
        StopAllCoroutines();

        StartCoroutine(FadeOutAudio());
    }

    private IEnumerator FadeInAudio()
    {
        float elapsedTime = 0.0f;

        while (elapsedTime < fadeInDuration)
        {
            elapsedTime += Time.deltaTime;

            audioSource.volume = Mathf.Lerp(0.0f, targetVolume, elapsedTime / fadeInDuration);

            yield return null;
        }

        audioSource.volume = targetVolume;
    }

    private IEnumerator FadeOutAudio()
    {
        float elapsedTime = 0.0f;
        float startVolume = audioSource.volume;

        while (elapsedTime < fadeOutDuration)
        {
            elapsedTime += Time.deltaTime;

            audioSource.volume = Mathf.Lerp(startVolume, 0.0f, elapsedTime / fadeOutDuration);

            yield return null;
        }

        audioSource.volume = 0f;
        audioSource.Stop();
    }
}
