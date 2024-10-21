using UnityEngine;
using Zenject;

namespace ExplosiveMiner.Installers
{
    public class GameManagerInstaller : MonoInstaller
    {
        [SerializeField] private Managers.GameManagerController _gameManagerController;
        [SerializeField] private Managers.GameManagerView _gameManagerView;
    
        public override void InstallBindings()
        {
            Container.Bind<Managers.GameManagerController>().FromInstance(_gameManagerController).AsSingle();
            Container.Bind<Managers.GameManagerView>().FromInstance(_gameManagerView).AsSingle();
        }
    }
}