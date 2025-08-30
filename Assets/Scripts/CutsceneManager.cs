using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject flash;
    [SerializeField] protected List<DialogData> levelEnterDialog = new List<DialogData>();

    void Start()
    {
        // Float in cryo for like 2s
        // Fall from the tube and barf
        StartCoroutine(LetIdle());

        // Dialog Start

        // DialogManager.instance.TriggerDialog(levelEnterDialog);

        // Dialog End

        // Spawn 2 clones

        // Exit left
    }

    void Update()
    {

    }

    IEnumerator LetIdle()
    {
        yield return new WaitForSecondsRealtime(3f);
        Animator().SetTrigger("Barf");
        Debug.Log("barf start");
        yield return new WaitForSeconds(Animator().GetCurrentAnimatorStateInfo(0).length + 3f);
        Debug.Log("barf finish");
        StartCoroutine(GetComponent<DialogManager>().TriggerDialog(levelEnterDialog));
    }

    // void Flash()
    // {

    //     yield return new WaitForSecondsRealtime(.5f);
    // }

    Animator Animator()
    {
        return player.GetComponent<Animator>();
    }
}
