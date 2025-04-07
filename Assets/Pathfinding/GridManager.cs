using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private static GridManager _instance;
    public static GridManager Instance
    {
        get
        {
            _instance = FindObjectOfType<GridManager>();
            if (_instance != null)
            {
                return _instance;
            }

            return null;
        }
    }
    
    public int nRows;
    public int nColums;
    public float gridCellSize;
    public Node[,] Nodes { get; set; }
    public Vector3 Origin { get; set; }
    private GameObject[] obstacleList;

    private void Awake()
    {
        Origin = new Vector3();
        obstacleList = GameObject.FindGameObjectsWithTag("Obstacle");
        SetupNodes();
    }

    private void SetupNodes()
    {
        Nodes = new Node[nRows, nColums];
        int index = 0;
        for (int i = 0; i < nColums; i++)
        {
            for (int j = 0; j < nRows; j++)
            {
                Vector3 cellPos = GetGridCellCenterPos(index);
                Node node = new Node(cellPos);
                Nodes[i, j] = node;
                index++;
            }
        }

        if (obstacleList != null && obstacleList.Length > 0)
        {
            foreach (GameObject obstacle in obstacleList)
            {
                int cellIndex = GetCellIndex(obstacle.transform.position);
                int col = GetColumn(cellIndex);
                int row = GetRow(cellIndex);
                Nodes[row, col].MaskAsObstacle();
            }
        }
    }

    private bool IsInBounds(Vector3 pos)
    {
        float width = nColums * gridCellSize;
        float height = nRows * gridCellSize;
        bool insideWidth = pos.x >= Origin.x && pos.x <= Origin.x + width;
        bool insideHeigth = pos.z >= Origin.z && pos.z <= Origin.z + height;
        return insideWidth && insideHeigth;
    }

    public int GetCellIndex(Vector3 pos)
    {
        if (!IsInBounds(pos))
        {
            return -1;
        }

        int col = (int) (pos.x / gridCellSize);
        int row = (int) (pos.z / gridCellSize);
        return row * nColums + col;
    }
    
    private int GetRow(int index)
    {
        int row = index / nColums;
        return row;
    }

    private int GetColumn(int index)
    {
        int column = index % nColums;
        return column;
    }

    public Vector3 GetGridCellPosition(int index)
    {
        int row = GetRow(index);
        int col = GetColumn(index);
        float xPosInGrid = col * gridCellSize;
        float zPosInGrid = row * gridCellSize;
        return Origin + new Vector3(xPosInGrid, 0f, zPosInGrid);
    }

    public Vector3 GetGridCellCenterPos(int index)
    {
        Vector3 cellPos = GetGridCellPosition(index);
        cellPos.x += gridCellSize / 2f;
        cellPos.z += gridCellSize / 2f;
        return cellPos;
    }

    private void AssignNeighbor(int row, int column, List<Node> neighbors)
    {
        if (row != -1 && column != -1 && row < nRows && column < nColums)
        {
            Node nodeToAdd = Nodes[row, column];
            if (!nodeToAdd.isObstacle)
            {
                neighbors.Add(nodeToAdd);
            }
        }
    }

    public void GetNeighbors(Node node, List<Node> neighbors)
    {
        Vector3 nodePos = node.position;
        int nodeIndex = GetCellIndex(nodePos);

        int row = GetRow(nodeIndex);
        int column = GetColumn(nodeIndex);

        // Bottom
        int nodeRow = row - 1;
        int nodeColumn = column;
        AssignNeighbor(nodeRow, nodeColumn, neighbors);
        
        // Top
        nodeRow = row + 1;
        nodeColumn = column;
        AssignNeighbor(nodeRow, nodeColumn, neighbors);
        
        // Right
        nodeRow = row;
        nodeColumn = column + 1;
        AssignNeighbor(nodeRow, nodeColumn, neighbors);
        
        // Left
        nodeRow = row;
        nodeColumn = column - 1;
        AssignNeighbor(nodeRow, nodeColumn, neighbors);
    }
    
    #region Draw Grid

    private void DebugDrawGrid(Vector3 origin, int nRows, int nColums, float cellSize, Color color)
    {
        float width = nColums * cellSize;
        float height = nRows * cellSize;

        for (int i = 0; i < nRows + 1; i++)
        {
            Vector3 startPos = origin + i * cellSize * Vector3.forward;
            Vector3 endPos = startPos + width * Vector3.right;
            Debug.DrawLine(startPos, endPos, color);
        }

        for (int i = 0; i < nColums + 1; i++)
        {
            Vector3 startPos = origin + i * cellSize * Vector3.right;
            Vector3 endPos = startPos + height * Vector3.forward;
            Debug.DrawLine(startPos, endPos, color);
        }
    }

    #endregion
    
    private void OnDrawGizmos()
    {
        DebugDrawGrid(transform.position, nRows, nColums, gridCellSize, Color.gray);

        if (Nodes != null)
        {
            foreach (Node node in Nodes)
            {
                if (node.isObstacle)
                {
                    Gizmos.DrawSphere(node.position, 1f);
                }
            }
        }
    }
}
