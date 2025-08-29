using UnityEngine;

public class MenuButton : MonoBehaviour
{
    public void LoadCreditsScene()
    {
        AudioManager.instance.PlayClickSound();
        SceneChanger.instance.LoadCreditsScene();
    }

    public void LoadGameScene()
    {
        AudioManager.instance.PlayClickSound();
        SceneChanger.instance.LoadGameScene();
    }
}