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
        UIManager.instance.DisplayMovesLeft(numShadowMoves);
        if (levelEndDialog.Count > 0)
        {
            DialogManager.instance.TriggerDialog(levelEnterDialog);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (shadowMoves.Count == numShadowMoves)
        {
            foreach (GameObject shadow in shadows)
            {
                shadow.SetActive(true);
            }
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

    #endregion
}
