using FMOD.Studio;
using System.Collections;
using UnityEngine;

public class KeyHole1 : MonoBehaviour
{
    private static InputHandler _inputHandler;
    protected static readonly string PLAYER_TAG = "Player";
    protected bool _isPlayerNearby = false;

    public GameObject Block;
    public GameObject KeyCanvas;

    public SpriteRenderer DoorClose;
    public SpriteRenderer DoorOpen;

    public GameObject HelpDoor;

    public bool once;

    private EventInstance? _keyHoleSound;


    private void Awake()
    {
        if (_inputHandler == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag(PLAYER_TAG);
            _inputHandler = player.GetComponent<InputHandler>();
        }

        DoorClose.enabled = true;
        DoorOpen.enabled = false;

        _inputHandler.OnInteractAction += OnInteractAction;
    }

    private void OnDestroy()
    {
        _inputHandler.OnInteractAction -= OnInteractAction;
    }


    protected virtual void OnInteractAction()
    {
        if (_isPlayerNearby && once == true)
        {
            StartCoroutine(Look());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isPlayerNearby) return;
        if (collision.CompareTag(PLAYER_TAG)) _isPlayerNearby = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!_isPlayerNearby) return;
        if (collision.CompareTag(PLAYER_TAG)) _isPlayerNearby = false;
    }


    private IEnumerator Look()
    {
        PauseManager.Instance.PauseGame();

        _keyHoleSound?.start();
        _keyHoleSound?.setPaused(false);

        KeyCanvas.GetComponent<Animator>().Play("HoleBlack");

        DoorClose.enabled = false;
        DoorOpen.enabled = true;
        HelpDoor.SetActive(false);

        once = false;
        yield return new WaitForSecondsRealtime(9f);

        KeyCanvas.GetComponent<Animator>().Play("Empty");
        Block.SetActive(false);

        PauseManager.Instance.ResumeGame();
    }


    private void Start()
    {
        _keyHoleSound = FMODManager.Instance.CreateEventInstance(FMODManager.Instance.EventDatabase.LastKeyHole);
    }
}
