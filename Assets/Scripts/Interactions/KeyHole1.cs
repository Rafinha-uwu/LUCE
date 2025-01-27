using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.Rendering.Universal;

public class KeyHole1 : MonoBehaviour
{
    private static InputHandler _inputHandler;
    protected static readonly string PLAYER_TAG = "Player";
    protected bool _isPlayerNearby = false;


    public GameObject Block;
    public GameObject KeyCanvas;

    public GameObject Door;

    public bool once;


    // Start is called before the first frame update
    void Start()
    {
        if (_inputHandler == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag(PLAYER_TAG);
            _inputHandler = player.GetComponent<InputHandler>();
        }

        _inputHandler.OnInteractAction += OnInteractAction;


    }

    // Update is called once per frame
    void Update()
    {

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

        KeyCanvas.GetComponent<Animator>().Play("HoleBlack");

        Door.SetActive(true);

        once = false;
        yield return new WaitForSecondsRealtime(9f);

        KeyCanvas.GetComponent<Animator>().Play("Empty");
        Block.SetActive(false);

        PauseManager.Instance.ResumeGame();

    }

}
