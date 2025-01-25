using UnityEngine;

public class HintsMenu : MonoBehaviour
{
    public event System.Action OnClose;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private PauseMenu _pauseMenu;
    [SerializeField] private GameObject See;

    public bool On = true;

    public GameObject Bunker1;
    public GameObject Bunker2;
    public GameObject Bunker3;
    public GameObject Eyes;
    public GameObject Def;
    public GameObject Blur;

    private GameObject current;


    CamaraManager camaram;
    public GameObject cm;

    private void Awake()
    {
        if (_canvas == null) throw new System.Exception("Canvas is not set in the inspector");
        if (_pauseMenu == null) throw new System.Exception("PauseMenu is not set in the inspector");

        camaram = cm.GetComponent<CamaraManager>();
    }

    private void Update()
    {
        if (camaram._currentCamera.name != null && On == true)
        {
            switch (camaram._currentCamera.name)
            {
                case "Cam Bunker":

                    See.SetActive(true);
                    Blur.SetActive(true);
                    Def.SetActive(false);
                    current = Bunker1;

                    break;

                case "Cam Bunker D1":

                    See.SetActive(true);
                    Blur.SetActive(true);
                    Def.SetActive(false);
                    current = Bunker2;

                    break;

                case "Cam Bunker D2":

                    See.SetActive(true);
                    Blur.SetActive(true);
                    Def.SetActive(false);
                    current = Bunker3;

                    break;

                case "Cam TP1":

                    See.SetActive(true);
                    Blur.SetActive(true);
                    Def.SetActive(false);
                    current = Eyes;

                    break;

                default:

                    See.SetActive(false);
                    Blur.SetActive(false);
                    Def.SetActive(true);

                    break;
            }
        }
    }

    public void Open()
    {
        _canvas.enabled = true;
    }

    public void Reveal()
    {
        See.SetActive(false);
        On = false;

        current.SetActive(true);
        Blur.SetActive(false);

    }


    public void Close()
    {

        See.SetActive(true);
        Blur.SetActive(true);
        if (current != null)
        {
            current.SetActive(false);
        }
        On = true;

        _canvas.enabled = false;
        _pauseMenu._canvas.enabled = true;
        OnClose?.Invoke();
    }
}