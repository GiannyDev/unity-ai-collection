using System;
using UnityEngine;

public class Node : IComparable
{
    public float nodeTotalCost;
    public float estimatedCost;
    public bool isObstacle;
    public Node parent;
    public Vector3 position;

    public Node(Vector3 pos)
    {
        estimatedCost = 0f;
        nodeTotalCost = 1f;
        isObstacle = false;
        parent = null;
        position = pos;
    }

    public void MaskAsObstacle()
    {
        isObstacle = true;
    }
    
    public int CompareTo(object obj)
    {
        Node newNode = (Node) obj;
        if (estimatedCost < newNode.estimatedCost)
        {
            return -1;
        }

        if (estimatedCost > newNode.estimatedCost)
        {
            return 1;
        }

        return 0;
    }
}
