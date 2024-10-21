using UnityEngine;
using Zenject;

namespace ExplosiveMiner.Installers
{
    public class GameObjectsInstaller : MonoInstaller
    {
        [Header("Audio Source")]
        [SerializeField] private AudioSource _xpSound;
        [SerializeField] private AudioSource _diggingSound;

        [Header("UI")]
        [SerializeField] private GameObject _diamondAreaUI;

        public override void InstallBindings()
        {
            // --- Sounds --- //
            Container.Bind<AudioSource>().WithId("XPSound").FromInstance(_xpSound);
            Container.Bind<AudioSource>().WithId("DiggingSound").FromInstance(_diggingSound);

            // --- UI --- //
            Container.Bind<GameObject>().WithId("DiamondAreaUI").FromInstance(_diamondAreaUI);
        }
    }
}