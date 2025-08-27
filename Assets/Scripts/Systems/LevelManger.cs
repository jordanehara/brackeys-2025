using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManger : MonoBehaviour
{
    public static LevelManger instance;

    [SerializeField] int numShadowMoves = 1;
    [SerializeField] List<GameObject> shadows = new List<GameObject>();
    [SerializeField] protected List<DialogData> levelEnterDialog = new List<DialogData>();
    [SerializeField] protected List<DialogData> shadowEnterDialog = new List<DialogData>();
    [SerializeField] protected List<DialogData> levelEndDialog = new List<DialogData>();

    private List<string> shadowMoves = new List<string>();

    void Awake()
    {
        if (instance == null) instance = this;
    }

    void Start()
    {
        StartLevlEnterDialog();
        EventsManager.instance.onShadowSpawn.AddListener(StartShadowEnterDialog);
        EventsManager.instance.onPlayerWin.AddListener(StartLogDialog);
    }

    void OnDestroy()
    {
        EventsManager.instance.onShadowSpawn.RemoveListener(StartShadowEnterDialog);
        EventsManager.instance.onPlayerWin.RemoveListener(StartLogDialog);
    }

    void Update()
    {
        UIManager.instance.MoveCounter(numShadowMoves - shadowMoves.Count);
        if (shadowMoves.Count == numShadowMoves)
        {
            UIManager.instance.HideMovesLeftText();
            foreach (GameObject shadow in shadows)
            {
                shadow.SetActive(true);
            }
        }
        else
        {
            UIManager.instance.DisplayMovesLeftText(numShadowMoves - shadowMoves.Count);
        }
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
            UIManager.instance.AppendMove(move);
        }
    }
    #endregion

    #region Dialog triggers
    public void StartLevlEnterDialog()
    {
        if (levelEndDialog.Count > 0)
        {
            StartCoroutine(DialogManager.instance.TriggerDialog(levelEnterDialog));
        }
    }

    public void StartShadowEnterDialog()
    {
        if (shadowEnterDialog.Count > 0)
        {
            StartCoroutine(DialogManager.instance.TriggerDialog(shadowEnterDialog));
        }
    }

    public void StartLogDialog()
    {
        if (levelEndDialog.Count > 0)
        {
            StartCoroutine(DialogManager.instance.TriggerDialog(levelEndDialog));
        }
    }
    #endregion
}
