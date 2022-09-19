using System.Collections.Generic;
using UnityEngine;

// spawns N bonuses randomly on the map (never in the walls)

public class BonusSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject bonus;
    [SerializeField]
    private int count = 10, gap = 4;
    [SerializeField]
    private int boundX = 40, boundY = 22;

    // Start is called before the first frame update
    void Start()
    {
        SpawnBonuses();
    }

    HashSet<int> FillSetWithRandomValues(int count, int maxValue)
    {
        HashSet<int> coords = new HashSet<int>();
        int rng;
        for (int i = 0; i < count; i++)
        {
            rng = Resources.instance.GetRandomInt(0, maxValue);
            while (coords.Contains(rng))
            {
                rng = (rng + 1) % maxValue;
            }
            coords.Add(rng);
        }
        return coords;
    }

    void SpawnBonuses()
    {
        HashSet<int> xCoords = FillSetWithRandomValues(count, boundX);
        HashSet<int> yCoords = FillSetWithRandomValues(count, boundY);
        int[] usedCoordsX = new int[count];
        int i = 0;
        foreach (int el in xCoords)
        {
            usedCoordsX[i] = el;
            i++;
        }

        i = 0;
        foreach (int el in yCoords)
        {
            Instantiate(bonus,
                transform.position + new Vector3(usedCoordsX[i] * gap, 0.5f, el * gap),
                transform.rotation, transform);
            i++;
        }
    }
}
