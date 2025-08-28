using TMPro;
using UnityEngine;

public class MoveTrackingManager : MonoBehaviour
{
    public static MoveTrackingManager instance;

    [SerializeField] GameObject directionIndicator;
    [SerializeField] TextMeshProUGUI movesList;
    [SerializeField] TextMeshProUGUI movesLeft;
    [SerializeField] TextMeshProUGUI movesCounter;
    void Awake()
    {
        if (instance == null) instance = this;
    }

    public void AppendMove(string move)
    {
        movesList.text += "\n" + move;
        // add directional indicator
    }

    public void MoveCounter(int numMoves)
    {
        movesCounter.text = numMoves.ToString();
    }

    public void ResetMoves()
    {
        movesLeft.text = $"";
        movesList.text = "";
    }

    public void ShowMovesLeftText(int numMoves)
    {
        movesLeft.text = $"In {numMoves} move(s), your clone will follow your path to capture you";
    }

    public void HideMovesLeftText()
    {
        movesLeft.gameObject.SetActive(false);
    }
}
