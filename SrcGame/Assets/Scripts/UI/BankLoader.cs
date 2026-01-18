using UnityEngine;

public class BankLoader : MonoBehaviour
{
    public string bankName = "Main"; // <--- HIER DEINEN BANK-NAMEN PRÜFEN

    void Awake()
    {
        Debug.Log("Versuche Bank zu laden: " + bankName);
        AkBankManager.LoadBank(bankName, false, false);
    }
}
