using UnityEngine;

public class StartTile : MonoBehaviour
{
    void Update()
    {
        if (GetComponentInChildren<Animator>() != null && !AnimatorIsPlaying())
        {
            GetComponentInChildren<Animator>().gameObject.SetActive(false);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name != "Shadow") return;
        if (GetComponentInChildren<Animator>() == null) return;
        GetComponentInChildren<Animator>().enabled = true;
        GetComponentInChildren<Animator>().SetTrigger("Close");
    }

    bool AnimatorIsPlaying()
    {
        return GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).length >
               GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime;
    }
}
