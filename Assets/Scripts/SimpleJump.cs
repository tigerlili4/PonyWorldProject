using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleJump : MonoBehaviour
{
    [Header("Jumpattributes")]
    [SerializeField] 
    private float _jumpSpeed = 4f;

    // Update is called once per frame
    void Update()
    {
        //Moving and deleting the jump
        transform.Translate(Vector3.back * Time.deltaTime * _jumpSpeed);
        if (transform.position.z < -5f)
        {
            Destroy(this.gameObject);
        }
    }

    //Inform Pony about collisions and delete jump afterwars
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pony"))
        {
            other.GetComponent<Pony>().Damage();
            Destroy(this.gameObject);
        }
    }
}
