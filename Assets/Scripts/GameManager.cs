using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Settings")]
    [Range(0, 100)] public float diamondSpawnRate;
    public int dynamiteCount;
    private int startDynamiteCount;
    public int diamondCount { get; private set; }
    public bool canGameInteract { get; private set; }

    [Header("UI")]
    [SerializeField] private Text dynamiteUIText;
    [SerializeField] private Text diamondUIText;
    public GameObject diamondUIIcon;

    [Header("Explosion Holes")]
    [SerializeField] private ExplodingHole[] explosionHoles;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitializeGame();
    }

    private void InitializeGame()
    {
        startDynamiteCount = dynamiteCount;
        InitUI();
        InitGame();
    }

    private void InitUI()
    {
        UpdateDiamondUIText();
        UpdateDynamiteUIText();
    }

    private void UpdateDiamondUIText()
    {
        diamondUIText.text = diamondCount.ToString();
    }

    private void UpdateDynamiteUIText()
    {
        dynamiteUIText.text = dynamiteCount.ToString();
    }

    public void AddDiamond()
    {
        diamondCount++;
        UpdateDiamondUIText();
    }

    public void ReduceDynamite()
    {
        if (dynamiteCount > 0)
        {
            dynamiteCount--;
            UpdateDynamiteUIText();
        }
    }

    public void HideExplosionHoles()
    {
        foreach (var explodingHole in explosionHoles)
        {
            explodingHole.HideGameObject();
        }
    }

    public void InitGame()
    {
        foreach (var explodingHole in explosionHoles)
        {
            explodingHole.Init();
        }
    }

    public void RestartGame()
    {
        foreach (var explodingHole in explosionHoles)
        {
            explodingHole.GoToLevelOne();
        }

        dynamiteCount = startDynamiteCount;
        diamondCount = 0;

        InitUI();
    }

    public bool CheckData()
    {
        for (int i = 0; i < explosionHoles.Length; i++)
        {
            if (!HasKeyForHole(i))
            {
                return false;
            }
        }

        return true;
    }

    public void SetGameInteract(bool value)
    {
        canGameInteract = value;
    }

    private bool HasKeyForHole(int index)
    {
        return PlayerPrefs.HasKey("ExplodingHoleState_" + index);
    }

    public void LoadData()
    {
        for (int i = 0; i < explosionHoles.Length; i++)
        {
            if (HasKeyForHole(i))
            {
                explosionHoles[i].currentState = (ExplodingHole.State)PlayerPrefs.GetInt("ExplodingHoleState_" + i);
            }
        }

        dynamiteCount = PlayerPrefs.GetInt("ExplosionCount", startDynamiteCount);
        diamondCount = PlayerPrefs.GetInt("DiamondCount", 0);

        InitUI();
    }

    public void SaveData()
    {
        for (int i = 0; i < explosionHoles.Length; i++)
        {
            PlayerPrefs.SetInt("ExplodingHoleState_" + i, (int)explosionHoles[i].currentState);
        }

        PlayerPrefs.SetInt("ExplosionCount", dynamiteCount);
        PlayerPrefs.SetInt("DiamondCount", diamondCount);
        
        PlayerPrefs.Save();
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }
}
