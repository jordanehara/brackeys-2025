using UnityEngine;

public class DoorTile : MonoBehaviour
{
    [SerializeField] public bool open;

    void Update()
    {
        if (open)
        {
            GetComponent<SpriteRenderer>().color = Color.purple;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.orange;
        }
    }
}
