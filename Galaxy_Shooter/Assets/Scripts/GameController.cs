// Game controller.
// Created on Fri Jul  6 22:00:00 2018
// Author: Prasun Roy (https://github.com/prasunroy)
// GitHub: https://github.com/prasunroy/galaxy-shooter

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private bool _debug = false;
    public bool gameOver = true;
    [SerializeField]
    private GameObject _playerPrefab;
    [SerializeField]
    private int _playerLivesOnStart = 1;
    [SerializeField]
    private int _playerScoreOnStart = 0;
    private UIController _uiController;

    // Initialize
    void Start ()
    {
        // Debug message
        if (_debug)
        {
            Debug.Log("[INFO] GameController initialized");
        }

        // Get UI controller reference
        _uiController = GameObject.Find("UI").GetComponent<UIController>();
    }
	
	// Update
	void Update ()
    {
		// Start game
        if (gameOver && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            // Hide main menu and reset player lives and player score
            if (_uiController != null)
            {
                _uiController.DisplayMainMenu(false);
                _uiController.UpdatePlayerLives(_playerLivesOnStart);
                _uiController.ResetPlayerScore(_playerScoreOnStart);
            }

            // Instantiate player
            Instantiate(_playerPrefab, Vector3.zero, Quaternion.identity);

            // Set game state flag
            gameOver = false;
        }
	}
}
