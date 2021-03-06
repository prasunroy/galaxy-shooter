﻿// Power up controller.
// Created on Fri Jun 29 22:00:00 2018
// Author: Prasun Roy (https://github.com/prasunroy)
// GitHub: https://github.com/prasunroy/galaxy-shooter

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    [SerializeField]
    private bool _debug = false;
    [SerializeField]
    private int _powerUpID = 0; // 0 = None, 1 = TripleShot, 2 = Speed, 3 = Shield
    [SerializeField]
    private float _speed = 1.0f;
    [SerializeField]
    private float _visibleBoundary = -10.0f;
    [SerializeField]
    private AudioClip _audioClip;

    // Initialize
    void Start ()
    {
        // Debug message
        if (_debug)
        {
            Debug.Log("[INFO] PowerUpController initialized");
        }
    }
    
    // Update
	void Update ()
    {
        // Move power up
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        // Destroy power up if out of visible boundary
        if (transform.position.y < _visibleBoundary)
        {
            Destroy(gameObject);
        }
    }

    // Triggered collision
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check whether collision with player
        if (other.tag == "Player")
        {
            // Get player reference
            PlayerController playerController = other.GetComponent<PlayerController>();

            // Control power up
            if (playerController != null)
            {
                playerController.PowerUpController(_powerUpID);
            }

            // Play audio clip
            if (_audioClip != null)
            {
                AudioSource.PlayClipAtPoint(_audioClip, Camera.main.transform.position);
            }
            
            // Destroy power up
            Destroy(gameObject);
        }
    }
}
