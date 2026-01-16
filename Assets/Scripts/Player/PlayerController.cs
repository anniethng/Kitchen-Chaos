using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool alive = true;

    // Movement (Achtung: Force braucht höhere Werte!)
    public float speed = 10f; 
    public Rigidbody rb;
    bool grounded;

    // GUI Elements
    public GameObject escapeMenu;
    public GameObject gameOverMenu;
    public GameObject HUD;

    // External classes
    GUIController guiController;

    void Start()
    {
        // Sicherheitshalber den Rigidbody holen, falls vergessen
        if (rb == null) rb = GetComponent<Rigidbody>();

        // GUI Controller suchen (mit Fallback, damit kein Fehler kommt)
        guiController = FindFirstObjectByType<GUIController>();
        
        if(escapeMenu) escapeMenu.SetActive(true);
        if(gameOverMenu) gameOverMenu.SetActive(false);
        if(HUD) HUD.SetActive(false);
    }
    
    // Physik gehört IMMER in FixedUpdate
    void FixedUpdate()
    {
        if (alive)
        {
            // Nur bewegen, wenn das Menü aus ist
            if (escapeMenu != null && !escapeMenu.activeSelf)
            {
                // Wir nutzen Input.GetAxis (das deckt W,A,S,D und Pfeiltasten ab)
                float moveHorizontal = Input.GetAxis("Horizontal");
                float moveVertical = Input.GetAxis("Vertical");

                Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

                // Hier ist der Zaubertrick: KRAFT statt Beamen -> Kugel rollt!
                rb.AddForce(movement * speed);
            }
        }
    }

    // Springen (Input) gehört in Update
    void Update()
    {
        if (alive && escapeMenu != null && !escapeMenu.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Space) && grounded)
            {
                rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
                grounded = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // WICHTIG: Wir haben vorhin den Tag "Floor" für Sounds vergeben.
        // Deshalb checken wir hier auf "Floor" ODER "Ground", damit beides geht.
        if(collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Floor"))
        {
            grounded = true;
        }
        
        if(collision.gameObject.CompareTag("Enemy"))
        {
            alive = false;
            GetComponent<MeshRenderer>().enabled = false;
            if(guiController) guiController.Death();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Pickup Logic
        if(other.gameObject.CompareTag("Pickup"))
        {
            // Geschwindigkeit erhöhen (angepasst für Force)
            speed += 5;
        }
    }
}