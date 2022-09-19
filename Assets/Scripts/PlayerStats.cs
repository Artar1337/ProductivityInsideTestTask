using Observer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IObservable
{
    #region Singleton
    public static PlayerStats instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Player instance error!");
            return;
        }
        instance = this;
    }
    #endregion

    private new MeshRenderer renderer;
    private bool inRageMode = false;

    [SerializeField]
    private float health, damage, speed, rageTime, rageSpeed;
    [SerializeField]
    private Color normalColor, rageColor;

    private HashSet<EnemyObserver> observers = new HashSet<EnemyObserver>();

    public float Health { get => health; set => health = value; }
    public float Speed { get => inRageMode ? rageSpeed : speed; set => speed = value; }
    public float RageSpeed { get => rageSpeed; set => rageSpeed = value; }
    public float Damage { get => damage; set => damage = value; }
    public bool InRageMode { get => inRageMode; }

    private void Start()
    {
        renderer = GetComponent<MeshRenderer>();
    }

    public void RecieveHit(float damage)
    {
        health -= damage;
        Debug.Log("player's hp: " + health);
        Resources.instance.SpawnParticles(transform);
        if (health < 0f)
        {
            GetComponent<CharacterController>().enabled = false;
            GameManager.instance.GameOver();
        }
    }

    public void EnterRageMode()
    {
        CancelInvoke();
        inRageMode = true; 
        NotifyObservers();
        renderer.materials[0].color = rageColor;
        Invoke(nameof(StopRageMode), rageTime);
    }

    public void StopRageMode()
    {
        inRageMode = false;
        NotifyObservers();
        renderer.materials[0].color = normalColor;
        Resources.instance.StopMusic();
    }

    public void RegisterObserver(IObserver o)
    {
        observers.Add((EnemyObserver)o);
    }

    public void RemoveObserver(IObserver o)
    {
        observers.Remove((EnemyObserver)o);
    }

    public void NotifyObservers()
    {
        foreach (IObserver o in observers)
        {
            o.Update(this);
        }
    }
}
