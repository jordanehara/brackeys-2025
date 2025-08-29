using UnityEngine;

public class TeleportDoorTile : MonoBehaviour
{
    protected bool open = true;
    [SerializeField] GameObject otherDoor;

    public void CloseDoor()
    {
        GetComponentInChildren<Animator>().SetTrigger("Close");
        open = false;
    }

    public void OpenDoor()
    {
        AudioManager.instance.PlayTeleportDoorOpenSound();
        GetComponentInChildren<Animator>().SetTrigger("Open");
        open = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (open)
        {
            AudioManager.instance.PlayTeleportSound();
            collision.gameObject.transform.position = otherDoor.transform.position;
            collision.gameObject.GetComponent<PlayerController>().ResetMovePoint();
            EventsManager.instance.onPlayerTeleport.Invoke();
        }
    }

}
