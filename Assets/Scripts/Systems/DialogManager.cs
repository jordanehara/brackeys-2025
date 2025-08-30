using System;
using System.Collections;
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
    [SerializeField] Image dialogPortrait;
    [SerializeField] AudioClip voidSound;
    [SerializeField] float typingSpeed = 0.01f;

    List<DialogData> savedDialogData = new List<DialogData>();
    public bool dialogRunning = false;
    bool dialogProgressedThisFrame = false;
    int dialogProgressionCount = 0;
    bool isTyping;
    Coroutine typeLine;

    void Awake()
    {
        if (instance == null) instance = this;
        Debug.Log("bro");
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
        return Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space);
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
            dialogPortrait.sprite = savedDialogData[dialogProgressionCount].portrait;
            dialogNameText.text = savedDialogData[dialogProgressionCount].name;
            typeLine = StartCoroutine(TypeLine());
        }
    }

    void EndDialog()
    {
        dialogRunning = false;
        dialogBox.SetActive(false);
        if (LevelManager.instance != null && !LevelManager.instance.GetLevelComplete())
        {
            UIManager.instance.ShowMoveTracker();
            UIManager.instance.ShowResetButton();
        }
        else
        {
            UIManager.instance.ShowContinueButton();
        }
        EventsManager.instance.onDialogEnded.Invoke();
    }

    public IEnumerator TriggerDialog(List<DialogData> dialogData)
    {
        UIManager.instance.HideMoveTracker();
        UIManager.instance.HideResetButton();
        EventsManager.instance.onDialogStarted.Invoke();
        yield return new WaitForSecondsRealtime(1f);
        dialogBox.SetActive(true);
        dialogProgressionCount = 0;

        savedDialogData = dialogData;
        NextDialog();

        dialogRunning = true;
    }

    void RunDialog()
    {
        if (ProgressDialogButtonPressed() && !dialogProgressedThisFrame)
        {
            if (isTyping) // if player is skipping the character by character printing, set the text
            {
                StopCoroutine(typeLine);
                dialogBodyText.text = savedDialogData[dialogProgressionCount].dialogText;
                isTyping = false;
            }
            else
            {
                dialogProgressedThisFrame = true;
                dialogProgressionCount++;
                NextDialog();
            }
        }
        else
        {
            dialogProgressedThisFrame = false;
        }
    }

    float GetVoicePitch()
    {
        switch (savedDialogData[dialogProgressionCount].name)
        {
            case "Skye":
                return 1;
            default:
                return 1;
        }
    }

    IEnumerator TypeLine()
    {
        dialogBodyText.text = "";
        isTyping = true;
        foreach (char letter in savedDialogData[dialogProgressionCount].dialogText)
        {
            dialogBodyText.text += letter;
            AudioManager.instance.PlaySpeakingSound(0.4f, GetVoicePitch());
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
    }
    #endregion
}

[Serializable]
public class DialogData
{
    public string name = "";
    public Sprite portrait;
    public string dialogText = "";
}