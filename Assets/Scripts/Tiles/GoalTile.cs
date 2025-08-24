using UnityEngine;

public class GoalTile : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        EventsManager.instance.onPlayerWin.Invoke();
    }
}
