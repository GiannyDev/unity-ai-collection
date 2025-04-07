using System;
using System.Collections.Generic;
using UnityEngine;

public class Astar
{
    public static PriorityQueue openList;
    public static HashSet<Node> closedList;

    private static float HeuristicEstimateCost(Node curNode, Node goalNode)
    {
        Vector3 dirToGoalNode = goalNode.position - curNode.position;
        return dirToGoalNode.magnitude;
    }

    private static List<Node> CalculatePath(Node node)
    {
        List<Node> nodeList = new List<Node>();
        while (node != null)
        {
            nodeList.Add(node);
            node = node.parent;
        }
        
        nodeList.Reverse();
        return nodeList;
    }

    public static List<Node> FindPath(Node start, Node goalNode)
    {
        openList = new PriorityQueue();
        openList.Push(start);
        start.nodeTotalCost = 0f;
        start.estimatedCost = HeuristicEstimateCost(start, goalNode);

        closedList = new HashSet<Node>();
        Node node = null;
        while (openList.Count > 0)
        {
            node = openList.First();
            if (node.position == goalNode.position)
            {
                return CalculatePath(node);
            }

            List<Node> neighbors = new List<Node>();
            GridManager.Instance.GetNeighbors(node, neighbors);

            for (int i = 0; i < neighbors.Count; i++)
            {
                Node neighborNode = neighbors[i];
                if (!closedList.Contains(neighborNode))
                {
                    float movingCost = HeuristicEstimateCost(node, neighborNode);
                    float totalMovingCost = node.nodeTotalCost + movingCost;
                    float neighborNodeEstimateCost = HeuristicEstimateCost(neighborNode, goalNode);

                    neighborNode.nodeTotalCost = totalMovingCost;
                    neighborNode.parent = node;
                    neighborNode.estimatedCost = totalMovingCost + neighborNodeEstimateCost;

                    if (!openList.Contains(neighborNode))
                    {
                        openList.Push(neighborNode);
                    }
                }
            }

            closedList.Add(node);
            openList.Remove(node);
        }

        return CalculatePath(node);
    }
}
