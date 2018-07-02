// Enemy controller.
// Created on Sat Jun 30 19:00:00 2018
// Author: Prasun Roy (https://github.com/prasunroy)
// GitHub: https://github.com/prasunroy/galaxy-shooter

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private bool _debug = false;
    [SerializeField]
    private float _minimumMovementSpeed = 1.0f;
    [SerializeField]
    private float _maximumMovementSpeed = 1.0f;
    private float _movementSpeed = 0.0f;
    [SerializeField]
    private float _visibleBoundary_xmin = -5.0f;
    [SerializeField]
    private float _visibleBoundary_xmax = 5.0f;
    [SerializeField]
    private float _visibleBoundary_ymin = -5.0f;
    [SerializeField]
    private float _visibleBoundary_ymax = 5.0f;
    [SerializeField]
    private float _spawnOffsetX = 0.0f;
    [SerializeField]
    private float _spawnOffsetY = 0.0f;
    [SerializeField]
    private int _enemyLives = 1;
    [SerializeField]
    private int _damageToPlayer = 1;
    [SerializeField]
    private int _damageToEnemy = 1;
    [SerializeField]
    private GameObject _enemyDestroyedPrefab;

    // Initialize
    void Start ()
    {
        // Debug message
        if (_debug)
        {
            Debug.Log("[INFO] EnemyController initialized");
        }

        // Control speed
        SpeedController();
    }
    
    // Update
	void Update ()
    {
        // Control movement
        MovementController();
    }

    // SpeedController
    private void SpeedController()
    {
        _movementSpeed = Random.Range(_minimumMovementSpeed, _maximumMovementSpeed);
    }

    // MovementController
    private void MovementController()
    {
        // Movement in xy plane
        transform.Translate(Vector3.down * _movementSpeed * Time.deltaTime);

        // Respawn if out of visible boundary
        if (transform.position.x < _visibleBoundary_xmin ||
            transform.position.x > _visibleBoundary_xmax ||
            transform.position.y < _visibleBoundary_ymin)
        {
            float x = Random.Range(_visibleBoundary_xmin + _spawnOffsetX, _visibleBoundary_xmax - _spawnOffsetX);
            float y = _visibleBoundary_ymax + _spawnOffsetY;
            float z = transform.position.z;
            transform.position = new Vector3(x, y, z);
            SpeedController();
        }
    }

    // Triggered collision
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Collision with player
        if (other.tag == "Player")
        {
            // Get player reference
            PlayerController playerController = other.GetComponent<PlayerController>();
            
            // Check player reference
            if (playerController != null)
            {
                // Damage player
                playerController.DamagePlayer(_damageToPlayer);

                // Damage enemy
                DamageEnemy(_damageToEnemy);
            }
        }
        // Collision with laser
        else if (other.tag == "Laser")
        {
            // Damage laser
            Destroy(other.gameObject);

            // Damage enemy
            DamageEnemy(_damageToEnemy);
        }
    }

    // DamageEnemy
    public void DamageEnemy(int damage = 0)
    {
        _enemyLives -= damage;
        if (_enemyLives <= 0)
        {
            Instantiate(_enemyDestroyedPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
