using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour{
 
	private bool paused = false;
  
	// Use this for initialization
	void Start () {
	

	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("Pause"))
        {
            paused = !paused;

            GameManager.Instance.setPauseState(paused);
            if(paused) GameManager.Instance.State = GameManager.GameState.Pause;
            else GameManager.Instance.State = GameManager.GameState.Playing;
        }
   
    }
    public void PauseButton()
    {
        paused = !paused;
        GameManager.Instance.setPauseState(paused);
        if (paused) GameManager.Instance.State = GameManager.GameState.Pause;
        else GameManager.Instance.State = GameManager.GameState.Playing;
    }

    public void Resume() {
        paused = false;
		GameManager.Instance.setPauseState(paused);
        GameManager.Instance.State = GameManager.GameState.Playing;
    }

    public void ReStart() {
        // 게임 재 시작
    }

    public void Sound() {
        // 사운드 OFF
    }

    public void Quit() {
        //Application.LoadLevel(SceneNumber);
    }

    public bool getPauseState() {
        return paused;
    }
}
