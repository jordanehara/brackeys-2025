using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoveTrackingManager : MonoBehaviour
{
    public static MoveTrackingManager instance;

    [SerializeField] GameObject directionIndicator;
    [SerializeField] TextMeshProUGUI movesList;
    [SerializeField] TextMeshProUGUI movesLeft;
    [SerializeField] TextMeshProUGUI movesCounter;
    [SerializeField] GameObject movesPanel;
    [SerializeField] Sprite baseImage;
    int currentMove = 0;
    Rect panel;

    void Awake()
    {
        if (instance == null) instance = this;
        panel = movesPanel.GetComponent<RectTransform>().rect;
    }

    public Quaternion GetRotation(string move)
    {
        float rotation = 0f;
        switch (move)
        {
            case "right":
                break;
            case "left":
                rotation = 180f;
                break;
            case "up":
                rotation = 90f;
                break;
            case "down":
                rotation = 270f;
                break;
            default:
                rotation = 0f;
                break;
        }
        return Quaternion.Euler(new Vector3(0, 0, rotation));
    }

    public void AddMove(string move)
    {
        // Rotate to the correct move inputs
        directionIndicator.transform.rotation = GetRotation(move);

        // Make child object of panel
        GameObject newDirection = Instantiate(directionIndicator, movesPanel.transform);

        // Move to a spaced position
        newDirection.transform.position = GetDirectionIconPosition();
        currentMove++;
    }

    Vector3 GetDirectionIconPosition()
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
        for (int i = 0; i < movesPanel.transform.childCount; i++)
        {
            Destroy(GetDirectionalTrackerObject(i));
        }
        currentMove = 0;
    }

    public GameObject GetDirectionalTrackerObject(int i)
    {
        return movesPanel.transform.GetChild(i).gameObject;
    }

    public void ResetImage(int i)
    {
        GetDirectionalTrackerObject(i).gameObject.transform.GetChild(0).GetComponent<Image>().sprite = baseImage;
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
