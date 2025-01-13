using FMOD.Studio;
using UnityEngine;

public class BackgroundPlayer : MonoBehaviour
{
    private static readonly string PLAYER_TAG = "Player";
    private static readonly string EVENT_PARAM_SCARED = "Scared";

    private Scared _scared;

    private EventInstance _bgmInstance;
    private EventInstance _ambienceInstance;


    private void Awake()
    {
        _scared = GameObject.FindGameObjectWithTag(PLAYER_TAG).GetComponent<Scared>();
    }


    private void Start()
    {
        _bgmInstance = FMODManager.Instance.CreateEventInstance(FMODManager.Instance.EventDatabase.GameBGM);
        _ambienceInstance = FMODManager.Instance.CreateEventInstance(FMODManager.Instance.EventDatabase.GameAmbience);

        OnScared(false);
        _scared.OnScared += OnScared;

        _bgmInstance.start();
        _ambienceInstance.start();
    }

    private void OnDestroy()
    {
        _scared.OnScared -= OnScared;
    }


    private void OnScared(bool isScared)
    {
        float scaredValue = isScared ? 1 : 0;

        _bgmInstance.setParameterByName(EVENT_PARAM_SCARED, scaredValue);
        _ambienceInstance.setParameterByName(EVENT_PARAM_SCARED, scaredValue);
    }
}
