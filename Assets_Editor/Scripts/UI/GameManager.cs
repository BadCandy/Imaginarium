using UnityEngine;
using System.Collections;

public class GameManager : Singleton<GameManager>
{ 
    protected GameManager(){}
    public enum GameState { Playing, Pause };
    private GameState _state;
    private bool paused = false;
    public LevelManager levelManager;
    public CameraManager cameraManager;
    public FollowCamera followCamera;
    public GameState State
    {
        get
        {
            return _state;
        }
        set
        {
            if(value == GameState.Playing)
            {
                PauseUI.SetActive(false);
                Time.timeScale = 1;
            }
            else if(value == GameState.Pause)
            {
                PauseUI.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

	public GameObject PauseUI;
	//GameObject Manager;
    
	// Use this for initialization
	void Start () {
		PauseUI.SetActive(false);
        levelManager.LevelLoad(StageManager.stageNameList[StageManager.currentStageNumber-1]);
        levelManager.TogglePlay();
        cameraManager.ChangeCameraSize();
        followCamera.FollowCameraToPlayer();
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void setPauseState(bool paused) {

		this.paused = paused;
	
		
		if (paused) { 
			PauseUI.SetActive(true);
			Time.timeScale = 0;
		}else {
			PauseUI.SetActive(false);
			Time.timeScale = 1;
		}

		Debug.Log ("Pause : " + paused);
    }
}
