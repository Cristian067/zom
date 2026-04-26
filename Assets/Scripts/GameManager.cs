using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject[] spawnPoints;
    public GameObject enemyPrefab;
    public GameObject playerSpawnPoint;
    public GameObject playerPrefab;

    public int enemiesInScene;

    public int round = 0;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Initialize()
    {
        CheckEnemiesCount();
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        var player = Instantiate(playerPrefab, playerSpawnPoint.transform.position, Quaternion.identity);
        Camera.main.transform.SetParent(player.transform.GetChild(0).transform);
        Camera.main.transform.localPosition = new Vector3(0, 0.40f, 0);
    }
    public void CheckEnemiesCount()
    {
        if (enemiesInScene <= 0)
        {
            round++;
            for (int i = 0; i < round; i++)
            {
                SpawnEnemy();
            }
        }
    }




    private void SpawnEnemy()
    {
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(enemyPrefab, spawnPoints[spawnIndex].transform.position, Quaternion.identity);
        enemiesInScene++;
    }
}
