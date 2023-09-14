using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Game : MonoBehaviour
{

    public enum State { Menu, Action, LevelFinished }
    public State currentGameState = State.Action;
    public int Score = 0;
    public int ScoreLeft = 0;
    public SpawnablesLibary ref_SpawnablesLibrary;
    public audioengine ref_audioengine;
    public charactercontrollermovement ref_playermovement;
    public Vector3 mostrecentgroundposition = Vector3.zero;
    public Camera ref_MainCamera;
    public Manager_Level ref_Level;
    public VirtualCameraManager ref_VirtualCameraManager;
    private static Manager_Game _instance;
    public static Manager_Game Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("No manager instance exists");
            }
            return _instance;
        }
    }
    public void HurtPlayer()
    {
        print("OUCH");
    }

    private void Awake()
    {
        _instance = this;
        ref_MainCamera = Camera.main;


            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
    }

    public void Coin_PickedUp()
    {
        Score++;
        ScoreLeft--;
        if (ScoreLeft == 0) { EndGame(); }
    }

    public void EndGame() 
    {
        currentGameState = State.LevelFinished;
    }

    public void respawntoground()
    {
        ref_playermovement.gameObject.transform.position = mostrecentgroundposition;   
    }
}
