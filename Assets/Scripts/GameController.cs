using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    MenuManager myMenuManager;
    [SerializeField]
    PlayerController playerController;
    [SerializeField]
    GameObject spawnManager;
    // Start is called before the first frame update
    void Start()
    {
        //Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void GameStart()
    {
        //playerController.SetActive(true);
        myMenuManager.Load_GameSreen();
        //spawnManager.SetActive(true);
    }
    public void GameOver( SpawnManager spawnManager, PlayerController playerController)
    {
        //появилосьменю
        // остановить рекорд
        myMenuManager.PauseOn();
        Destroy(spawnManager);
        Destroy(gameObject);

    }
}
