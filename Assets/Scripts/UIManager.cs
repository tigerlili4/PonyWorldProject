using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

// Managing all displays
public class UIManager : MonoBehaviour
{
    private int _carrots = 0;
    private int _lives = 4;
    
    [Header("Texts")]
    [SerializeField] 
    private Text _carrotsText;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _livesText;
    [SerializeField]
    private Text _beginnerText;
    
    // Start is called before the first frame update
    void Start()
    {
        //creating start position for displayed texts
        _gameOverText.gameObject.SetActive(false);
        _carrotsText.text = "Carrots: " + _carrots;
        _livesText.text = "Lives: " + _lives;
        _beginnerText.gameObject.SetActive(true);
    }

    // if pony falls from the track or looses all lives, GameOver!
    // Beginnertext removed from display if still active at GameOver
     public void ShowGameOver()
    {
        _beginnerText.gameObject.SetActive(false);
        _gameOverText.gameObject.SetActive(true);
    }

     //inactivating explaining text
     public void ShowBeginnerText()
     {
         _beginnerText.gameObject.SetActive(false);
     }
     
     // Counting the collected carrots and displaying them
     public void AddCarrots(int carrot)
     {
         _carrots += carrot;
         _carrotsText.text = "Carrots: " + _carrots;
     }
     //indicating a change in remaining lives 
    public void ChangeLives(int lives)
    {
        _lives += lives;
        _livesText.text = "Lives: " + _lives;
    }
}
