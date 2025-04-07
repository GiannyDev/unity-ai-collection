using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAStar : MonoBehaviour
{
    public List<Node> pathList;

    public Node StartNode { get; set; }
    public Node GoalNode { get; set; }

    [SerializeField] private GameObject startCube;
    [SerializeField] private GameObject endCube;

    private float elapsedTime;
    private float intervalTime = 1f;
    
    private void Start()
    {
        pathList = new List<Node>();
        FindPath();
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > intervalTime)
        {
            elapsedTime = 0f;
            FindPath();
        }
    }

    private void FindPath()
    {
        Vector3 startPos = startCube.transform.position;
        Vector3 endPos = endCube.transform.position;

        StartNode = new Node(GridManager.Instance.GetGridCellCenterPos(GridManager.Instance.GetCellIndex(startPos)));
        GoalNode = new Node(GridManager.Instance.GetGridCellCenterPos(GridManager.Instance.GetCellIndex(endPos)));
        pathList = Astar.FindPath(StartNode, GoalNode);
    }

    private void OnDrawGizmos()
    {
        if (pathList == null)
        {
            return;
        }

        if (pathList.Count > 0)
        {
            int index = 1;
            foreach (Node node in pathList)
            {
                if (index < pathList.Count)
                {
                    Node nextNode = pathList[index];
                    Debug.DrawLine(node.position, nextNode.position, Color.magenta);
                    index++;
                }
            }
        }
    }
}
