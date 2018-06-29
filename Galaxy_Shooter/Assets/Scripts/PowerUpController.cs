// Power up controller.
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
    private float _speed = 1.0f;

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
    }

    // Triggered collision
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check whether collision with player
        if (other.tag == "Player")
        {
            // Get player reference
            PlayerController playerController = other.GetComponent<PlayerController>();
            
            // Check player reference
            if (playerController != null)
            {
                // Control power up
                playerController.PowerUpController();
            }
            
            // Destroy power up
            Destroy(gameObject);
        }
    }
}
