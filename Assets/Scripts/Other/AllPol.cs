using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.Rendering.Universal;

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


    // Start is called before the first frame update
    void Start()
    {
        if (_inputHandler == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag(PLAYER_TAG);
            _inputHandler = player.GetComponent<InputHandler>();
        }

        _inputHandler.OnInteractAction += OnInteractAction;

        _colect = CC.GetComponent<CountColect>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_colect.nColect > 5 && once)
        {
            E.SetActive(true);
        }
    }


    protected virtual void OnInteractAction()
    {
        if (_isPlayerNearby && _colect.nColect > 5 && once == true)
        {
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

        yield return new WaitForSecondsRealtime(6.5f);
        PauseManager.Instance.ResumeGame();
    }
}
