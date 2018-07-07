// Player animation controller.
// Created on Sat Jul  7 18:00:00 2018
// Author: Prasun Roy (https://github.com/prasunroy)
// GitHub: https://github.com/prasunroy/galaxy-shooter

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField]
    private bool _debug = false;
    private Animator _animator;

    // Initialize
    void Start ()
    {
        // Debug message
        if (_debug)
        {
            Debug.Log("[INFO] PlayerAnimationController initialized");
        }

        // Get animator reference
        _animator = GetComponent<Animator>();
    }
    
    // Update
    void Update ()
    {
		// Player turns left
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) && _animator != null)
        {
            _animator.SetBool("Player_Turn_L", true);
            _animator.SetBool("Player_Turn_R", false);
        }
        else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow) && _animator != null)
        {
            _animator.SetBool("Player_Turn_L", false);
            _animator.SetBool("Player_Turn_R", false);
        }
        // Player turns right
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) && _animator != null)
        {
            _animator.SetBool("Player_Turn_L", false);
            _animator.SetBool("Player_Turn_R", true);
        }
        else if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow) && _animator != null)
        {
            _animator.SetBool("Player_Turn_L", false);
            _animator.SetBool("Player_Turn_R", false);
        }
    }
}
