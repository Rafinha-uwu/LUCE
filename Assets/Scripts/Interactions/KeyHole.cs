using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.Rendering.Universal;
using Cinemachine;

public class KeyHole : MonoBehaviour
{
    private static InputHandler _inputHandler;
    protected static readonly string PLAYER_TAG = "Player";
    protected bool _isPlayerNearby = false;

    public GameObject HelpDoor;
    public GameObject HelpBox;

    public GameObject Olho;
    public GameObject KeyCanvas;

    public GameObject Light;
    public bool fade;

    public bool once;

    private CinemachineImpulseSource impulseSource;

    private void Awake()
    {
        if (_inputHandler == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag(PLAYER_TAG);
            _inputHandler = player.GetComponent<InputHandler>();
        }

        _inputHandler.OnInteractAction += OnInteractAction;
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    private void OnDestroy()
    {
        _inputHandler.OnInteractAction -= OnInteractAction;
    }

    private void Update()
    {
        if (fade && Light.GetComponent<Light2D>().intensity > 0)
        {
            Light.GetComponent<Light2D>().intensity -= 0.05f;
        }
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

        HelpDoor.SetActive(false);
        KeyCanvas.GetComponent<Animator>().Play("Hole");
        Olho.GetComponent<Animator>().Play("LoopBlack");

        once = false;
        yield return new WaitForSecondsRealtime(9f);

        KeyCanvas.GetComponent<Animator>().Play("Empty");
        Olho.GetComponent<Animator>().Play("Nada");

        HelpBox.SetActive(true);
        fade = true;

        CameraShake.instance.CameraShaking(impulseSource);

        PauseManager.Instance.ResumeGame();
    }
}
