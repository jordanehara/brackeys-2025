using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [SerializeField] public int numShadowMoves = 1;
    [SerializeField] string answer;
    [SerializeField] List<GameObject> shadows = new List<GameObject>();
    [SerializeField] protected List<DialogData> levelEnterDialog = new List<DialogData>();
    [SerializeField] protected List<DialogData> shadowEnterDialog = new List<DialogData>();
    [SerializeField] protected List<DialogData> levelEndDialog = new List<DialogData>();

    private List<string> shadowMoves = new List<string>();
    bool levelCompleted = false;

    void Awake()
    {
        if (instance == null) instance = this;
    }

    void Start()
    {
        UIManager.instance.currentBiscuits = 0;
        // Debug.Log(answer);
        UIManager.instance.SetLevelUI();
        StartLevelEnterDialog();
        EventsManager.instance.onShadowSpawn.AddListener(StartShadowEnterDialog);
        EventsManager.instance.onPlayerWin.AddListener(StartLogDialog);
        if (SceneChanger.instance.GetLevelNumber() == 10)
        {
            UIManager.instance.HideMoveTracker();
        }
    }

    void OnDestroy()
    {
        EventsManager.instance.onShadowSpawn.RemoveListener(StartShadowEnterDialog);
        EventsManager.instance.onPlayerWin.RemoveListener(StartLogDialog);
    }

    void Update()
    {
        MoveTrackingManager.instance.MoveCounter(numShadowMoves - shadowMoves.Count);
        if (shadowMoves.Count == numShadowMoves)
        {
            MoveTrackingManager.instance.HideMovesLeftText();
            foreach (GameObject shadow in shadows)
            {
                shadow.SetActive(true);
            }
        }
        else
        {
            MoveTrackingManager.instance.ShowMovesLeftText(numShadowMoves - shadowMoves.Count);
        }
    }

    public void SetLevelComplete()
    {
        levelCompleted = true;
    }

    public bool GetLevelComplete()
    {
        return levelCompleted;
    }

    #region Shadow movement
    public List<string> GetShadowMoves()
    {
        return shadowMoves;
    }

    public void AddMove(string move)
    {
        if (shadowMoves.Count < numShadowMoves)
        {
            shadowMoves.Add(move);
            UIManager.instance.MoveTracker().AddMove(move);
        }
    }

    #endregion

    #region Dialog triggers

    public void StartDialog(List<DialogData> dialog)
    {
        if (dialog.Count > 0)
        {
            StartCoroutine(DialogManager.instance.TriggerDialog(dialog));
        }
        else
        {
            StartCoroutine(wait());
        }
    }

    public IEnumerator wait()
    {
        yield return new WaitForSecondsRealtime(2f);
    }

    public void StartLevelEnterDialog()
    {
        StartDialog(levelEnterDialog);
    }

    public void StartShadowEnterDialog()
    {
        StartDialog(shadowEnterDialog);
    }

    public void StartLogDialog()
    {
        UIManager.instance.ResetText();
        StartDialog(levelEndDialog);
    }
    #endregion
}
