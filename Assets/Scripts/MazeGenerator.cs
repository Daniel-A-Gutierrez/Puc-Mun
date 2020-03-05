using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public Texture2D mazeTexture = null;
    [SerializeField] GameObject wall = null;
    [SerializeField] GameObject floor = null;
    public MazeGraph mazeGraph;
    public Vector3 spawnPoint;
    
    void Awake()
    {
        GetComponent<MazeGraph>();
        mazeGraph = GetComponent<MazeGraph>();
        mazeGraph.Initialize();
        Generate();
    }

    public void Generate() 
    {
        Vector3 spawns = Vector3.zero;
        int points = 0;
        List<Vector3> edgeHolder = new List<Vector3>();

        for (int i = 0; i < mazeTexture.width; i++) {
            for (int j = 0; j < mazeTexture.height; j++) {
                if (mazeTexture.GetPixel(i, j).r < 0.5f) 
                {
                    Transform t = Instantiate(wall.transform, transform);
                    t.Translate(i, 0, j);
                } 
                else 
                {
                    Transform t = Instantiate(floor.transform, transform);
                    t.Translate(i, 0, j);

                    if (mazeTexture.GetPixel(i + 1, j).r > 0.5f && i<mazeTexture.width-1)
                        edgeHolder.Add(floor.transform.position + new Vector3(i + 1, 0, j) + new Vector3(0.5f - mazeTexture.width / 2, 1, 0.5f - mazeTexture.height / 2));
                    if (mazeTexture.GetPixel(i - 1, j).r > 0.5f && i > 0)
                        edgeHolder.Add(floor.transform.position + new Vector3(i - 1, 0, j)+ new Vector3(0.5f - mazeTexture.width / 2, 1, 0.5f - mazeTexture.height / 2));
                    if (mazeTexture.GetPixel(i, j + 1 ).r > 0.5f && j < mazeTexture.height - 1)
                        edgeHolder.Add(floor.transform.position + new Vector3(i, 0, j + 1)+ new Vector3(0.5f - mazeTexture.width / 2, 1, 0.5f - mazeTexture.height / 2));
                    if (mazeTexture.GetPixel(i, j - 1).r > 0.5f && j > 0)
                        edgeHolder.Add(floor.transform.position + new Vector3(i, 0, j - 1)+ new Vector3(0.5f - mazeTexture.width / 2, 1, 0.5f - mazeTexture.height / 2));
                    mazeGraph.AddNode(t.position+ new Vector3(0.5f - mazeTexture.width / 2,1, 0.5f - mazeTexture.height / 2) , edgeHolder.ToArray());
                    edgeHolder.Clear();
                    
                }

                if (mazeTexture.GetPixel(i, j).b >= 0.5f) {
                    spawns += new Vector3(i, 1, j);
                    points++;
                }
            }
        }

        mazeGraph.Simplify();
        
        transform.Translate(0.5f - mazeTexture.width / 2, 0, 0.5f - mazeTexture.height / 2);
        spawnPoint = spawns / points + transform.position;
    }
}
