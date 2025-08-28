using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] GameObject levelUIParent;
    [SerializeField] GameObject continueButton;
    [SerializeField] GameObject resetButton;
    [SerializeField] GameObject directionPanel;
    [SerializeField] TextMeshProUGUI biscuitsTracker;

    public int currentBiscuits = 0;

    void Awake()
    {
        if (instance == null) instance = this;
    }

    void Start()
    {
        ShowBiscuitCount();
        EventsManager.instance.onPlayerWin.AddListener(PlayerWin);
        EventsManager.instance.onResetLevel.AddListener(ResetText);
    }

    void OnDestroy()
    {
        EventsManager.instance.onPlayerWin.RemoveListener(PlayerWin);
        EventsManager.instance.onResetLevel.AddListener(ResetText);
    }

    #region Show/Hide UI elements
    public void SetLevelUI()
    {
        levelUIParent.SetActive(true);
        HideContinueButton();
    }

    public void HideLevelUI()
    {
        levelUIParent.SetActive(false);
    }

    public void ShowMoveTracker()
    {
        directionPanel.SetActive(true);
    }

    public void HideMoveTracker()
    {
        directionPanel.SetActive(false);
    }

    public void ShowContinueButton()
    {
        continueButton.SetActive(true);
    }

    public void HideContinueButton()
    {
        continueButton.SetActive(false);
    }

    public void ShowResetButton()
    {
        resetButton.SetActive(true);
    }

    public void HideResetButton()
    {
        resetButton.SetActive(false);
    }

    #endregion

    void PlayerWin()
    {
        GameManager.instance.biscuitsCollected += currentBiscuits;
    }

    public void ResetText()
    {
        MoveTracker().ResetMoves();
        ShowBiscuitCount();
    }

    public MoveTrackingManager MoveTracker()
    {
        return GetComponentInChildren<MoveTrackingManager>();
    }

    public void ShowBiscuitCount()
    {
        biscuitsTracker.text = $"{GameManager.instance.biscuitsCollected}/3";
    }

    public void IncreaseBiscuitCount()
    {
        currentBiscuits++;
        biscuitsTracker.text = $"{currentBiscuits}/3";
    }
}
