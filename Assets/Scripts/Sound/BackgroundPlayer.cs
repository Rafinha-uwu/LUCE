using FMOD.Studio;
using UnityEngine;

public class BackgroundPlayer : MonoBehaviour
{
    private EventInstance? _bgmInstance;
    private EventInstance? _ambienceInstance;


    private void Start()
    {
        _bgmInstance = FMODManager.Instance.CreateEventInstance(FMODManager.Instance.EventDatabase.GameBGM);
        _ambienceInstance = FMODManager.Instance.CreateEventInstance(FMODManager.Instance.EventDatabase.GameAmbience);

        _bgmInstance?.start();
        _ambienceInstance?.start();
    }
}
