using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] protected List<DialogData> levelEnterDialog = new List<DialogData>();
    [SerializeField] protected List<DialogData> afterCloneSpawn = new List<DialogData>();
    [SerializeField] GameObject beam;
    [SerializeField] List<GameObject> shadows;
    bool beamStarted;

    void Start()
    {
        EventsManager.instance.onDialogEnded.AddListener(SpawnBeamAndClones);
        StartCoroutine(LetIdle());
    }

    void OnDestroy()
    {
    }

    void Update()
    {
        if (!AnimatorIsPlaying() && beamStarted)
        {
            beam.SetActive(false);
            foreach (GameObject shadow in shadows)
            {
                shadow.SetActive(true);
            }
        }
    }

    IEnumerator LetIdle()
    {
        yield return new WaitForSecondsRealtime(3f);
        Animator().SetTrigger("Barf");
        yield return new WaitForSeconds(Animator().GetCurrentAnimatorStateInfo(0).length + 2f);
        Animator().SetTrigger("Recover");
        StartCoroutine(GetComponent<DialogManager>().TriggerDialog(levelEnterDialog));
    }

    void SpawnBeamAndClones()
    {
        AudioManager.instance.PlayTeleportDogSound();
        beam.SetActive(true);
        beamStarted = true;
        EventsManager.instance.onDialogEnded.RemoveListener(SpawnBeamAndClones);
        StartCoroutine(GetComponent<DialogManager>().TriggerDialog(afterCloneSpawn));
    }

    Animator Animator()
    {
        return player.GetComponent<Animator>();
    }

    bool AnimatorIsPlaying()
    {
        Animator animator = beam.GetComponentInChildren<Animator>();
        if (!animator.isActiveAndEnabled) return false;
        return animator.GetCurrentAnimatorStateInfo(0).length >
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }
}
