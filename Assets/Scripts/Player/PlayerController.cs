using System;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    bool alive = true;

    float speed = 5f;
    public Rigidbody rb;
    Boolean grounded;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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


}
