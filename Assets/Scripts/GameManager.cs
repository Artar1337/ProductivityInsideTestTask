using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

// checking ESC input
// initiates gameover
// handles focus event
// controlls logic of UI buttons

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("GameManager instance error!");
            return;
        }
        instance = this;
    }
    #endregion

    [SerializeField]
    private GameObject[] objectsToDisableAndEnableAfterStart;

    private CharacterController player;
    private List<NavMeshAgent> enemies = new List<NavMeshAgent>();

    public bool Paused { get; private set; }

    public void GameOver(bool playerWon = false)
    {
        if (playerWon)
        {
            UIManager.instance.SetWin(true);
            return;
        }
        UIManager.instance.SetGameOver(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        // some inits
        SetSoundEnable(true);
        player = GameObject.Find("Player").GetComponent<CharacterController>();

        Transform enemyPool = GameObject.Find("EnemyPool").transform;
        for(int i = 0; i < enemyPool.childCount; i++)
        {
            enemies.Add(enemyPool.GetChild(i).GetComponentInChildren<NavMeshAgent>());
        }
        
        foreach (var obj in objectsToDisableAndEnableAfterStart)
            obj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // buttons read
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameOver();
        }
    }

    void OnApplicationFocus(bool hasFocus)
    {
        UIManager.instance.SetPause(!hasFocus);
        Paused = !hasFocus;
        if (player == null || enemies == null)
            return;
        foreach (var enemy in enemies)
        {
            if (enemy != null)
                enemy.enabled = hasFocus;
        }
            
        player.enabled = hasFocus;
    }

    public void SetSoundEnable(bool status)
    {
        AudioListener.volume = 0.0f;
        if(status)
            AudioListener.volume = 1.0f;
    }

    public void LoadGame()
    {
        UIManager.instance.SetStart(false);
        foreach (var obj in objectsToDisableAndEnableAfterStart)
            obj.SetActive(true);
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
