﻿using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _currentLevel = 1;
    [SerializeField] private GridManager _gridManager;

    private bool _gameOver = false;

    private LevelData _ld;

    // Start is called before the first frame update
    void Start()
    {
        PoolManager.Init();
        _ld = ResourceManager.LoadLevel(_currentLevel);
        _gridManager.Init(_ld);
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameOver)
        {
            // Cleanup

            return;
        }

        // Draw grid

        // Spawn enemies

        // Spawn towers

        // Tick towers

        // Damage enemies

        // Remove enemies
    }
}