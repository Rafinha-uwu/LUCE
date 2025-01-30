using UnityEngine;

public class CreditsMenu : MonoBehaviour
{
    public event System.Action OnClose;

    [SerializeField] private Canvas _canvas;
    [SerializeField] private UnityEngine.UI.Button _closeButton;
    private Animator _animator;


    private void Awake()
    {
        if (_canvas == null) throw new System.Exception("Canvas is not set in the inspector");
        if (_closeButton == null) throw new System.Exception("CloseButton is not set in the inspector");

        bool hasAnimator = TryGetComponent(out _animator);
        if (!hasAnimator) throw new System.Exception("Animator is not found on CreditsMenu");
    }

    private void Start() => Close();


    public void Open()
    {
        _canvas.enabled = true;
        _closeButton.Select();

        // Start animation
        _animator.SetBool("Thanks", false);
        _animator.Play("Credits");

        Invoke(nameof(CreditsStopped), 25.3f);
    }

    private void CreditsStopped() => Close();

    public void Close()
    {
        CancelInvoke();
        _canvas.enabled = false;
        OnClose?.Invoke();
    }
}
