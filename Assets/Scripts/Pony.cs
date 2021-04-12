using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Pony : MonoBehaviour
{
    [Header("Ground")]
    [SerializeField] 
    private LayerMask _ground;
    private Vector3 JumpForce = new Vector3(0, 8.5f, 0);
    private Rigidbody rb;
    private bool _onGround = false;
    private const int _speed = 5;
    [Header("Playerattributes")]
    [SerializeField]
    private int _lives = 4;
    [Header("UIManager")] 
    [SerializeField]
    private UIManager _uiManager;
    [Header("Spawnmanager")] 
    [SerializeField]
    private Spawnmanager _spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        //instantiating Rigidbody for jump
        rb = GetComponent<Rigidbody>();
        transform.position = new Vector3(0, 1, 0);
    }
    
    
   // Update is called once per frame
    void Update()
    {
        // Go left if `a` is pressed
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * (_speed * Time.deltaTime));
        }
        // Go right if `d` is pressed
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * (_speed * Time.deltaTime));
        }
        
        // Jump if space is pressed and we're on the ground
        if (_onGround && Input.GetKeyDown(KeyCode.Space))
        {
            // We jump by adding force upwards
            rb.AddForce(JumpForce, ForceMode.Impulse);
            _onGround = false;
        }

        //deleting the text after a while
        if (Time.time >= 10f && Time.time <= 11f)
        {
            _uiManager.ShowBeginnerText();
        }
        
        // if Pony leaves track, instant Gameover
        if (transform.position.y < -5f)
        {
            Damage();
        }
    }
    
    // on ground only true after pony touched the ground
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Floor"))
        {
            _onGround = true;
        }
    }
    
    // loss of lives for touching jumps
    public void Damage()
    {
        _lives -= 1;
        _uiManager.ChangeLives(-1);

        // if all lives lost or pony leaves Track, Gameover is initiated
        if (_lives == 0 || Mathf.Abs(transform.position.x) > 6f)
        {
            if (_spawnManager != null)
            {
                _spawnManager.onPlayerDeath();
            }
            else
            {
                Debug.LogError("Spawnmanager not assigned!");
            }
            //Deleting all remaining objects after Gameover
            _uiManager.ShowGameOver();
            Destroy(this.gameObject);
            Destroy(_spawnManager.gameObject);
            
        }
    }

    // Depending on the kind of PowerUp, different advantages unlock themselves
    public void ActivatePowerUp(string kind)
    {
        if (kind == "Carrot")
        {
            _uiManager.AddCarrots(1);
        }
        if (kind == "Apple")
        {
            _uiManager.AddCarrots(2);
        }
        if (kind == "Donut")
        {
            if (_lives < 4f)
            {
                _lives += 1;
                _uiManager.ChangeLives(1);
            }
        }
    }
}
