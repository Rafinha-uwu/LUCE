using UnityEngine;
using UnityEngine.UI;

public class HintsMenu : MonoBehaviour
{
    public event System.Action OnClose;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private GameObject See;
    [SerializeField] private UnityEngine.UI.Button _firstButton;
    private UnityEngine.UI.Button _seeButton;

    public bool On = true;

    public GameObject Bunker1;
    public GameObject Bunker2;
    public GameObject Bunker3;
    public GameObject Eyes;
    public GameObject Def;
    public GameObject Blur;

    private GameObject current;


    private void Awake()
    {
        if (_canvas == null) throw new System.Exception("Canvas is not set in the inspector");
        if (_firstButton == null) throw new System.Exception("First button is not set in the inspector");

        _seeButton = See.GetComponent<UnityEngine.UI.Button>();
    }

    public void Open()
    {
        _canvas.enabled = true;

        switch (SaveManager.Instance.LastCheckpointName)
        {
            case "BunkerStartCheckpoint":

                See.SetActive(true);
                Blur.SetActive(true);
                Def.SetActive(false);
                current = Bunker1;

                break;

            case "BunkerD1Checkpoint":

                See.SetActive(true);
                Blur.SetActive(true);
                Def.SetActive(false);
                current = Bunker2;

                break;

            case "BunkerD2Checkpoint":

                See.SetActive(true);
                Blur.SetActive(true);
                Def.SetActive(false);
                current = Bunker3;

                break;

            case "PuzzleEyeCheckpoint":

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


        // Set navigation
        _firstButton.navigation = new Navigation
        {
            mode = Navigation.Mode.Explicit,
            selectOnUp = current != null ? _seeButton : null
        };

        // Select
        if (current != null) _seeButton.Select();
        else _firstButton.Select();
    }

    public void Reveal()
    {
        _firstButton.Select();
        See.SetActive(false);
        current.SetActive(true);
        Blur.SetActive(false);
    }


    public void Close()
    {
        See.SetActive(true);
        Blur.SetActive(true);
        if (current != null) current.SetActive(false);

        _canvas.enabled = false;
        OnClose?.Invoke();
    }
}