using UnityEngine;

public class StartTile : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (animator != null && !AnimatorIsPlaying())
        {
            animator.gameObject.SetActive(false);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name != "Shadow") return;
        if (animator == null) return;
        animator.enabled = true;
        AudioManager.instance.PlayDoorCloseSound();
        // animator.SetTrigger("Close");
        // GetComponent<Collider2D>().enabled = false;
    }

    bool AnimatorIsPlaying()
    {
        return animator.GetCurrentAnimatorStateInfo(0).length >
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }
}
