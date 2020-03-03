using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public Texture2D mazeTexture = null;
    [SerializeField] GameObject wall = null;
    [SerializeField] GameObject floor = null;

    public Vector3 spawnPoint;
    
    void Awake()
    {
        Generate();
    }

    public void Generate() {
        Vector3 spawns = Vector3.zero;
        int points = 0;

        for (int i = 0; i < mazeTexture.width; i++) {
            for (int j = 0; j < mazeTexture.height; j++) {
                if (mazeTexture.GetPixel(i, j).r < 0.5f) {
                    Transform t = Instantiate(wall.transform, transform);
                    t.Translate(i, 0, j);
                } else {
                    Transform t = Instantiate(floor.transform, transform);
                    t.Translate(i, 0, j);
                }

                if (mazeTexture.GetPixel(i, j).b >= 0.5f) {
                    spawns += new Vector3(i, 1, j);
                    points++;
                }
            }
        }
        
        transform.Translate(0.5f - mazeTexture.width / 2, 0, 0.5f - mazeTexture.height / 2);
        spawnPoint = spawns / points + transform.position;
    }
}
