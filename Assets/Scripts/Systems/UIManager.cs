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

    void Awake()
    {
        if (instance == null) instance = this;
    }

    void Start()
    {
        EventsManager.instance.onPlayerWin.AddListener(DisplayWinText);
        EventsManager.instance.onPlayerLose.AddListener(DisplayLoseText);
        EventsManager.instance.onResetLevel.AddListener(ResetText);
    }

    void OnDestroy()
    {
        EventsManager.instance.onPlayerWin.RemoveListener(DisplayWinText);
        EventsManager.instance.onPlayerLose.RemoveListener(DisplayLoseText);
        EventsManager.instance.onResetLevel.AddListener(ResetText);
    }

    void DisplayWinText()
    {
        playerWinText.SetActive(true);
    }

    void DisplayLoseText()
    {
        Debug.Log("Player lose");
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
    }
}
