using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    //public Transform[] spawnPoints;
    public Transform spawnPoint_A;
    public Transform spawnPoint_B;
    public Transform spawnPoint_C;
    public Transform spawnPoint_D;

    public GameObject player;

    public static LevelManager instance;


    // Start is called before the first frame update
    void Start()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void loadLevel_1()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void respawn(GameObject player)
    {
        int mySpawnPosition = Random.Range(0, 3);
        player.transform.rotation = Quaternion.identity;

        if (mySpawnPosition == 0)
        {
            player.transform.position =spawnPoint_A.position;
        }
        else if (mySpawnPosition == 1)
        {
            player.transform.position = spawnPoint_B.position;
        }
        else if (mySpawnPosition == 2)
        {
            player.transform.position = spawnPoint_C.position;
        }
        if (mySpawnPosition == 3)
        {
            player.transform.position = spawnPoint_D.position;
        }
    }


    // Update is called once per frame
    void Update()
    {
       
    }
}
