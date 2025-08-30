using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public static SceneChanger instance;

    [SerializeField] string menuSceneName;
    [SerializeField] string creditsSceneName;
    [SerializeField] string gameSceneName;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene(menuSceneName);
    }

    public void LoadCreditsScene()
    {
        SceneManager.LoadScene(creditsSceneName);

    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("Level_0");
    }

    public void ReloadScene()
    {
        EventsManager.instance.onResetLevel.Invoke();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public int GetLevelNumber()
    {
        if (!SceneManager.GetActiveScene().name.Contains("Level")) return 0;
        return int.Parse(SceneManager.GetActiveScene().name.Split('_').Last());
    }

    public void GoToNextLevel()
    {
        int level = GetLevelNumber();
        if (GetLevelNumber() == 10)
            LoadCreditsScene();
        else
        {
            SceneManager.LoadScene("Level_" + (level + 1));
        }
    }
}
