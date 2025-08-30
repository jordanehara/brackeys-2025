using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [SerializeField] protected Animator thisAnimator;
    protected Vector3 oldPos = Vector3.zero;
    protected Vector3 deltaPos = Vector3.zero;

    public virtual void SetWalking(bool val)
    {
        thisAnimator.SetBool("Walking", val);
    }

    public virtual void TriggerDeath()
    {
        thisAnimator.SetTrigger("Die");
    }

    public virtual void TriggerDash()
    {
        thisAnimator.SetTrigger("Horizontal");
    }

    public virtual void TriggerAscend()
    {
        thisAnimator.SetTrigger("StartAscend");
    }

    public virtual void TriggerDescend()
    {
        thisAnimator.SetTrigger("StartDescend");
    }
}
