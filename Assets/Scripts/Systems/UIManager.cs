using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] GameObject playerWinText;
    [SerializeField] TextMeshProUGUI movesList;
    [SerializeField] TextMeshProUGUI movesLeft;
    [SerializeField] TextMeshProUGUI movesCounter;
    [SerializeField] TextMeshProUGUI biscuitsTracker;

    void Awake()
    {
        if (instance == null) instance = this;
    }

    void Start()
    {
        DisplayBiscuitCount();
        EventsManager.instance.onPlayerWin.AddListener(PlayerWin);
        EventsManager.instance.onResetLevel.AddListener(ResetText);
    }

    void OnDestroy()
    {
        EventsManager.instance.onPlayerWin.RemoveListener(PlayerWin);
        EventsManager.instance.onResetLevel.AddListener(ResetText);
    }

    void PlayerWin()
    {
        playerWinText.SetActive(true);
        GameManager.instance.biscuitsCollected++;
    }

    public void AppendMove(string move)
    {
        movesList.text += "\n" + move;
    }

    public void MoveCounter(int numMoves)
    {
        movesCounter.text = numMoves.ToString();
    }

    public void DisplayMovesLeftText(int numMoves)
    {
        movesLeft.text = $"In {numMoves} move(s), your clone will follow your path to capture you";
    }

    public void HideMovesLeftText()
    {
        movesLeft.gameObject.SetActive(false);
    }

    public void ResetText()
    {
        movesLeft.text = $"";
        movesList.text = "";
        DisplayBiscuitCount();
    }

    public void DisplayBiscuitCount()
    {
        biscuitsTracker.text = $"{GameManager.instance.biscuitsCollected}/3";
    }

    public void IncreaseBiscuitCount()
    {
        biscuitsTracker.text = $"{GameManager.instance.biscuitsCollected + 1}/3";
    }
}
