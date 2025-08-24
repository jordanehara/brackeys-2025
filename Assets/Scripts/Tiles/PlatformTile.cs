
using UnityEngine;

public class PlatformTile : MonoBehaviour
{
    [SerializeField] private float tileSize = 2f;

    public Vector3 MoveLeft()
    {
        return new Vector3(-tileSize, 0, 0);
    }

    public Vector3 MoveRight()
    {
        return new Vector3(tileSize, 0, 0);
    }
}