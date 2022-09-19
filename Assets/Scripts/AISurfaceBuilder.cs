using Unity.AI.Navigation;
using UnityEngine;

// just rebuilts a navmesh for a random labyrinth

public class AISurfaceBuilder : MonoBehaviour
{
    // call when rebuilt a level
    public void BakeAiSurface()
    {
        GetComponent<NavMeshSurface>().BuildNavMesh();
    }
}
