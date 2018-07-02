// Player controller.
// Created on Sun Jun 24 10:00:00 2018
// Author: Prasun Roy (https://github.com/prasunroy)
// GitHub: https://github.com/prasunroy/galaxy-shooter

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private bool _debug = false;
    [SerializeField]
    private float _movementSpeed = 1.0f;
    [SerializeField]
    private float _movementBound_xmin = -5.0f;
    [SerializeField]
    private float _movementBound_xmax = 5.0f;
    [SerializeField]
    private float _movementBound_ymin = -5.0f;
    [SerializeField]
    private float _movementBound_ymax = 5.0f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _laserCooldown = 0.0f;
    private float _laserActivationTime = 0.0f;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private float _tripleShotCooldown = 0.0f;
    [SerializeField]
    private bool _tripleShotActive = false;
    [SerializeField]
    private float _speedMultiplier = 1.0f;
    [SerializeField]
    private float _speedCooldown = 0.0f;
    [SerializeField]
    private bool _speedActive = false;
    [SerializeField]
    private GameObject _shieldPrefab;
    [SerializeField]
    private float _shieldCooldown = 0.0f;
    [SerializeField]
    private bool _shieldActive = false;
    [SerializeField]
    private int _playerLives = 1;
    [SerializeField]
    private GameObject _explosionPrefab;

    // Initialize
    private void Start()
    {
        // Debug message
        if (_debug)
        {
            Debug.Log("[INFO] PlayerController initialized");
        }
    }
    
    // Update
    private void Update()
    {
        // Control movement
        MovementController();

        // Control laser
        LaserController();
    }

    // MovementController
    private void MovementController()
    {
        // Input parameters
        float input_xaxis = Input.GetAxis("Horizontal");
        float input_yaxis = Input.GetAxis("Vertical");

        // Movement in xy plane
        // Speed boost
        if (_speedActive)
        {
            transform.Translate(Vector3.right * _movementSpeed * _speedMultiplier * input_xaxis * Time.deltaTime);
            transform.Translate(Vector3.up * _movementSpeed * _speedMultiplier * input_yaxis * Time.deltaTime);
        }
        // Speed normal
        else
        {
            transform.Translate(Vector3.right * _movementSpeed * input_xaxis * Time.deltaTime);
            transform.Translate(Vector3.up * _movementSpeed * input_yaxis * Time.deltaTime);
        }

        // Movement boundary
        if (transform.position.x < _movementBound_xmin)
        {
            transform.position = new Vector3(_movementBound_xmin, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > _movementBound_xmax)
        {
            transform.position = new Vector3(_movementBound_xmax, transform.position.y, transform.position.z);
        }

        if (transform.position.y < _movementBound_ymin)
        {
            transform.position = new Vector3(transform.position.x, _movementBound_ymin, transform.position.z);
        }
        else if (transform.position.y > _movementBound_ymax)
        {
            transform.position = new Vector3(transform.position.x, _movementBound_ymax, transform.position.z);
        }
    }

    // LaserController
    private void LaserController()
    {
        // Check input
        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
        {
            // Check laser activation time
            if (Time.time > _laserActivationTime)
            {
                // Instantiate triple shot
                if (_tripleShotActive)
                {
                    Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
                }
                // Instantiate laser
                else
                {
                    Instantiate(_laserPrefab, transform.position, Quaternion.identity);
                }
                
                // Update laser activation time
                _laserActivationTime = Time.time + _laserCooldown;
            }
        }
    }

    // PowerUpController
    public void PowerUpController(int powerUpID = 0)
    {
        // Enable power up
        // 0 = None, 1 = TripleShot, 2 = Speed, 3 = Shield
        switch (powerUpID)
        {
            case 1:
                _tripleShotActive = true;
                break;
            case 2:
                _speedActive = true;
                break;
            case 3:
                _shieldActive = true;
                break;
            default:
                break;
        }

        // Start power up cooldown coroutine
        StartCoroutine(PowerUpCooldown(powerUpID));
    }

    // PowerUpCooldown coroutine
    private IEnumerator PowerUpCooldown(int powerUpID = 0)
    {
        // Wait for cooldown
        // Disable power up
        // 0 = None, 1 = TripleShot, 2 = Speed, 3 = Shield
        switch (powerUpID)
        {
            case 1:
                yield return new WaitForSeconds(_tripleShotCooldown);
                _tripleShotActive = false;
                break;
            case 2:
                yield return new WaitForSeconds(_speedCooldown);
                _speedActive = false;
                break;
            case 3:
                yield return new WaitForSeconds(_shieldCooldown);
                _shieldActive = false;
                break;
            default:
                break;
        }
    }

    // DamagePlayer
    public void DamagePlayer(int damage = 0)
    {
        // Disable shield if enabled
        if (_shieldActive)
        {
            _shieldActive = false;
        }
        // Damage player if shield is disabled
        else
        {
            _playerLives -= damage;
        }
        if (_playerLives <= 0)
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
