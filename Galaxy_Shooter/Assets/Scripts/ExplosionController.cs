// Explosion controller.
// Created on Sat Jul  7 01:00:00 2018
// Author: Prasun Roy (https://github.com/prasunroy)
// GitHub: https://github.com/prasunroy/galaxy-shooter

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    [SerializeField]
    private bool _debug = false;
    [SerializeField]
    private float _destroyAfter = 0.0f;
    private AudioSource _audioSource;

    // Initialize
    void Start ()
    {
        // Debug message
        if (_debug)
        {
            Debug.Log("[INFO] ExplosionController initialized");
        }

        // Get audio source reference
        _audioSource = GetComponent<AudioSource>();

        // Play explosion sound effect
        if (_audioSource != null)
        {
            _audioSource.Play();
        }
        
        // Destroy after a delay
        Destroy(gameObject, _destroyAfter);
	}
}
