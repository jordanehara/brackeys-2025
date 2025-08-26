using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public static DialogManager instance;
    [SerializeField] GameObject dialogBox;
    [SerializeField] TextMeshProUGUI dialogBodyText;
    [SerializeField] TextMeshProUGUI dialogNameText;

    List<DialogData> savedDialogData = new List<DialogData>();
    bool dialogRunning = false;
    bool dialogProgressedThisFrame = false;
    int dialogProgressionCount = 0;


    void Awake()
    {
        if (instance == null) instance = this;
    }

    void Start()
    {
        HideDialog();
    }

    void Update()
    {
        if (IsDialogRunning()) RunDialog();
    }

    void HideDialog()
    {
        dialogBox.SetActive(false);
    }

    #region Utility
    bool ProgressDialogButtonPressed()
    {
        return Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E);
    }

    bool IsDialogRunning()
    {
        return dialogRunning;
    }
    #endregion

    #region Dialog Core
    void NextDialog()
    {
        if (dialogProgressionCount >= savedDialogData.Count)
        {
            EndDialog();
        }
        else
        {
            dialogBodyText.text = savedDialogData[dialogProgressionCount].dialogText;

            dialogProgressionCount++;
        }
    }
    void EndDialog()
    {
        dialogRunning = false;
        dialogBox.SetActive(false);
        EventsManager.instance.onDialogEnded.Invoke();
    }

    public void TriggerDialog(string npcName, List<DialogData> dialogData)
    {
        if (dialogData == null)
        {
            Debug.LogError("Null dialog data");
            return;
        }
        dialogBox.SetActive(true);
        dialogNameText.text = npcName;
        dialogProgressionCount = 0;

        savedDialogData = dialogData;
        NextDialog();

        dialogRunning = true;
        EventsManager.instance.onDialogStarted.Invoke();
    }

    void RunDialog()
    {
        if (ProgressDialogButtonPressed() && !dialogProgressedThisFrame)
        {
            dialogProgressedThisFrame = true;
            NextDialog();
        }
        else
        {
            dialogProgressedThisFrame = false;
        }
    }
    #endregion
}

[Serializable]
public class DialogData
{
    public string name = "";
    public RawImage portrait;
    public string dialogText = "";
}