using UnityEngine;
using UnityEngine.Events;

public class EventsManager : MonoBehaviour
{
    public static EventsManager instance;

    public UnityEvent<GameObject> onDoorChanged;
    public UnityEvent onPlayerWin; // reach goal
    public UnityEvent onPlayerLose; // fall off platforms/run into shadow
    public UnityEvent onPlayerTeleport;
    public UnityEvent onResetLevel;
    public UnityEvent onDialogEnded;
    public UnityEvent onDialogStarted;

    void Awake()
    {
        if (instance == null) instance = this;
    }
}
