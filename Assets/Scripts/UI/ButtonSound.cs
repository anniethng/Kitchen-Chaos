using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public void PlayClick()
    {
        AkUnitySoundEngine.PostEvent("Play_Click", null);
        
    }
}
