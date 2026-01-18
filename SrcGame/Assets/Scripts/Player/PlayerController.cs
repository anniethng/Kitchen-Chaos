using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public bool alive = true;
    public Rigidbody rb;
    public float speed = 5f; 
    public float jumpForce = 1.2f; 
    public float fallLimit = -5f;
    bool grounded;

    [Header("Energy System")]
    public float energy = 100f;
    public Slider energySlider; 
    public float energyConsumption = 30f; // Energie pro Sprung

    [Header("Wwise Bezeichnungen")]
    public string switchGroup = "CollisionType";
    public string impactEvent = "Play_Impact";

    [Tooltip("Name des Game Parameters in Wwise")]
    public string speedRTPC = "StressLevel";

    [Tooltip("Name der State Group in Wwise")]
    public string musicStateGroup = "GameState";
    [Tooltip("Name des Game Over States in Wwise")]
    public string gameOverState = "GameOver";

    GUIController guiController;

    // --- DELEGATES & EVENTS ABONNIEREN ---
    void OnEnable() { InputManager.OnJumpPressed += PerformJump; }
    void OnDisable() { InputManager.OnJumpPressed -= PerformJump; }

    void Start() {
        if (rb == null) rb = GetComponent<Rigidbody>();
        guiController = FindFirstObjectByType<GUIController>();
    }

    void FixedUpdate() {
        if (alive && (guiController != null && !guiController.escapeMenu.activeSelf)) {
            float moveH = Input.GetAxis("Horizontal");
            float moveV = Input.GetAxis("Vertical");
            Vector3 move = new Vector3(moveH, 0f, moveV);
            
            // Bewegung über Forces
            rb.AddForce(move * speed);

            // Sanfte Bremse (Reibungssimulation)
            if (move.magnitude == 0) {
                rb.linearVelocity *= 0.95f;
                rb.angularVelocity *= 0.95f;
            }
        }
    }

    void Update() {
        // UI Ladebalken aktualisieren
        if (energySlider) energySlider.value = energy;
        
        // Energie aufladen am Boden
        if (grounded && energy < 100) energy += 10 * Time.deltaTime;

        // Fall-Check (Absturz)
        if (alive && transform.position.y < fallLimit) Die();

        if(alive) {
            // Wwise RTPC für Geschwindigkeit setzen
            float currentSpeed = rb.linearVelocity.magnitude;
            AkUnitySoundEngine.SetRTPCValue(speedRTPC, currentSpeed, gameObject);
        }
    }

    void PerformJump() {
        // Sprung nur wenn am Boden und genug Energie
        if (alive && grounded && energy >= energyConsumption) {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            energy -= energyConsumption;
            grounded = false;
            Debug.Log("Delegate-Sprung ausgeführt!");
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            grounded = true;
            // Wwise Sound Trigger
            AkUnitySoundEngine.SetSwitch(switchGroup, "Hard", gameObject);
            AkUnitySoundEngine.PostEvent(impactEvent, gameObject);
        }
        if (collision.gameObject.CompareTag("Enemy")) Die();
    }

    public void Die() {
        if (!alive) return;
        alive = false;

    AkUnitySoundEngine.SetState(musicStateGroup, gameOverState);

        if (guiController) guiController.Death();

    }
}