using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class MazeRenderer : MonoBehaviour
{
    [SerializeField]
    [Range(1,50)]
    public int width = 10;

    [SerializeField]
    [Range(1,50)]
    public int height = 10;

    [SerializeField]
    private float size = 1f;

    [SerializeField]
    private Transform wallPrefab = null;

    [SerializeField]
    private Transform floorPrefab = null;

    [SerializeField]
    private Transform playerSpawnPrefab = null;

    [SerializeField]
    private Transform playerGoalPrefab = null;

    [SerializeField]
    private Transform playerObject = null;

    private List<GameObject> mazeObjects;
    private List<Transform> playerPoints;

    // Start is called before the first frame update
    void Start()
    {
        mazeObjects = new List<GameObject>();
        playerPoints = new List<Transform>();
        var mazeRenderer = GameObject.Find("MazeRenderer");
        var player = GameObject.Find("Player");
        var plane = GameObject.Find("Plane");
        var maze = MazeGenerator.Generate(width, height);
        Draw(maze);
        // DontDestroyOnLoad(mazeRenderer);
        // DontDestroyOnLoad(player);
        // DontDestroyOnLoad(plane);
    }

    public void Redraw(int nw, int nh) {
         for(int i = 0; i <  mazeObjects.Count; i++)
        {
            Destroy(mazeObjects[i].gameObject);
        }
        mazeObjects.Clear();

         for(int i = 0; i <  playerPoints.Count; i++)
        {
            Destroy(playerPoints[i].gameObject);
        }
        playerPoints.Clear();
        width = width + 10;
        height = height + 10;
        var maze = MazeGenerator.Generate(width, height); 
        // SceneManager.LoadScene("Level2Scene");
        Draw(maze);
    }

    private void Draw(WallState[,] maze)
    {
        var rng = new System.Random(/*seed*/);
        int spawnX = rng.Next(0, width);
        int spawnY = rng.Next(0, height);
        var playerPosition = new Vector3(-width/2 + spawnX, 0, -height/2 + spawnY);
        var playerSpawnPoint = Instantiate(playerSpawnPrefab, transform) as Transform;
        playerSpawnPoint.position = playerPosition + new Vector3(0,0.005f,0);
        playerPoints.Add(playerSpawnPoint);

        int goalX = rng.Next(1, width-1);
        int goalY = rng.Next(1, height-1);
        var goalPosition = new Vector3(-width/2 + goalX, 0.1f, -height/2 + goalY);
        var goalSpawnPoint = Instantiate(playerGoalPrefab, transform) as Transform;
        goalSpawnPoint.position = goalPosition;
        goalSpawnPoint.localScale = new Vector3(0.5f, 1f, 0.5f);
        playerPoints.Add(goalSpawnPoint);

        playerObject.position = playerSpawnPoint.position;
        // playerSpawnPoint.localScale = new Vector3(playerSpawnPoint.localScale.x, playerSpawnPoint.localScale.y, playerSpawnPoint.localScale.z);

        var floor = Instantiate(floorPrefab, transform);
        floor.localScale = new Vector3(width, 1, height);

        for (int i=0; i < width; ++i) {
            for(int j=0; j < height; ++j) {
                var cell = maze[i,j];
                var position = new Vector3(-width/2 + i, 0, -height/2 + j);

                if(cell.HasFlag(WallState.UP)) {
                    var topWall = Instantiate(wallPrefab, transform) as Transform;
                    playerPoints.Add(topWall);
                    topWall.position = position + new Vector3(0,0,size/2);
                    topWall.localScale = new Vector3(size, topWall.localScale.y, topWall.localScale.z);
                }

                if(cell.HasFlag(WallState.LEFT)) {
                    var leftWall = Instantiate(wallPrefab, transform) as Transform;
                    playerPoints.Add(leftWall);
                    leftWall.position = position + new Vector3(-size/2,0,0);
                    leftWall.localScale = new Vector3(size, leftWall.localScale.y, leftWall.localScale.z);
                    leftWall.eulerAngles = new Vector3(0,90,0);
                }

                if(i == width - 1) {
                    if(cell.HasFlag(WallState.RIGHT)) {
                        var rightWall = Instantiate(wallPrefab, transform) as Transform;
                        playerPoints.Add(rightWall);
                        rightWall.position = position + new Vector3(size/2,0,0);
                        rightWall.localScale = new Vector3(size, rightWall.localScale.y, rightWall.localScale.z);
                        rightWall.eulerAngles = new Vector3(0,90,0);
                    }
                }

                if(j == 0) {
                    if(cell.HasFlag(WallState.DOWN)) {
                        var bottomWall = Instantiate(wallPrefab, transform) as Transform;
                        playerPoints.Add(bottomWall);
                        bottomWall.position = position + new Vector3(0,0,-size/2);
                        bottomWall.localScale = new Vector3(size, bottomWall.localScale.y, bottomWall.localScale.z);
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
