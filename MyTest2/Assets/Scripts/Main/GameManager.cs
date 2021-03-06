﻿using mytest2.CameraSystem;
using mytest2.Character;
using mytest2.Main;
using mytest2.UI;
using mytest2.UI.InputSystem;
using mytest2.UI.Loading;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Prefabs")]
    public PrefabsLibrary PrefabsLibraryPrefab;
    [Header("References")]
    public UIManager UIManager;
    public CameraController CameraController;
    public GameStateController GameState;
    public Transform PlayerSpawnPoint;
    public Transform EnemySpawnPoint;
    public List<CreatureController> Enemies;

    private bool m_IsActive = false;
    private PrefabsLibrary m_PrefabsLibrary;

    public bool IsActive
    {
        get { return m_IsActive; }
    }
    public PrefabsLibrary PrefabLibrary
    {
        get { return m_PrefabsLibrary; }
    }

 
    public void FinishRound()
    {
        LevelLoader.Instance.LoadNextLevel();
    }

    public void RestartRound()
    {
        m_IsActive = false;

        GameState.GameOver();
    }


    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        CreateMainEntities();
        CreatePlayer();
        StartLoop();
    }


    void CreateMainEntities()
    {
        m_PrefabsLibrary = Instantiate(PrefabsLibraryPrefab);
    }

    void CreatePlayer()
    {
        GameState.Player = Instantiate(m_PrefabsLibrary.PlayerPrefab, PlayerSpawnPoint.position, Quaternion.identity);

        Enemies = new List<CreatureController>();
        CreatureController enemy = Instantiate(m_PrefabsLibrary.EnemyPrefab, EnemySpawnPoint.position, Quaternion.identity).GetComponent<CreatureController>();
        Enemies.Add(enemy);
    }

    void StartLoop()
    {
        CameraController.OnCameraArrived += () => InputManager.Instance.InputIsEnabled = true;
        CameraController.Init(GameState.Player.transform);

        m_IsActive = true;
    }
}
