using UnityEngine;

public class GlobalSound : MonoBehaviour
{
   public static GlobalSound instance;

    private void Awake()
    {
         if (instance == null && instance != this)
         {
             Destroy(gameObject);
             return;
    }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlayUI_Click()
    {
        AkUnitySoundEngine.PostEvent("Play_Click", gameObject);
    }
}
