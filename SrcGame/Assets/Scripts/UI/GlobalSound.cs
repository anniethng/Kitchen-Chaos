using UnityEngine;

public class GlobalSound : MonoBehaviour
{
    public static GlobalSound Instance;

    private void Awake()
    {
        // Singleton-Check: Verhindert doppelte Musik beim Szenenwechsel
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlayUI_Click()
    {
        // Hier feuern wir das Event an Wwise
        AkUnitySoundEngine.PostEvent("Play_Click", gameObject);
    }
}