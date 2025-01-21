using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class MapLocationSetter : MonoBehaviour
{
    private static readonly string PLAYER_TAG = "Player";

    private static BackgroundPlayer _backgroundPlayer;
    [SerializeField] private BackgroundPlayer.MapLocation _location;

    [SerializeField] private bool _oneTimeTrigger = false;
    private bool _triggered = false;


    private void Start()
    {
        if (_backgroundPlayer == null) _backgroundPlayer = FindObjectOfType<BackgroundPlayer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(PLAYER_TAG)) return;
        if (_oneTimeTrigger && _triggered) return;

        _triggered = true;
        _backgroundPlayer.SetMapLocation(_location);
    }
}

