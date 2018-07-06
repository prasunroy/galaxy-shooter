// Spawn controller.
// Created on Mon Jul  2 22:00:00 2018
// Author: Prasun Roy (https://github.com/prasunroy)
// GitHub: https://github.com/prasunroy/galaxy-shooter

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField]
    private bool _debug = false;
    [SerializeField]
    private float _visibleBoundary_xmin = -5.0f;
    [SerializeField]
    private float _visibleBoundary_xmax = 5.0f;
    [SerializeField]
    private float _visibleBoundary_ymin = -5.0f;
    [SerializeField]
    private float _visibleBoundary_ymax = 5.0f;
    [SerializeField]
    private GameObject[] _enemyPrefabs;
    [SerializeField]
    private float _enemySpawnDelay = 60.0f;
    [SerializeField]
    private float _enemySpawnOffsetX = 0.0f;
    [SerializeField]
    private float _enemySpawnOffsetY = 0.0f;
    [SerializeField]
    private GameObject[] _powerUpPrefabs;
    [SerializeField]
    private float _powerUpSpawnDelay = 60.0f;
    [SerializeField]
    private float _powerUpSpawnOffsetX = 0.0f;
    [SerializeField]
    private float _powerUpSpawnOffsetY = 0.0f;
    private GameController _gameController;

    // Initialize
    void Start()
    {
        // Debug message
        if (_debug)
        {
            Debug.Log("[INFO] SpawnController initialized");
        }

        // Get game controller reference
        _gameController = GameObject.Find("GameManager").GetComponent<GameController>();
    }

    // Start spawn controller coroutines
    public void StartSpawnControllerCoroutines()
    {
        if (_gameController != null)
        {
            // Start enemy spawn controller coroutine
            StartCoroutine(EnemySpawn());

            // Start power up spawn controller coroutine
            StartCoroutine(PowerUpSpawn());
        }
    }
    
    // EnemySpawn coroutine
    private IEnumerator EnemySpawn()
    {
        while (!_gameController.gameOver)
        {
            yield return new WaitForSeconds(_enemySpawnDelay);

            float x = Random.Range(_visibleBoundary_xmin + _enemySpawnOffsetX, _visibleBoundary_xmax - _enemySpawnOffsetX);
            float y = _visibleBoundary_ymax + _enemySpawnOffsetY;
            float z = transform.position.z;

            Instantiate(_enemyPrefabs[Random.Range(0, _enemyPrefabs.Length)], new Vector3(x, y, z), Quaternion.identity);
        }
    }

    // PowerUpSpawn coroutine
    private IEnumerator PowerUpSpawn()
    {
        while (!_gameController.gameOver)
        {
            yield return new WaitForSeconds(_powerUpSpawnDelay);

            float x = Random.Range(_visibleBoundary_xmin + _powerUpSpawnOffsetX, _visibleBoundary_xmax - _powerUpSpawnOffsetX);
            float y = _visibleBoundary_ymax + _powerUpSpawnOffsetY;
            float z = transform.position.z;

            Instantiate(_powerUpPrefabs[Random.Range(0, _powerUpPrefabs.Length)], new Vector3(x, y, z), Quaternion.identity);
        }
    }
}
