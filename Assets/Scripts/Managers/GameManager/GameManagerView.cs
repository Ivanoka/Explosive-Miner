using UnityEngine;
using UnityEngine.UI;

namespace ExplosiveMiner.Managers
{
    public class GameManagerView : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private Text shovelTextUI;
        [SerializeField] private Text diamondTextUI;
        [SerializeField] private GameObject suggestionRestartUI;

        [Header("Camera")]
        [SerializeField] private GameObject cameraObject;

        public void UpdateShovelText(int shovelCount)
        {
            shovelTextUI.text = shovelCount.ToString();
        }

        public void UpdateDiamondText(int diamondCount)
        {
            diamondTextUI.text = diamondCount.ToString();
        }

        public void ShowRestartSuggestion(bool show)
        {
            suggestionRestartUI.SetActive(show);
        }

        public void CenterCamera(int matrixWidth, int matrixHeight, int matrixDepth)
        {
            cameraObject.transform.position = new Vector3(matrixWidth / 2, matrixHeight / 2, matrixDepth / 2);
        }
    }
}