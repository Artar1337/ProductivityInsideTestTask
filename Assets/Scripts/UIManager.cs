using UnityEngine;

// manages UI visuals (done by SetActive(false/true), idk how to do it another way)

public class UIManager : MonoBehaviour
{
    #region Singleton
    public static UIManager instance = null;

    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("UI manager instance error!");
            return;
        }
        instance = this;
    }
    #endregion

    GameObject pause, start, gameOver, win;

    private void Start()
    {
        pause = transform.Find("Pause").gameObject;
        start = transform.Find("Start").gameObject;
        gameOver = transform.Find("GameOver").gameObject;
        win = transform.Find("Win").gameObject;
    }

    public void SetPause(bool status)
    {
        if (pause != null)
            pause.SetActive(status);
    }

    public void SetWin(bool status)
    {
        win.SetActive(status);
    }

    public void SetStart(bool status)
    {
        start.SetActive(status);
    }

    public void SetGameOver(bool status)
    {
        gameOver.SetActive(status);
    }
}
