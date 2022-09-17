using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class AISurfaceBuilder : MonoBehaviour
{
    // call when rebuilt a level
    public void BakeAiSurface()
    {
        GetComponent<NavMeshSurface>().BuildNavMesh();
    }
}
