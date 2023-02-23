using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph
{
    public enum NodeType
    {
        Spawn,
        Room,
        End,
    }
    public struct Node
    {
        public Node(Vector2Int pos, NodeType nodeType)
        {
            this.pos = pos;
            this.nodeType = nodeType;

            this.x = pos.x;
            this.y = pos.y;
        }
        public Vector2 pos;
        public NodeType nodeType;
        public int x { get; private set; }
        public int y { get; private set; }
    }

    public int nodeCount;
    public Dictionary<Node, List<Node>> adjList;

    public Graph()
    {
        adjList = new Dictionary<Node, List<Node>>();
    }
    public Node AddNode(Node node)
    {
        if (!adjList.ContainsKey(node))
            adjList[node] = new List<Node>();
        Debug.Log($"{node.x}, {node.y}");
        return node;
    }
    public void AddEdge(Node src, Node dest)
    {
        if (!adjList.ContainsKey(src))
            adjList[src] = new List<Node>();
        adjList[src].Add(dest);

        if (!adjList.ContainsKey(dest))
            adjList[dest] = new List<Node>();
        adjList[dest].Add(src);
    }
    public Node RandomNode(int minBoundary, int maxBoundary, NodeType nodeType = NodeType.Room)
    {
        int x = Random.Range(minBoundary, maxBoundary);
        int y = Random.Range(minBoundary, maxBoundary);
        Node newNode = new Node(new Vector2Int(x, y), nodeType);
        return newNode;
    }
    public void GenerateRandomNodes(int nodes, int minBoundary, int maxBoundary, NodeType nodeType = NodeType.Room)
    {
        for (int i = 0; i < nodes; i++)
        {
            this.AddNode(RandomNode(minBoundary, maxBoundary, nodeType));
        }
    }
    public void EdgeNodes()
    {
        foreach(var node in this.adjList)           
        {
            foreach(var otherNode in this.adjList)
            {
                if(!node.Equals(otherNode))
                {
                    this.AddEdge(node.Key, otherNode.Key);
                }
            }
        }
    }
    public void DelaunayTriangulationAlgorithm()
    {
         
    }
}
