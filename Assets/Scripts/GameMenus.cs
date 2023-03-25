using UnityEngine;

public class GameMenus : MonoBehaviour
{
    [SerializeField] private CanvasGroup pauseMenu;
    [SerializeField] private CanvasGroup deathMenu;
    private bool _paused;
    private bool _playerDead;
    private void Start()
    {
        CameraFollow.playerDead += PlayerDead;
    }

    private void Pause()
    {
        _paused = true;
    }

    private void Resume()
    {
        _paused = false;
    }
    
    private void PlayerDead()
    {
        _playerDead = true;     
    }

    private void Update()
    {
        // Check if player is pressing escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // If the game is paused, resume
            if (_paused)
            {
                Resume();
            }
            // If the game is not paused, pause
            else
            {
                Pause();
            }
        }

        if (!_playerDead)
        {
            if (_paused)
            {
                Time.timeScale = Mathf.Lerp(Time.timeScale, 0, 0.05f);
                pauseMenu.alpha = Mathf.Lerp(pauseMenu.alpha, 1, 0.05f);
            }
            else if (!_playerDead)
            {
                Time.timeScale = Mathf.Lerp(Time.timeScale, 1, 0.05f);
                pauseMenu.alpha = Mathf.Lerp(pauseMenu.alpha, 0, 0.05f);
            }
        } else {
            Time.timeScale = Mathf.Lerp(Time.timeScale, 0, 0.005f);
            deathMenu.alpha = Mathf.Lerp(deathMenu.alpha, 1, 0.01f);
        }
    }
}
