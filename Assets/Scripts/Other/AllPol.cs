using FMOD.Studio;
using System.Collections;
using UnityEngine;

public class AllPol : MonoBehaviour
{
    private static InputHandler _inputHandler;
    protected static readonly string PLAYER_TAG = "Player";
    protected bool _isPlayerNearby = false;

    public GameObject E;

    public bool once;

    private CountColect _colect;
    public GameObject CC;

    public GameObject CutCol;

    private EventInstance? _cutsceneSound;


    private void Awake()
    {
        if (_inputHandler == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag(PLAYER_TAG);
            _inputHandler = player.GetComponent<InputHandler>();
        }

        _colect = CC.GetComponent<CountColect>();
        _inputHandler.OnInteractAction += OnInteractAction;
    }

    private void OnDestroy()
    {
        _inputHandler.OnInteractAction -= OnInteractAction;
    }


    private void Update()
    {
        if (once && _colect.nColect >= _colect.MaxColect) E.SetActive(true);
    }


    protected virtual void OnInteractAction()
    {
        if (!once || !_isPlayerNearby || _colect.nColect < _colect.MaxColect) return;

        if (E.GetComponent<Help>().isUsingController == true)
        {
            E.GetComponent<Animator>().Play("Idle");
        }
        else
        {
            E.GetComponent<Animator>().Play("Idle 1");
        }

        once = false;
        StartCoroutine(WeBack());
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


    private IEnumerator WeBack()
    {
        CutCol.GetComponent<Animator>().Play("Show");

        yield return new WaitForSecondsRealtime(1);
        E.SetActive(false);
        PauseManager.Instance.PauseGame();

        _cutsceneSound?.setPaused(false);
        _cutsceneSound?.start();

        yield return new WaitForSecondsRealtime(10f);
        PauseManager.Instance.ResumeGame();
    }


    private void Start()
    {
        _cutsceneSound = FMODManager.Instance.CreateEventInstance(FMODManager.Instance.EventDatabase.AllPolaroidsCutscene);
    }
}
