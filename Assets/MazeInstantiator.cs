using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeInstantiator : MonoBehaviour
{
    [SerializeField]
    private GameObject wallX, wallZ;
    [SerializeField]
    private float wallLength;

    // Start is called before the first frame update
    void Start()
    {
        MazeGenerator gen = GetComponent<MazeGenerator>();
        gen.GenerateMaze();
        gen.OutputMaze();
        InstantiateMaze(gen);
    }

    private void InstantiateMaze(MazeGenerator maze)
    {
        InstantiateXAxisWalls(maze.HorizontalWalls, maze.Rows, maze.Columns);
        InstantiateZAxisWalls(maze.VerticalWalls, maze.Rows, maze.Columns);
    }

    private void InstantiateXAxisWalls(bool[,] points, int rows, int columns)
    {
        Quaternion rotated = new Quaternion();
        rotated.eulerAngles = new Vector3(0, 90, 0);
        
        for(int i = 0; i < rows; i++)
        {
            for(int j = 0; j < columns; j++)
            {
                if (!points[i, j])
                    continue;
                Instantiate(wallX,
                    new Vector3(j * wallLength - wallLength / 2 + transform.position.x, 
                    1, 
                    wallLength * (rows - i) + transform.position.z),
                    rotated,
                    transform);
            }
        }
    }

    private void InstantiateZAxisWalls(bool[,] points, int rows, int columns)
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (!points[i, j])
                    continue;
                Instantiate(wallZ,
                    new Vector3(j * wallLength + transform.position.x , 
                    1, 
                    wallLength * (rows - i) + wallLength / 2 + transform.position.z),
                    transform.rotation,
                    transform);
            }
            
        }
    }
}
