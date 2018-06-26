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
    private bool _debug = false;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private Vector3 _relativePositionOfLaser = Vector3.zero;
    [SerializeField]
    private float _laserCooldown = 0.0f;

    private float _laserActivationTime = 0.0f;

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
        transform.Translate(Vector3.right * _movementSpeed * input_xaxis * Time.deltaTime);
        transform.Translate(Vector3.up * _movementSpeed * input_yaxis * Time.deltaTime);

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
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            // Check laser activation time
            if (Time.time > _laserActivationTime)
            {
                // Instantiate laser
                Instantiate(_laserPrefab, transform.position + _relativePositionOfLaser, Quaternion.identity);

                // Update laser activation time
                _laserActivationTime = Time.time + _laserCooldown;
            }
        }
    }
}
