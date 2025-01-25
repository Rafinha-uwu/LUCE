using UnityEngine;
using UnityEngine.Rendering.Universal;
using Cinemachine;
using System;
using FMOD.Studio;

public class Scared : MonoBehaviour
{
    public event Action<bool> OnScared;
    private bool _isScared = false;

    LightDetection lightdetection;
    public GameObject lt;

    CamaraManager camaram;
    public GameObject cm;


    private float Ortho;
    private bool OrthReady;


    private float GlobalL;
    private bool GLReady;

    public GameObject GL;

    public float Speed;
    public float Speed_scared;

    // public Vector2 LastCheck;
    private PlayerController _playerController;
    private PlayerHoldItem _playerHoldItem;
    private static readonly string ANIMATOR_PARAMETER = "IsScared";

    public float ScaredTime = 10f;
    public float ScaredCountDown = 10f;
    public bool timerIsRunning = false;

    /*
    public CinemachineVirtualCamera BunkCam;
    public CinemachineVirtualCamera BunkEl;
    public CinemachineVirtualCamera BunkTun;

    private GameObject CheckTrue;
    public GameObject Check1;
    public GameObject Check2;
    */

    private bool _checkScared = true;
    public void SetCheckScared(bool value) => _checkScared = value;
    // public bool check = true;
    // public bool check2 = true;

    public GameObject Scared1;
    public GameObject Scared2;
    public GameObject Scared3;

    private Animator _scared1Animator;
    private Animator _scared2Animator;
    private Animator _scared3Animator;

    public GameObject Darkness;

    private EventInstance? _scaredSound;


    private void Start()
    {
        lightdetection = lt.GetComponent<LightDetection>();
        camaram = cm.GetComponent<CamaraManager>();

        timerIsRunning = true;
        ScaredCountDown = ScaredTime;

        _playerController = GetComponent<PlayerController>();
        Speed = _playerController.MovingState._speed;

        _playerHoldItem = GetComponent<PlayerHoldItem>();

        _scared1Animator = Scared1 != null ? Scared1.GetComponent<Animator>() : null;
        _scared2Animator = Scared2 != null ? Scared2.GetComponent<Animator>() : null;
        _scared3Animator = Scared3 != null ? Scared3.GetComponent<Animator>() : null;

        _scaredSound = FMODManager.Instance.CreateEventInstance(FMODManager.Instance.EventDatabase.PlayerScared);
        OnScared += PlayScaredSound;

        SetPosition();
    }

    private void OnDestroy()
    {
        OnScared -= PlayScaredSound;
        _playerController.MovingState._speed = Speed;
    }

    private void FixedUpdate()
    {
        if (!_checkScared) return;
        if (timerIsRunning)
        {
            // check = false;
            if (ScaredCountDown > 0)
            {
                ScaredCountDown -= Time.deltaTime;
            }
            else
            {
                ScaredCountDown = 0;
                timerIsRunning = false;
            }
        }

        if (Time.timeScale == 0) return;

        switch (camaram._currentCamera.name)
        {


            case "Cam Basement":
                Ortho = 6;
                GlobalL = 0.05f;
                break;


            case "Cam Elevator":
                Ortho = 5;
                GlobalL = 0.05f;
                break;

            case "Cam Bunker":
                Ortho = 7;
                GlobalL = 0.05f;
                break;

            case "Cam Tunel":
                Ortho = 3;
                GlobalL = 0.03f;
                break;

            case "Cam Bunker D1":
                Ortho = 6;
                GlobalL = 0.05f;
                break;

            case "Cam Bunker D2":
                Ortho = 6;
                GlobalL = 0.002f;
                break;



            case "Cam Bunker R":
                Ortho = 7;
                GlobalL = 0.05f;
                break;

            case "Cam Bunker R2":
                Ortho = 4;
                GlobalL = 0.05f;
                break;

            case "Cam Tunel 2":
                Ortho = 6;
                GlobalL = 0.04f;
                break;

            case "Cam TP1":
                Ortho = 10;
                GlobalL = 0.05f;
                break;

            case "Cam Exit":
                Ortho = 4;
                GlobalL = 0.03f;
                break;

            default:

                break;
        }


        if (OrthReady)
        {
            camaram._currentCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = Ortho;
        }

        if (GLReady)
        {
            GL.GetComponent<Light2D>().intensity = GlobalL;
        }

        bool isScared = lightdetection.LightValue < 0.25f;
        if (_isScared != isScared)
        {
            _isScared = isScared;
            OnScared?.Invoke(_isScared);
            _playerController.Animator.SetBool(ANIMATOR_PARAMETER, isScared);
            _playerController.MovingState._speed = isScared ? Speed_scared : Speed;
        }

        if (isScared)
        {
            // if (check2 == true)
            // {
            if (_scared1Animator != null) _scared1Animator.SetBool("Scared", true);
            if (_scared2Animator != null) _scared2Animator.SetBool("Scared", true);
            if (_scared3Animator != null) _scared3Animator.SetBool("Scared", true);
            // check = false;
            // }

            if (ScaredCountDown > 0)
            {

                if (OrthReady)
                {
                    OrthReady = false;
                }

                if (this.GetComponentInChildren<Light2D>().intensity > 0.01f)
                {
                    this.GetComponentInChildren<Light2D>().intensity -= 0.00035f;
                }
                

                if (camaram._currentCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize > 3)
                {
                    camaram._currentCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize -= 0.0015f;


                }


                if (GLReady)
                {
                    GLReady = false;
                }

                if (GL.GetComponent<Light2D>().intensity > 0.001f)
                {
                    GL.GetComponent<Light2D>().intensity -= 0.00003f;
                }




            }

            else
            {
                Invoke("Death", 0.5f);
                Darkness.GetComponent<Animator>().SetBool("Dark", true);
            }
        }
        else
        {
            if (this.GetComponentInChildren<Light2D>().intensity < 0.17)
            {
                this.GetComponentInChildren<Light2D>().intensity += 0.006f;

            }
            if (camaram._currentCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize < Ortho)
            {
                camaram._currentCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize += 0.012f;
            }
            if (camaram._currentCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize >= Ortho)
            {
                OrthReady = true;

            }

            if (GL.GetComponent<Light2D>().intensity < GlobalL)
            {
                GL.GetComponent<Light2D>().intensity += 0.002f;
            }

            if (GL.GetComponent<Light2D>().intensity >= GlobalL)
            {

                GLReady = true;
            }

            if (_scared1Animator != null) _scared1Animator.SetBool("Scared", false);
            if (_scared2Animator != null) _scared2Animator.SetBool("Scared", false);
            if (_scared3Animator != null) _scared3Animator.SetBool("Scared", false);
            // check2 = true;

            ScaredCountDown = ScaredTime;
        }
    }

    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Checkpoint")
        {
            LastCheck = collision.gameObject.transform.position;
            CheckTrue = collision.gameObject;
        }
    }
    */

    public void Death()
    {
        PlayDeathSound();
        _playerHoldItem.ForceDrop();

        /*
        if (camaram._currentCamera.name == "Cam Bunker")
        {
            if(CheckTrue == Check1)
            {
                camaram._currentCamera = BunkEl;
                BunkCam.enabled = false;
                BunkEl.enabled = true;
            }
            if (CheckTrue == Check2)
            {
                camaram._currentCamera = BunkEl;
                BunkCam.enabled = false;
                BunkEl.enabled = true;
                LastCheck = Check1.transform.position;
            }

        }
        */
        SetPosition();
        LoadCheckpoint();

        if (_scared1Animator != null) _scared1Animator.SetBool("Scared", false);
        if (_scared2Animator != null) _scared2Animator.SetBool("Scared", false);
        if (_scared3Animator != null) _scared3Animator.SetBool("Scared", false);
        Darkness.GetComponent<Animator>().SetBool("Dark", false);
        // check2 = true;
        // check = true;
        // this.transform.position = LastCheck;
        ScaredCountDown = ScaredTime;
        this.GetComponentInChildren<Light2D>().intensity = 0.17f;
        timerIsRunning = true;
    }


    private void SetPosition()
    {
        Checkpoint checkpoint = SaveManager.Instance.LastCheckpoint;
        if (checkpoint == null) return;

        Vector3 checkPosition = checkpoint.transform.position;
        _playerController.transform.position = new(checkPosition.x, checkPosition.y, _playerController.transform.position.z);
        _playerController.Rb.velocity = Vector2.zero;

        if (checkpoint.CheckpointCamera != null)
        {
            camaram._currentCamera.enabled = false;
            camaram._currentCamera = checkpoint.CheckpointCamera;
            camaram._currentCamera.enabled = true;
        }
    }

    private void LoadCheckpoint()
    {
        Checkpoint checkpoint = SaveManager.Instance.LastCheckpoint;
        if (checkpoint != null) checkpoint.Load();
    }


    private void PlayDeathSound()
    {
        FMODManager.Instance.PlayOneShotAttached(
            FMODManager.Instance.EventDatabase.PlayerDeath,
            _playerController.gameObject
        );
    }

    private void PlayScaredSound(bool isScared)
    {
        if (!_scaredSound.HasValue) return;

        if (isScared)
        {
            _scaredSound?.start();
            FMODManager.Instance.AttachInstance(_scaredSound.Value, _playerController.transform, _playerController.Rb);
        }
        else _scaredSound?.stop(STOP_MODE.ALLOWFADEOUT);
    }
}
