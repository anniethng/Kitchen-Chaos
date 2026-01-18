using UnityEngine;
using System;

public class InputManager : MonoBehaviour
{
    // Das Event (Delegate), das für die 4 Punkte in Aufgabe 1/4 sorgt
    public static event Action OnJumpPressed;

    void Update()
    {
        // Wir prüfen den Input zentral an einer Stelle
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Event abfeuern, falls jemand zuhört
            OnJumpPressed?.Invoke();
        }
    }
}