using Cinemachine;
using UnityEngine;

public class PuzzleEyeStarter : MonoBehaviour, ISavable
{
    private static readonly string SAVE_NAME = "PuzzleEyeStarter";
    private static readonly string PLAYER_TAG = "Player";

    private PuzzleEye puzzle;
    public GameObject pp;
    private bool _started = false;

    [SerializeField] private CinemachineVirtualCamera _puzzleCamera;


    private void Awake()
    {
        puzzle = pp.GetComponent<PuzzleEye>();
    }


    private void OnTriggerEnter2D(Collider2D collision) => OnTrigger(collision);
    private void OnTriggerStay2D(Collider2D collision) => OnTrigger(collision);

    private void OnTrigger(Collider2D collision)
    {
        if (_started) return;
        if (!collision.CompareTag(PLAYER_TAG)) return;

        StartPuzzle();
    }

    private void StartPuzzle()
    {
        _started = true;
        puzzle.StartPuzzle();

        if (_puzzleCamera != null && CamaraManager.instance._currentCamera != _puzzleCamera)
            CamaraManager.instance.SwapCameras(CamaraManager.instance._currentCamera, _puzzleCamera, Vector2.right);
    }


    public string GetSaveName() => SAVE_NAME;
    public object GetSaveData() => _started;
    public void LoadData(object data) => _started = (bool)data;
}
