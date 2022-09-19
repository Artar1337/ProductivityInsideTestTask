using UnityEngine;

// just destroys the object aften N seconds

public class DestroyAfterSeconds : MonoBehaviour
{
    [SerializeField]
    float seconds;
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(DestroyObject), seconds);
    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }
}
