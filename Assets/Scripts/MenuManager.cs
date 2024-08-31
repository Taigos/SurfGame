using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    [SerializeField]
    GameObject menuPause;
    [SerializeField]
    GameObject gameBar;
    [SerializeField]
    GameObject preGameText;

    public void PauseOn()
    {
        Time.timeScale = 0f;
        menuPause.SetActive(true);
        gameBar.SetActive(false);

        //(UnityEngine.UI.Text)(menuPause.GetComponentsInChildren<Text>()).text 
    }
    public void Return_ButtonClick()
    {
        Time.timeScale = 1.0f;
        preGameText.SetActive(false);
        menuPause.SetActive(false);
        gameBar.SetActive(true);
    }
    public void GeneralMenu_ButtonClick()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
        preGameText.SetActive(true);
        //Application.Quit(0);
    }
    public void Load_GameSreen()
    {
        SceneManager.LoadScene(1);
    }
}
