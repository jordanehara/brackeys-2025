using UnityEngine;

public class TeleportButtonTile : MonoBehaviour
{
    [SerializeField] GameObject door1;
    [SerializeField] GameObject door2;
    [SerializeField] SpriteRenderer button;
    private bool doorsUsed = false; // doors start open

    void Start()
    {
        EventsManager.instance.onPlayerTeleport.AddListener(CloseDoors);
    }

    void OnDestroy()
    {
        EventsManager.instance.onPlayerTeleport.RemoveListener(CloseDoors);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (doorsUsed)
        {
            door1.GetComponent<TeleportDoorTile>().OpenDoor();
            door2.GetComponent<TeleportDoorTile>().OpenDoor();
            AudioManager.instance.PlayTeleportButtonPushSound();
            GetComponentInChildren<Animator>().SetTrigger("Push");
            doorsUsed = false;
        }
    }

    protected void CloseDoors()
    {
        door1.GetComponent<TeleportDoorTile>().CloseDoor();
        door2.GetComponent<TeleportDoorTile>().CloseDoor();
        AudioManager.instance.PlayTeleportButtonReleaseSound();
        GetComponentInChildren<Animator>().SetTrigger("Unpush");
        doorsUsed = true;
    }
}