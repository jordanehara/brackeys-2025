using UnityEngine;

public class StartTile : MonoBehaviour
{
    // When the player leaves the tile, close the door
    // Delete it when the animation is done
    void Update()
    {
        // Check if player is in the tile
        // If not in the tile. Start the animation at a different speed.
        if (GetComponentInChildren<Animator>() != null && !AnimatorIsPlaying())
        {
            GetComponentInChildren<Animator>().gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Player on");
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (GetComponentInChildren<Animator>() != null)
        {
            GetComponentInChildren<Animator>().enabled = true;
            GetComponentInChildren<Animator>().SetTrigger("Close");
        }
    }

    bool AnimatorIsPlaying()
    {
        return GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).length >
               GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime;
    }
}
