using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class DataPersistenceManage : MonoBehaviour
{
    [Header("Debuggin")]
    [SerializeField] private bool intializeDataIfNull;

    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    private GameData gameData;
    public static DataPersistenceManage instance { get; private set; }

    private List<IDatapersistence> dataPersistenceObjects;

    private FileDataHandler dataHandler;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Found more than one Data Persistence Manager in the scene, Destory the newest");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Debug.Log("OnSceneLoaded");
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    public void OnSceneUnloaded(Scene scene)
    {
        //Debug.Log("OnSceneUnloaded");
        SaveGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        // Load any save data from a file using the data handller
        this.gameData = dataHandler.Load();

        if (this.gameData == null && intializeDataIfNull)
        {
            Debug.Log("started a new game using the Debuging data persistor");
            NewGame();
        }

        // if no data can be loaded, initialize to a new game
        if(this.gameData == null)
        {
            Debug.Log("No data was found. A New Game needs to be Started before data can be loaded");
            return;
        }

        // push the loaded data to all other scripts that need it
        foreach (IDatapersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }

        // Debug.Log("Loaded gold count = " + gameData.gold);
    }

    public void SaveGame()
    {
        if(this.gameData == null)
        {
            Debug.LogWarning("No data was found. A New Game needs to be started before data can be saved.");
            return;
        }
        // TODO - pass thge data to other scripts so they can update it
        foreach (IDatapersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }

        // Debug.Log("Saved gold count = " + gameData.gold);

        // save that data to a file using the data handler
        dataHandler.Save(gameData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDatapersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDatapersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDatapersistence>();

        return new List<IDatapersistence>(dataPersistenceObjects);
    }
    public bool HasGameData()
    {
        return this.gameData != null;
    }
}
