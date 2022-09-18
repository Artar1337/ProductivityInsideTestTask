using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

    public void GameOver()
    {
        Debug.Log("gameover");
    }

    // Start is called before the first frame update
    void Start()
    {
        // some inits
    }

    // Update is called once per frame
    void Update()
    {
        // buttons read
    }
}
