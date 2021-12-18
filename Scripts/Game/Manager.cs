using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public static Manager Instance;
    Transform playerPos;
    public int checkpoint;
    public int _scene;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        LoadCheckpoint();
        LoadScene();
        SetPlayerPos();
    }

    private void Start()
    {
        SetPlayerPos();

        if (SceneManager.GetActiveScene().name != "Menu")
            Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name != "Menu")
        {
            FindObjectOfType<Transition>().StartTransition("Menu");
            Cursor.visible = true;
        }
    }

    public void SaveInt(string saveName, int ID)
    {
        PlayerPrefs.SetInt(saveName, ID);
    }


    public void LoadCheckpoint()
    {
        if (PlayerPrefs.HasKey("checkpoint"))
            checkpoint = PlayerPrefs.GetInt("checkpoint");
        else
            checkpoint = 0;
    }

    public void LoadScene()
    {
        if (PlayerPrefs.HasKey("_scene"))
            _scene = PlayerPrefs.GetInt("_scene");
        else
            _scene = 0;
    }

    private void OnLevelWasLoaded()
    {
        SetPlayerPos();
    }

    void SetPlayerPos()
    {
        if (GameObject.Find("Player") != null)
        {
            playerPos = GameObject.Find("Player").transform;

            Checkpoint[] checkpoints = FindObjectsOfType<Checkpoint>();
            int currentCheckpoint;
            foreach(Checkpoint _points in checkpoints)
            {
                if (_points.CheckPointID == checkpoint)
                {
                    currentCheckpoint = _points.CheckPointID;
                    playerPos.transform.position = _points.gameObject.transform.position;
                }
         
            }

        }
    }

}
