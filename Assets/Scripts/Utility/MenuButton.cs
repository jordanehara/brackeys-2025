using UnityEngine;

public class MenuButton : MonoBehaviour
{
    public void LoadCreditsScene()
    {
        SceneChanger.instance.LoadCreditsScene();
    }

    public void LoadGameScene()
    {
        SceneChanger.instance.LoadGameScene();
    }
}