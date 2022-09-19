using UnityEngine;

// activates bonus on trigger enter, depends on PlayerStats

public class Bonus : MonoBehaviour
{
    [SerializeField]
    private AudioClip rageMusic;

    private void ActivateBonus()
    {
        PlayerStats.instance.EnterRageMode();
        Resources.instance.PlayBackgroundMusic(rageMusic, true);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            ActivateBonus();
        }
    }
}
