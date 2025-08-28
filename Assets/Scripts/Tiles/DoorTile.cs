using TMPro;
using UnityEngine;

public class DoorTile : MonoBehaviour
{
    [SerializeField] public bool open;
    Color originalColor;

    void Start()
    {
        originalColor = GetComponent<SpriteRenderer>().color;
    }

    void Update()
    {
        if (open)
        {
            GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        }
        else
        {
            GetComponent<SpriteRenderer>().color = originalColor;
        }
    }
}
