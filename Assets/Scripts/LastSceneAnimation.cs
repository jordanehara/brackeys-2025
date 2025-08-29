using UnityEngine;

public class LastSceneAnimation : MonoBehaviour
{
    [SerializeField] GameObject beam;
    [SerializeField] GameObject dog;

    bool beamStarted;

    void Start()
    {
        EventsManager.instance.onSpawnDog.AddListener(SpawnDog);
    }

    void Update()
    {
        if (!AnimatorIsPlaying() && beamStarted)
        {
            beam.SetActive(false);
            dog.SetActive(true);
        }
    }

    void OnDestroy()
    {
        EventsManager.instance.onSpawnDog.RemoveListener(SpawnDog);
    }

    void SpawnDog()
    {
        AudioManager.instance.PlayTeleportDogSound();
        beam.SetActive(true);
        beamStarted = true;
    }

    bool AnimatorIsPlaying()
    {
        Animator animator = beam.GetComponentInChildren<Animator>();
        if (!animator.isActiveAndEnabled) return false;
        return animator.GetCurrentAnimatorStateInfo(0).length >
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }
}
