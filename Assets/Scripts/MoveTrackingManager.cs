using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveTrackingManager : MonoBehaviour
{
    public static MoveTrackingManager instance;

    [SerializeField] GameObject directionIndicator;
    [SerializeField] TextMeshProUGUI movesList;
    [SerializeField] TextMeshProUGUI movesLeft;
    [SerializeField] TextMeshProUGUI movesCounter;
    [SerializeField] GameObject movesPanel;
    int currentMove = 0;
    Rect panel;

    void Awake()
    {
        if (instance == null) instance = this;
        panel = movesPanel.GetComponent<RectTransform>().rect;
    }

    public void AppendMove(string move)
    {
        switch (move)
        {
            case "right":
                AddMove(0f);
                break;
            case "left":
                AddMove(180f);
                break;
            case "up":
                AddMove(90f);
                break;
            case "down":
                AddMove(270f);
                break;
        }
    }

    public void AddMove(float rotation)
    {
        // Rotate to the correct move inputs
        directionIndicator.transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotation));

        // Make child object of panel
        GameObject newDirection = Instantiate(directionIndicator, movesPanel.transform);

        // Move to a spaced position
        newDirection.transform.position = GetDirectionIconPosition();
        currentMove++;
    }

    public Vector3 GetDirectionIconPosition()
    {
        return movesPanel.transform.position + new Vector3(currentMove * panel.width / LevelManager.instance.numShadowMoves + panel.width / LevelManager.instance.numShadowMoves / 2, 0, 0);
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
