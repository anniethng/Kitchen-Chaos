using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool alive = true;

    int speed = 5;
    public Rigidbody rb;
    Boolean grounded;

    public GameObject escapeMenu;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // escapeMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        {
            if (alive)
            {
                if(Input.GetKey(KeyCode.W))
                {
                    transform.Translate(Vector3.forward * Time.deltaTime * speed);
                }
                
                if(Input.GetKey(KeyCode.S))
                {
                    transform.Translate(Vector3.back * Time.deltaTime * speed);
                }
                
                if(Input.GetKey(KeyCode.A))
                {
                    transform.Translate(Vector3.left * Time.deltaTime * speed);
                }
                
                if(Input.GetKey(KeyCode.D))
                {
                    transform.Translate(Vector3.right * Time.deltaTime * speed);
                }

                if(Input.GetKeyDown(KeyCode.Space) && grounded)
                {
                    rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
                    grounded = false;
                }

                /* if(Input.GetKeyDown(KeyCode.Escape) && escapeMenu.gameObject.activeSelf)
                {
                    escapeMenu.gameObject.SetActive(false);
                } else if (Input.GetKeyDown(KeyCode.Escape) && !escapeMenu.gameObject.activeSelf)
                {
                    escapeMenu.gameObject.SetActive(true);
                } */
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
        
        if(collision.gameObject.CompareTag("Enemy"))
        {
            alive = false;
            GetComponent<MeshRenderer>().enabled = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pickup") && speed != 10)
        {
            speed += 5;
        }
    }
}
