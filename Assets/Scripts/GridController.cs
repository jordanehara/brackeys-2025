using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    [SerializeField] public bool hideGrid = true;
    [SerializeField] private List<GameObject> levelPieces = new List<GameObject>();

    public Grid grid;
    private int x = 14;
    private int y = 8;

    private void Awake()
    {
        grid = new Grid(x, y, 2f, new Vector3(-x, -y), hideGrid);
        foreach (GameObject tile in levelPieces)
        {
            if (tile != null)
                grid.SetValue(tile.transform.position, GetTileNumber(tile));
        }
        EventsManager.instance.onDoorChanged.AddListener(ChangeDoorState);
    }

    void OnDestroy()
    {
        EventsManager.instance.onDoorChanged.RemoveListener(ChangeDoorState);
    }

    void ChangeDoorState(GameObject door)
    {
        grid.SetValue(door.transform.position, door.GetComponent<DoorTile>().open ? 0 : 5);
    }

    private int GetTileNumber(GameObject tile)
    {
        switch (tile.name)
        {
            case "Platform":
                return 1;
            case "Ladder":
                return 2;
            case "Top Ladder":
                return 3;
            case "Button":
                return 4;
            case "Door":
                if (tile.GetComponent<DoorTile>().open)
                    return 0;
                return 5;
            case "Goal":
                return 6;
            case "Start":
                return 7;
            case "Teleport Button":
                return 8;
            case "Teleport Door":
                return 9;
            default:
                return -1;
        }
    }
}
