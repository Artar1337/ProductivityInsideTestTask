using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// generates maze according to eller's algorithm

public class MazeGenerator : MonoBehaviour
{
    [SerializeField]
    private int rows, columns;
    private int uniqueCounter = 0;
    private bool[,] verticalWalls, horizontalWalls;
    private int[] mazeLine;

    public int Rows { get => rows; }
    public int Columns { get => columns; }
    public bool[,] VerticalWalls { get => verticalWalls; }
    public bool[,] HorizontalWalls { get => horizontalWalls; }

    public void GenerateMaze()
    {
        // init variables
        uniqueCounter = 0;
        verticalWalls = new bool[rows, columns];
        horizontalWalls = new bool[rows, columns];
        mazeLine = new int[columns];

        for (int i = 0; i < rows - 1; i++)
        {
            AssignUniqueSet();
            AddVerticalWalls(i);
            AddHorizontalWalls(i);
            CheckHorizontalWalls(i);
            PrepareNewLine(i);
        }
        AddEndLine();
    }

    private void AssignUniqueSet()
    {
        for (int i = 0; i < columns; i++)
        {
            if (mazeLine[i] == 0)
            {
                // assign unique set
                mazeLine[i] = uniqueCounter;
                uniqueCounter++;
            }
        }
    }

    private void AddVerticalWalls(int row)
    {
        for (int i = 0; i < columns - 1; i++)
        {
            // should add a wall
            bool choise = Resources.instance.GetRandomBool();
            // to avoid cycles
            if (choise == true || mazeLine[i] == mazeLine[i + 1])
            {
                verticalWalls[row, i] = true;
            }
            else
            {
                MergeSet(i, mazeLine[i]);
            }
        }
        // add right wall at last column
        verticalWalls[row, columns - 1] = true;
    }

    private void MergeSet(int index, int element)
    {
        int currentSet = mazeLine[index + 1];
        for (int j = 0; j < columns; j++)
        {
            if (mazeLine[j] == currentSet)
            {
                mazeLine[j] = element;
            }
        }
    }

    private void AddHorizontalWalls(int row)
    {
        for (int i = 0; i < columns; i++)
        {
            // if true - place the wall
            bool choise = Resources.instance.GetRandomBool();
            if (CalculateUniqueSet(mazeLine[i]) != 1 && choise == true)
            {
                horizontalWalls[row, i] = true;
            }
        }
    }

    private int CalculateUniqueSet(int element)
    {
        int countUniqSet = 0;
        for (int i = 0; i < columns; i++)
        {
            if (mazeLine[i] == element)
            {
                countUniqSet++;
            }
        }
        return countUniqSet;
    }

    private void CheckHorizontalWalls(int row)
    {
        for (int i = 0; i < columns; i++)
        {
            if (CalculateHorizontalWalls(mazeLine[i], row) == 0)
            {
                horizontalWalls[row, i] = false;
            }
        }
    }

    private int CalculateHorizontalWalls(int element, int row)
    {
        int countHorizontalWalls = 0;
        for (int i = 0; i < columns; i++)
        {
            if (mazeLine[i] == element && horizontalWalls[row, i] == false)
            {
                countHorizontalWalls++;
            }
        }
        return countHorizontalWalls;
    }

    private void PrepareNewLine(int row)
    {
        for (int i = 0; i < columns; i++)
        {
            if (horizontalWalls[row, i] == true)
            {
                mazeLine[i] = 0;
            }
        }
    }

    private void AddEndLine()
    {
        AssignUniqueSet();
        AddVerticalWalls(rows - 1);
        CheckEndLine();
    }

    private void CheckEndLine()
    {
        for (int i = 0; i < columns - 1; i++)
        {
            if (mazeLine[i] != mazeLine[i + 1])
            {
                verticalWalls[rows - 1, i] = false;
                MergeSet(i, mazeLine[i]);
            }
            horizontalWalls[rows - 1, i] = true;
        }
        horizontalWalls[rows - 1, columns - 1] = true;
    }

}
