// Laser controller.
// Created on Mon Jun 25 22:00:00 2018
// Author: Prasun Roy (https://github.com/prasunroy)
// GitHub: https://github.com/prasunroy/galaxy-shooter

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1.0f;
    [SerializeField]
    private float _visibleBoundary = 10.0f;
    [SerializeField]
    private bool _debug = false;

    // Initialize
    void Start ()
    {
        // Debug message
        if (_debug)
        {
            Debug.Log("[INFO] LaserController initialized");
        }
    }

    // Update
    void Update ()
    {
        // Move laser
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        // Destroy laser if out of visible boundary
        if (transform.position.y > _visibleBoundary)
        {
            Destroy(gameObject);
        }
    }
}
