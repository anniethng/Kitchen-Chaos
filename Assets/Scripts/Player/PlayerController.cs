using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool alive = true;

    public int speed = 5;
    public Rigidbody rb;
    Boolean grounded;

    public GameObject escapeMenu;
    public GameObject gameOverMenu;
    public GameObject HUD;

    PlayerController playerController;
    PlayerScore playerScore;
    SpawnPickup spawnPickup;
    GUIController guiController;

    void Start()
    {
        playerController = FindFirstObjectByType<PlayerController>();
        playerScore = FindFirstObjectByType<PlayerScore>();
        spawnPickup = FindFirstObjectByType<SpawnPickup>();
        guiController = FindFirstObjectByType<GUIController>();
        
        escapeMenu.SetActive(true);
        gameOverMenu.SetActive(false);
        HUD.SetActive(false);
    }
    
    // Update is called once per frame
    void Update()
    {
        {
            if (alive)
            {
                if (!escapeMenu.gameObject.activeSelf)
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

                if(Input.GetKeyDown(KeyCode.Escape) && escapeMenu.gameObject.activeSelf)
                {
                    escapeMenu.gameObject.SetActive(false);
                    HUD.SetActive(true);
                    guiController.UpdateScores();
                } else if (Input.GetKeyDown(KeyCode.Escape) && !escapeMenu.gameObject.activeSelf)
                {
                    escapeMenu.gameObject.SetActive(true);
                    HUD.SetActive(false);
                    guiController.UpdateScores();
                }
            }
        }
    }

    public void Reset()
    {
        playerScore.score = 0;
        playerScore.scoreText.text = "Score: " + "0";
        playerController.transform.position = new Vector3(0, 1, 33);
        playerController.speed = 5;

        spawnPickup.SpawnNewPickups();
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
            escapeMenu.SetActive(false);
            gameOverMenu.SetActive(true);
            HUD.SetActive(false);
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
