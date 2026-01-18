using UnityEngine;

public class ObjectSound : MonoBehaviour
{
    public string impactEvent = "Play_Impact";
    public string switchGroup = "Player_Collision (Switch)"; // Name deiner Switch Group in Wwise

    public float velocityThreshold = 2.0f;
    public float cooldownTime = 0.2f;
    private float lastSoundTime;

    private void OnCollisionEnter(Collision collision)
    {
        if (Time.time - lastSoundTime < cooldownTime) return;

        float impactForce = collision.relativeVelocity.magnitude;
        if (impactForce > velocityThreshold)
        {
            // --- NEU: Switch basierend auf dem getroffenen Objekt setzen ---
            if (collision.gameObject.CompareTag("Ground"))
            {
                AkUnitySoundEngine.SetSwitch(switchGroup, "Ground", gameObject);
            }
            else if (collision.gameObject.CompareTag("Obstacle"))
            {
                AkUnitySoundEngine.SetSwitch(switchGroup, "Obstacle", gameObject);
            }
            // ---------------------------------------------------------------

            AkUnitySoundEngine.PostEvent(impactEvent, gameObject);
            lastSoundTime = Time.time;
        }
    }
}