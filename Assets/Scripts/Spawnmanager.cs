using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnmanager : MonoBehaviour
{
    [Header("Jumpspawning")] 
    [SerializeField]
    private List<GameObject> _jumpPrefab;
    [SerializeField] 
    private float _delay = 4f;
    private bool _spawningOn = true;

    [Header("Powerupspawning")] 
    [SerializeField]
    private GameObject _applePrefab;
    [SerializeField] 
    private GameObject _carrotPrefab;
    [SerializeField]
    private GameObject _donutPrefab;
    [SerializeField] 
    private float _powerUPSpawndelay = 20;
    
    private float _iterator = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Starting the spawning
        StartCoroutine(SpawnSystem());
        StartCoroutine(SpawnPowerUp());
    }

    //Stop spawning after Gameover
    public void onPlayerDeath()
    {
        _spawningOn = false;
    }

    //Spawn new jumps at certain time intervals and at set position
    IEnumerator SpawnSystem()
    {
        while (_spawningOn)
        {
            Instantiate(_jumpPrefab[Random.Range(0, _jumpPrefab.Count)], new Vector3(0f, -0.25f, 120f), Quaternion.identity, this.transform);
            
            //changing spawnrate after 5 jumps
            _iterator += 1f;
            if ( 0f == _iterator % 5f && _delay > 1.5f)
            {
                _delay -=  0.5f;
            }
            yield return new WaitForSeconds(_delay);
        }
    }

    //Spawning new Powerups at certain intervals, at different positions and different probabilities
    IEnumerator SpawnPowerUp()
    {
        while (_spawningOn)
        {
            float var = Random.Range(1f, 100f);
            if (var > 95f)
            {
                // 5% chance of donut
                Instantiate(_donutPrefab, new Vector3(Random.Range(-4f, 4f), 4.5f, 90f), Quaternion.identity,
                    this.transform);
                 
            }
            else if (var > 80f)
            {
                // 15% chance of instantiating an apple
                Instantiate(_applePrefab, new Vector3(Random.Range(-4f, 4f), 4.5f, 90f), Quaternion.identity,
                    this.transform);
            }
            else
            {
                // if not apple or donut, carrot instantiated 
                Instantiate(_carrotPrefab, new Vector3(Random.Range(-4f, 4f), 4.5f, 90f), Quaternion.identity,
                    this.transform);
            }
            // Time between spawning
            yield return new WaitForSeconds(_powerUPSpawndelay);
        }
    }
}