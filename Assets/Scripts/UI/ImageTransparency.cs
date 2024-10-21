using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

namespace ExplosiveMiner.UI
{
    [RequireComponent(typeof(Image))]
    public class ImageTransparency : MonoBehaviour
    {
        [SerializeField] private float _fadeDuration = 1f;
        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        private void OnEnable()
        {
            FadeInImage().Forget();
        }

        private async UniTask FadeInImage()
        {
            Color startColor = _image.color;
            startColor.a = 0f;
            _image.color = startColor;
            float elapsedTime = 0f;

            while (elapsedTime < _fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                float newAlpha = Mathf.Lerp(0f, 1f, elapsedTime / _fadeDuration);
                _image.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);
                
                await UniTask.Yield();
            }

            _image.color = new Color(startColor.r, startColor.g, startColor.b, 1f);
        }
    }
}