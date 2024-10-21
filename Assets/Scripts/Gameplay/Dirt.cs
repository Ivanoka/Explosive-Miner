using UnityEngine;
using Zenject;

namespace ExplosiveMiner.Gameplay
{
    public class Dirt : MonoBehaviour
    {
        [SerializeField] private GameObject _diamondPrefab;
        [Inject(Id = "DiggingSound")] private AudioSource _diggingSound;
        [Inject] private DiContainer _diContainer;
        [Inject] private Managers.GameManagerController _gameManager;

        private void OnMouseUp()
        {
            if (_gameManager.CanGameInteract)
            {
                TryDig();
            }
        }

        private void TryDig()
        {
            if (!_gameManager.CanUseShovel()) return;

            PlayDiggingSound();
            _gameManager.UseShovel();
            SpawnDiamond();
            HideDirt();
        }

        private void PlayDiggingSound()
        {
            _diggingSound.pitch = Random.Range(0.8f, 1.2f);
            _diggingSound.Play();
        }

        private void SpawnDiamond()
        {
            var diamond = _diContainer.InstantiatePrefab(_diamondPrefab, transform.position, Quaternion.identity, null);
            _gameManager.diamondObjects.Add(diamond);
        }

        private void HideDirt()
        {
            gameObject.SetActive(false);
        }
    }
}