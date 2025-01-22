using FMOD.Studio;
using UnityEngine;

public class BackgroundPlayer : MonoBehaviour
{
    private static readonly string EVENT_PARAMETER_LOCATION = "Location";
    private EventInstance? _bgmInstance;
    private EventInstance? _ambienceInstance;


    private void Start()
    {
        _bgmInstance = FMODManager.Instance.CreateEventInstance(FMODManager.Instance.EventDatabase.GameBGM);
        _ambienceInstance = FMODManager.Instance.CreateEventInstance(FMODManager.Instance.EventDatabase.GameAmbience);

        _bgmInstance?.start();
        _ambienceInstance?.start();
    }

    public void SetMapLocation(MapLocation location)
    {
        _bgmInstance?.setParameterByName(EVENT_PARAMETER_LOCATION, (int)location);
        _ambienceInstance?.setParameterByName(EVENT_PARAMETER_LOCATION, (int)location);
    }


    public enum MapLocation
    {
        Basement,
        Bunker,
        BunkerDown1,
        BunkerDown2,
        BunkerRight,
        Tunnel,
        // ...
    }
}
