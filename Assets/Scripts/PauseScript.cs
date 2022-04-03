using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    private InputSystem input;
    [SerializeField] private GameObject panelMenu;
    [SerializeField] private Animator playerAnimator;

    private void Awake()
    {
        input = new InputSystem();
        input.Enable();
    }

    // Start is called before the first frame update
    void Start()
    {
        input.Player.Pause.performed += ctxPause => Pause();
    }

    private void Pause()
    {
        Time.timeScale = 0;
        panelMenu.active = true;
        playerAnimator.enabled = false;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        panelMenu.active = false;
        playerAnimator.enabled = true;
    }

    public void Exit()
    {
        Time.timeScale = 1;
        panelMenu.active = false;
        SceneManager.LoadScene("Menu");
    }
}
