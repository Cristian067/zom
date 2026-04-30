using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set;}

    public GameObject[] spawnPoints;
    public GameObject enemyPrefab;
    public GameObject playerSpawnPoint;
    public GameObject playerPrefab;

    public int enemiesInScene;

    public int round = 0;

    private bool isPaused;

    [SerializeField] private GameObject pausePanel;

    private List<GameObject> playersAlive = new List<GameObject>();



    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            Pause();
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            Resume();
        }
    }

    private void Initialize()
    {
        CheckEnemiesCount();
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        var player = Instantiate(playerPrefab, playerSpawnPoint.transform.position, Quaternion.identity);
        playersAlive.Add(player);
        Camera.main.transform.SetParent(player.transform.GetChild(0).transform);
        Camera.main.transform.localPosition = new Vector3(0, 0.40f, 0);
    }

    public void GameOver(GameObject player)
    {
        playersAlive.Remove(player);
        if (playersAlive.Count == 0)
        {
            SceneManager.LoadScene(0);
            
        }
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


    public void RestartGame()
    {
        Time.timeScale =1;
        SceneManager.LoadScene(1);
    }

        public void BackToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        isPaused = true;
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        isPaused = false;
    }




}
