

using UnityEngine;

public class TeleportButtonTile : MonoBehaviour
{
    [SerializeField] private GameObject door1;
    [SerializeField] private GameObject door2;
    private bool pushed = true; // doors start open

    void Start()
    {
        EventsManager.instance.onPlayerTeleport.AddListener(CloseDoors);
    }

    void OnDestroy()
    {
        EventsManager.instance.onPlayerTeleport.RemoveListener(CloseDoors);
    }

    void Update()
    {
        if (pushed)
        {
            GetComponent<SpriteRenderer>().color = Color.lightBlue;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.chocolate;
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (!pushed)
        {
            door1.GetComponent<TeleportDoorTile>().OpenDoor();
            door2.GetComponent<TeleportDoorTile>().OpenDoor();
            pushed = true;
        }
    }

    protected void CloseDoors()
    {
        door1.GetComponent<TeleportDoorTile>().CloseDoor();
        door2.GetComponent<TeleportDoorTile>().CloseDoor();
        pushed = false;
    }
}