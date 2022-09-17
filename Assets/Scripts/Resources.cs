using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources : MonoBehaviour
{

    #region Singleton
    public static Resources instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Resources instance error!");
            return;
        }
        instance = this;
    }
    #endregion

    private System.Random rng;

    public float GetRandomFloat(float min, float max)
    {
        float range = max - min;
        float sample = (float)rng.NextDouble();
        return (sample * range) + min;
    }

    public bool GetRandomBool()
    {
        return rng.Next(0, 2) > 0;
    }

    public int GetRandomInt(int min, int max)
    {
        return rng.Next(min, max);
    }

    // Start is called before the first frame update
    void Start()
    {
        rng = new System.Random();
    }

}
