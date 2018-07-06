// UI controller.
// Created on Thu Jul  5 22:00:00 2018
// Author: Prasun Roy (https://github.com/prasunroy)
// GitHub: https://github.com/prasunroy/galaxy-shooter

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private bool _debug = false;
    [SerializeField]
    private Image _mainMenuImage;
    [SerializeField]
    private Sprite[] _playerLivesSprites;
    [SerializeField]
    private Image _playerLivesImage;
    [SerializeField]
    private Text _playerScoreText;
    [SerializeField]
    private int _playerScoreTotal;

    // Initialize
    void Start ()
    {
        // Debug message
        if (_debug)
        {
            Debug.Log("[INFO] UIController initialized");
        }
    }

    // Display main menu
    public void DisplayMainMenu(bool visible = true)
    {
        _mainMenuImage.enabled = visible;
    }

    // Update player lives
    public void UpdatePlayerLives(int lives)
    {
        _playerLivesImage.sprite = _playerLivesSprites[lives];
    }

    // Update player score
    public void UpdatePlayerScore(int score)
    {
        _playerScoreTotal += score;
        _playerScoreText.text = "SCORE = " + _playerScoreTotal;
    }

    // Reset player score
    public void ResetPlayerScore(int score = 0)
    {
        _playerScoreTotal = score;
        _playerScoreText.text = "SCORE = " + _playerScoreTotal;
    }
}
