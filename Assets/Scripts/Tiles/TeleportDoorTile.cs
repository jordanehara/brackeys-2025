using UnityEngine;

public class TeleportDoorTile : MonoBehaviour
{
    protected bool open = true;
    [SerializeField] GameObject otherDoor;

    void Update()
    {
        if (open)
        {
            GetComponent<SpriteRenderer>().color = Color.magenta;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.brown;
        }
    }

    public void CloseDoor()
    {
        open = false;
    }

    public void OpenDoor()
    {
        open = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (open)
        {
            collision.gameObject.transform.position = otherDoor.transform.position - new Vector3(0, .15f, 0);
            collision.gameObject.GetComponent<PlayerController>().ResetMovePoint();
            EventsManager.instance.onPlayerTeleport.Invoke();
        }
    }

}
