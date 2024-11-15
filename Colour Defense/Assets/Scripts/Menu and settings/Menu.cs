using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [Header("Menu buttons")]
    [SerializeField] private List<Button> buttons = new List<Button>();


    private void Start()
    {
        if (!DataPersistenceManage.instance.HasGameData())
        {
            buttons[0].interactable = false;
        }
    }
    public void NewGameButton()
    {
        DisableAllButtons();
        // Create a new game - which will initialize our game data
        DataPersistenceManage.instance.NewGame();


        SceneManager.LoadScene(1);
    }

    public void ContinueButton()
    {
        DisableAllButtons();
        // Load the next scene - which in turn loads the game because of 
        // OnSceneLoad() in the DataPersistenceManager
        SceneManager.LoadScene(1);
    }

    public void QuitButton()
    {
        DisableAllButtons();
        Application.Quit();
    }

    private void DisableAllButtons()
    {
        foreach (Button button in buttons)
        {
            button.interactable = false;
        }
    }
}
