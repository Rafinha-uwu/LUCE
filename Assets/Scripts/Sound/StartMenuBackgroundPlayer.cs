using FMOD.Studio;
using UnityEngine;

public class StartMenuBackgroundPlayer : MonoBehaviour
{
    private EventInstance? _bgmInstance;

    private void Start()
    {
        _bgmInstance = FMODManager.Instance.CreateEventInstance(FMODManager.Instance.EventDatabase.StartMenuBGM);
        _bgmInstance?.start();
    }

    public void StopBGM()
    {
        _bgmInstance?.stop(STOP_MODE.ALLOWFADEOUT);
    }
}
