using UnityEngine;

public class Biscuit : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            AudioManager.instance.PlayBiscuitCollectSound();
            UIManager.instance.IncreaseBiscuitCount();
            Destroy(gameObject);
        }
    }
}
