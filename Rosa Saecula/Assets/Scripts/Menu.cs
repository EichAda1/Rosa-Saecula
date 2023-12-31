using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [Header("Menu Buttons")]
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button loadGameButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button exitGameButton;

    private void Start()
    {
        if (!DataPersistenceManager.instance.HasGameData())
        {
            loadGameButton.interactable = false;
        }
    }

    public void OnNewGameClicked()
    {
        DisableMenuButtons();
        DataPersistenceManager.instance.NewGame();
        DataPersistenceManager.instance.SaveGame();
        SceneManager.LoadSceneAsync("StartingArea");
    }

    public void OnLoadGameClicked() 
    {
        DisableMenuButtons();
        DataPersistenceManager.instance.LoadGame();
        SceneManager.LoadSceneAsync(DataPersistenceManager.instance.gameData.sceneID);
    }

    //public void OnOptionsClicked()
    //{
    //    DisableMenuButtons();
    //    SceneManager.LoadSceneAsync("Options");
    //}

    public void GameQuit()
    {
        DisableMenuButtons();
        Application.Quit();
    }

    private void DisableMenuButtons()
    {
        newGameButton.interactable = false;
        loadGameButton.interactable = false;
        optionsButton.interactable = false;
        exitGameButton.interactable = false;
    }
}