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
    [SerializeField] TextMeshProUGUI biscuitsTracker;

    void Awake()
    {
        if (instance == null) instance = this;
    }

    void Start()
    {
        DisplayBiscuitCount();
        EventsManager.instance.onPlayerWin.AddListener(PlayerWin);
        EventsManager.instance.onPlayerLose.AddListener(PlayerLose);
        EventsManager.instance.onResetLevel.AddListener(ResetText);
    }

    void OnDestroy()
    {
        EventsManager.instance.onPlayerWin.RemoveListener(PlayerWin);
        EventsManager.instance.onPlayerLose.RemoveListener(PlayerLose);
        EventsManager.instance.onResetLevel.AddListener(ResetText);
    }

    void PlayerWin()
    {
        playerWinText.SetActive(true);
        GameManager.instance.biscuitsCollected++;
    }

    void PlayerLose()
    {
        Debug.Log("Player lose");
        SceneChanger.instance.ReloadScene();
    }

    public void AppendMove(string move)
    {
        movesList.text += "\n" + move;
    }

    public void DisplayMovesLeft(int numMoves)
    {
        movesLeft.text += numMoves.ToString();
    }

    public void ResetText()
    {
        movesLeft.text = "Moves Left: ";
        movesList.text = "";
        DisplayBiscuitCount();
    }

    public void DisplayBiscuitCount()
    {
        biscuitsTracker.text = $"Biscuits Found: {GameManager.instance.biscuitsCollected}/3";
    }

    public void IncreaseBiscuitCount()
    {
        biscuitsTracker.text = $"Biscuits Found: {GameManager.instance.biscuitsCollected + 1}/3";
    }
}
