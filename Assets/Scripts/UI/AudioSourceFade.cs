using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace ExplosiveMiner.Sound
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioSourceFade : MonoBehaviour
    {
        [SerializeField] private float _fadeInDuration;
        [SerializeField] private float _fadeOutDuration;
        [SerializeField, Range(0.0f, 1.0f)] private float _targetVolume;
        [SerializeField] private bool _canPlayOnAwake;

        private AudioSource _audioSource;
        private CancellationTokenSource _cancellationTokenSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            if (_canPlayOnAwake) Play();
        }

        public void Play()
        {
            _audioSource.volume = 0.0f;
            _audioSource.Play();
            _cancellationTokenSource = new CancellationTokenSource();
            FadeInAudio(_cancellationTokenSource.Token).Forget();
        }

        public void Stop()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
            FadeOutAudio(_cancellationTokenSource.Token).Forget();
        }

        private async UniTask FadeInAudio(CancellationToken cancellationToken)
        {
            float elapsedTime = 0.0f;

            while (elapsedTime < _fadeInDuration)
            {
                cancellationToken.ThrowIfCancellationRequested();

                elapsedTime += Time.deltaTime;
                _audioSource.volume = Mathf.Lerp(0.0f, _targetVolume, elapsedTime / _fadeInDuration);
                await UniTask.Yield();
            }

            _audioSource.volume = _targetVolume;
        }

        private async UniTask FadeOutAudio(CancellationToken cancellationToken)
        {
            float elapsedTime = 0.0f;
            float startVolume = _audioSource.volume;

            while (elapsedTime < _fadeOutDuration)
            {
                cancellationToken.ThrowIfCancellationRequested();

                elapsedTime += Time.deltaTime;
                _audioSource.volume = Mathf.Lerp(startVolume, 0.0f, elapsedTime / _fadeOutDuration);
                await UniTask.Yield();
            }

            _audioSource.volume = 0f;
            _audioSource.Stop();
        }
    }
}