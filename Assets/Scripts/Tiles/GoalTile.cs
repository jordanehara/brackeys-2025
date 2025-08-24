using UnityEngine;

public class GoalTile : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
            EventsManager.instance.onPlayerWin.Invoke();
    }
}
