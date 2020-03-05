using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MazeGraph : MonoBehaviour
{
    //Dictionary<Vector3, int> vertexNumbers;
    //Tuple<Vector3,Vector3>[] Edges; //flawed try hashset? 
    //List<Vector3> Vertices;
    
    HashSet<Vector3> Graph;
    Dictionary<Vector3,Vector3[]> edges; 
    //maze graph represents an undirected graph of intersections
    //you populate it with positions, then it figures out on its own if there is a straight, minimal path between them.
    bool running = false;
    void Start() //initialized called in maze generator's awake
    {
        //running = true;
        //Graph= new HashSet<Vector3>();
    }

    //O(1) yay
    public void AddNode(Vector3 node, Vector3[] edges)
    {
        if(Graph.Contains(node) || Contains(node,edges))
        {
            print("Cant add the same node twice");
            return; //cant add the same node twice , or at least without removing it first
        }
        Graph.Add(node);
        this.edges[node] = edges;
    }

    public void AddNode(Vector3 node)
    {
        if(Graph.Contains(node))
        {
            print("Cant add the same node twice");
            return; //cant add the same node twice , or at least without removing it first
        }
        Graph.Add(node);
        this.edges[node] = new Vector3[4];
    }

    public void Initialize()
    {
        running = true;
        Graph = new HashSet<Vector3>();
        edges = new Dictionary<Vector3, Vector3[]>();
    }

    //assumes edges exist between spatially adjacent vertices with a path between them. 
    //simplifies vertices with 2 edges that dont turn to 1 edge.
    //generates garbage, assumes no node has an edge to itself, and nodes have at most 4 edges.
    public void Simplify()
    {
        
        List<Vector3> removeAfter = new List<Vector3>();
        foreach(Vector3 node in Graph)
        {
            if(edges[node].Length == 2)
            {
                if(  Mathf.Abs(Vector3.Dot( (edges[node][0] - node).normalized
                    , (node - edges[node][1]).normalized )) > .99f  ) //cheap way of checking if theyre colinear
                {
                    //replace node with its outbound verts.  so remove node[graph] and edges[node] , and set   edges[edges[node][0]] [ index of node ] = edges[node][1] and vise versa
                    removeAfter.Add(node);
                    int nodeInA = IndexOf(node,edges[edges[node][0]]); //index of node in the edge array of its neighbor
                    int nodeInB = IndexOf(node, edges[edges[node][1]]);
                    if(nodeInA < 0 ||nodeInB < 0 )
                    {
                        print("Graph Error : Node not in edge list of linked node");
                        continue;
                    }
                    edges[edges[node][0]][nodeInA] = edges[node][1];
                    edges[edges[node][1]][nodeInB] = edges[node][0];
                    edges.Remove(node);
                }
            }
        }
        foreach(Vector3 node in removeAfter)
        {
            Graph.Remove(node);
        }
    }

    int IndexOf(Vector3 v,Vector3[] array)
    {
        for(int i = 0 ; i < array.Length ; i++)
        {
            if(array[i] == v)
                return i;
        }
        return -1;
    }

    bool Contains(Vector3 v , Vector3[] array)
    {
        foreach(Vector3 element in array)
            if(v==element)
                return true;
        return false;
    }

    void OnDrawGizmosSelected()
    {
        if (running)
        {
            foreach (Vector3 node in Graph)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawSphere(node, .5f);
                Gizmos.color = Color.red;
                foreach(Vector3 neighbor in edges[node])
                {
                    Gizmos.DrawLine(node, neighbor);
                }
            }
        }
    }
}
