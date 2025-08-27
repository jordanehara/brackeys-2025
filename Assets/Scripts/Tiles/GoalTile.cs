using UnityEngine;

public class GoalTile : MonoBehaviour
{
    void OnDestroy()
    {
        EventsManager.instance.onDialogEnded.RemoveListener(OpenDoor);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            EventsManager.instance.onPlayerWin.Invoke();
            EventsManager.instance.onDialogEnded.AddListener(OpenDoor);
        }
    }

    void OpenDoor()
    {
        GetComponentInChildren<Animator>().enabled = true;
    }
}
