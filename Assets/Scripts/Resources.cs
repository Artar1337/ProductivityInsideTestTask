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

    [SerializeField]
    private ScriptableWeapon[] weapons;
    [SerializeField]
    private ScriptableStats[] stats;
    [SerializeField]
    private GameObject hitParticles;

    private Dictionary<string, ScriptableWeapon> weaponDictionary;
    private Dictionary<string, ScriptableStats> statsDictionary;

    private AudioSource audioSource;
    

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
        audioSource = GetComponent<AudioSource>();
        weaponDictionary = new Dictionary<string, ScriptableWeapon>();
        statsDictionary = new Dictionary<string, ScriptableStats>();

        foreach(ScriptableWeapon obj in weapons)
        {
            weaponDictionary.Add(obj.Name, obj);
        }

        foreach (ScriptableStats obj in stats)
        {
            statsDictionary.Add(obj.Name, obj);
        }

        weapons = null;
        stats = null;
    }

    // instantiate on hit receiver
    public void SpawnParticles(Transform root)
    {
        Instantiate(hitParticles, root.transform.position + hitParticles.transform.position, root.transform.rotation);
    }

    public ScriptableWeapon GetWeaponByName(string name)
    {
        return weaponDictionary[name];
    }

    public ScriptableStats GetStatsByName(string name)
    {
        return statsDictionary[name];
    }

    public void PlayBackgroundMusic(AudioClip clip, bool loop = true)
    {
        if (audioSource.isPlaying)
            return;
        audioSource.loop = loop;
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }

}
