using TMPro;
using UnityEngine;

public class DoorTile : MonoBehaviour
{
    [SerializeField] public bool open;

    void Update()
    {
        if (open)
        {
            GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.orange;
        }
    }
}
