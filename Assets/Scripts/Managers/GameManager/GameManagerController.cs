using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Zenject;

namespace ExplosiveMiner.Managers
{
    [RequireComponent(typeof(GameManagerView))]
    public class GameManagerController : MonoBehaviour
    {
        private const string ConfigFileName = "config";

        [Header("Prefabs")]
        [SerializeField] private GameObject dirtPrefab;
        [Inject] private DiContainer diContainer;

        private GameManagerModel _model;
        [Inject] private GameManagerView _view;
        private string _saveFilePath;
        private string _configFilePath;
        public bool CanGameInteract { get; private set; }
        private List<GameObject> _dirtMatrix = new List<GameObject>();
        [HideInInspector] public List<GameObject> diamondObjects = new List<GameObject>();

        private void Start()
        {
            _model = new GameManagerModel(10, 0, 50.0f, 3, 3, 3);
            _configFilePath = Application.dataPath + "/" + ConfigFileName + ".json";
        }

        public void StartGame()
        {
            LoadConfigData();
            ResetMatrix();
            _model.ShovelCount = _model.StartShovelCount;
            InitUI();
            _view.CenterCamera(_model.MatrixWidth, _model.MatrixHeight, _model.MatrixDepth);
        }

        public void ResumeGame()
        {
            LoadConfigData();
            ResetMatrix();
            LoadGameData();
            InitUI();
            _view.CenterCamera(_model.MatrixWidth, _model.MatrixHeight, _model.MatrixDepth);
        }

        public void RestartGame()
        {
            ResetMatrixState();
            ClearDiamonds();
            _model.ShovelCount = _model.StartShovelCount;
            _model.DiamondCount = 0;
            InitUI();
        }

        public void EndGame()
        {
            SaveGameData();
            ClearMatrix();
            ClearDiamonds();
        }

        private void InitUI()
        {
            _view.UpdateDiamondText(_model.DiamondCount);
            _view.UpdateShovelText(_model.ShovelCount);
        }

        public void AddDiamond()
        {
            _model.DiamondCount++;
            _view.UpdateDiamondText(_model.DiamondCount);
        }

        public bool CanUseShovel()
        {
            if (!CanGameInteract) return false;
            if (_model.ShovelCount > 0)
            {
                return true;
            }
            _view.ShowRestartSuggestion(true);
            return false;
        }

        public void UseShovel()
        {
            if (_model.ShovelCount > 0)
            {
                _model.ShovelCount--;
                _view.UpdateShovelText(_model.ShovelCount);
            }
        }

        public void SetGameInteract(bool value)
        {
            CanGameInteract = value;
        }

        private void ResetMatrix()
        {
            ClearMatrix();

            for (int x = 0; x < _model.MatrixWidth; x++)
            {
                for (int y = 0; y < _model.MatrixHeight; y++)
                {
                    for (int z = 0; z < _model.MatrixDepth; z++)
                    {
                        _dirtMatrix.Add(diContainer.InstantiatePrefab(dirtPrefab, new Vector3(x, y, z), Quaternion.identity, null));
                    }
                }
            }
        }

        private void ResetMatrixState()
        {
            foreach (var dirtObject in _dirtMatrix)
            {
                dirtObject.SetActive(true);
            }
        }

        private void ClearMatrix()
        {
            foreach (var dirtObject in _dirtMatrix)
            {
                Destroy(dirtObject);
            }
            _dirtMatrix.Clear();
        }

        private void ClearDiamonds()
        {
            foreach (var diamondObject in diamondObjects)
            {
                Destroy(diamondObject);
            }
            diamondObjects.Clear();
        }

        private void SaveMatrixState()
        {
            _model.DirtStateMatrix.Clear();

            foreach (var dirt in _dirtMatrix)
            {
                _model.DirtStateMatrix.Add(dirt.activeSelf ? 1 : 0);
            }
        }

        private void LoadMatrixState()
        {
            if (_model.DirtStateMatrix.Count == _dirtMatrix.Count)
            {
                for (int i = 0; i < _model.DirtStateMatrix.Count; i++)
                {
                    _dirtMatrix[i].SetActive(_model.DirtStateMatrix[i] == 1);
                }
            }
        }

        public void SaveGameData()
        {
            SaveMatrixState();

            if (_model.DirtStateMatrix.Count != 0)
            {
                try
                {
                    Serialization.GameData saveFileData = new Serialization.GameData(_model.DiamondCount, _model.ShovelCount, _model.DirtStateMatrix);
                    Serialization.GameData.SaveData(saveFileData);
                }
                catch (System.Exception ex)
                {
                    Debug.LogWarning("Error while saving data: " + ex.Message);
                }
            }
        }

        private void LoadGameData()
        {
            try
            {
                Serialization.GameData saveFileData = Serialization.GameData.LoadData();

                _model.DiamondCount = saveFileData.diamondCount;
                _model.ShovelCount = saveFileData.shovelCount;
                _model.DirtStateMatrix = saveFileData.dirtStateMatrix;

                LoadMatrixState();

                Debug.Log("The data has been successfully uploaded.");
                
            }
            catch (System.Exception ex)
            {
                Debug.LogWarning("Error while loading data: " + ex.Message);
            }
        }

        private void LoadConfigData()
        {
            try
            {
                if (System.IO.File.Exists(_configFilePath))
                {
                    string jsonData = System.IO.File.ReadAllText(_configFilePath);
                    Serialization.GameConfig configFileData = JsonConvert.DeserializeObject<Serialization.GameConfig>(jsonData);

                    _model.MatrixWidth = configFileData.matrixWidth;
                    _model.MatrixHeight = configFileData.matrixHeight;
                    _model.MatrixDepth = configFileData.matrixDepth;
                    _model.StartShovelCount = configFileData.startShovelCount;
                    _model.DiamondSpawnRate = configFileData.diamondSpawnRate;

                    Debug.Log("The config has been successfully uploaded.");
                }
                else
                {
                    Serialization.GameConfig configFileData = new Serialization.GameConfig(_model.MatrixWidth, _model.MatrixHeight, _model.MatrixDepth, _model.StartShovelCount, _model.DiamondSpawnRate);
                    string jsonData = JsonConvert.SerializeObject(configFileData, Formatting.Indented);
                    System.IO.File.WriteAllText(_configFilePath, jsonData);
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogWarning("Error while loading config: " + ex.Message);
            }
        }

        private void OnApplicationQuit()
        {
            SaveGameData();
        }
    }
}