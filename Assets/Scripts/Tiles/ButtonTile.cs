using System.Collections.Generic;
using UnityEngine;

public class ButtonTile : MonoBehaviour
{
    [SerializeField] private List<GameObject> doors = new List<GameObject>();

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (GameObject door in doors)
        {
            door.GetComponent<DoorTile>().open = !door.GetComponent<DoorTile>().open;
            EventsManager.instance.onDoorChanged.Invoke(door);
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        foreach (GameObject door in doors)
        {
            door.GetComponent<DoorTile>().open = !door.GetComponent<DoorTile>().open;
            EventsManager.instance.onDoorChanged.Invoke(door);
        }
    }
}
