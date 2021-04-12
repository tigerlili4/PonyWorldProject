using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpCollectable : MonoBehaviour
{
    [SerializeField] 
    private float _speed = 10f;
    
    // Update is called once per frame
    void Update()
    {
        // movement of Powerups
        transform.Translate(Vector3.back * Time.deltaTime *_speed);
        //deleting object after moving out of frame
        if (transform.position.z < -3f)
        {
            Destroy(this.gameObject);
        }
    }

    // if PowerUp collides with pony the kind of object is transmitted to the pony
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pony"))
        {
            if (this.CompareTag("Donut"))
            {
                other.GetComponent<Pony>().ActivatePowerUp("Donut");
            }
            if (this.CompareTag("Apple"))
            {
                other.GetComponent<Pony>().ActivatePowerUp("Apple");
            }
            if (this.CompareTag("Carrot"))
            {
                other.GetComponent<Pony>().ActivatePowerUp("Carrot");
            }
        }
        // after the collision the PowerUp is deleted
        Destroy(this.gameObject);
    }
}
