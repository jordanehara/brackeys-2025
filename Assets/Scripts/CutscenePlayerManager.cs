using UnityEngine;

public class CutscenePlayerManager : MonoBehaviour
{
    [SerializeField] GameObject flash;

    Vector3 newPos;

    public void Start()
    {
        newPos = transform.position;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, newPos, 10f * Time.deltaTime);
    }

    public void Fall()
    {
        newPos = transform.position + new Vector3(0, -2);
    }
}
