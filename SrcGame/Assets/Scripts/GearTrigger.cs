using UnityEngine;

public class GearTrigger : MonoBehaviour
{
    public Animator gearAnimator;
    public string animationBool = "isSpinning";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gearAnimator.SetBool(animationBool, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gearAnimator.SetBool(animationBool, false);
        }
    }
}
