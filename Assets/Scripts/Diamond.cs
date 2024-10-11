using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Diamond : MonoBehaviour
{
    public event Action OnDestroyed;

    public GameObject uiElement;
    [SerializeField] private float flySmoothTime;
    [SerializeField] private float distanceToDestroy;

    private Vector3 uiElementPoint;
    private Vector3 scaleVelocity = Vector3.zero;

    private void OnMouseDown()
    {
        if (GameManager.instance.canGameInteract)
        {
            GetComponent<Rigidbody>().isKinematic = true;

            uiElementPoint = uiElement.transform.position;
            uiElementPoint = Camera.main.ScreenToWorldPoint(uiElementPoint);

            GameManager.instance.AddDiamond();

            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
            audioSource.Play();

            StartCoroutine(FlyToIcon());
        }
    }

    private IEnumerator FlyToIcon()
    {
        while (Vector3.Distance(transform.position, uiElementPoint) >= distanceToDestroy)
        {
            transform.position = Vector3.SmoothDamp(transform.position, uiElementPoint, ref scaleVelocity, flySmoothTime);
            yield return null;
        }

        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        OnDestroyed?.Invoke();
    }
}
