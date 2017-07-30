using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class HUDController : MonoBehaviour 
{
	public GUISkin skin;
	public gameController gameController;
	public Text coins;
	public Text lifes;
	
	bool pause;
	Rect windowPause, windowWin, windowLoose;

    bool cancel;

    public int playerScore = 0;
	public int highScore = 0;
	string highScoreKey = "HighScore";

	void Start() {

		pause = false;
		Time.timeScale = 1;
		windowPause = new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 150);
		windowWin = new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 200);
        windowLoose = new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 105);

        highScore = PlayerPrefs.GetInt(highScoreKey,0);
    }

    void Update() {

        cancel = Input.GetButtonDown("Cancel");
    }

    void FixedUpdate () {

		if(cancel)
		{
			pause = !pause;

			if(pause) {

                Time.timeScale = 0;
            }
			//else {
            //
            //    Time.timeScale = 1;
            //}
		}

		if(gameController.win || gameController.loose)
		{
			Time.timeScale = 0;
		}

        coins.text = gameController.coins.ToString();
		lifes.text = gameController.lifes.ToString();
	}
	
	void OnGUI() {

		GUI.skin = skin;

		if(pause)
		    windowPause = GUI.Window (0, windowPause, PauseFunctions, "Pause Menu");

		if(gameController.win)
			windowWin = GUI.Window (0, windowWin, WinFunctions, "You won!");

        if(gameController.loose)
            windowLoose = GUI.Window(0, windowLoose, LooseFunctions, "You lost!");
    }
	
	void PauseFunctions(int id) {

		if(GUILayout.Button("Resume")) {
			pause = false;
			Time.timeScale = 1;
		}

		if(GUILayout.Button("Restart")) {
			gameController.Die();
			pause = false;
			Time.timeScale = 1;
		}

		if(GUILayout.Button("Quit")) {
			SceneManager.LoadScene("Menu");
		}
	}

	void WinFunctions(int id) {

		string message = "";

		playerScore = gameController.coins * gameController.lifes;

		if (playerScore < 0)
			message = "Disgusting!!!";
		else if (playerScore > 0)
			message = "Awful!";
		else if (playerScore > 25)
			message = "Miserable!";
		else if (playerScore > 50)
			message = "Unacceptable!";
		else if (playerScore > 75)
			message = "Eeeh...";
		else if (playerScore > 100)
			message = "Wonderful!";
		else if (playerScore > 200)
			message = "Awesome!";
        else
            message = "???";

        GUILayout.Label (message);
		GUILayout.Label ("Your Score: " + playerScore.ToString ());

		if(playerScore > highScore) {

			GUILayout.Label("New record!");

			PlayerPrefs.SetInt(highScoreKey, playerScore);
			PlayerPrefs.Save();

			GUILayout.Space(5);
		}
		else
			GUILayout.Space (35);

		if(GUILayout.Button("Play")) {

			string currentLevel = SceneManager.GetActiveScene().name;
			int level = 0;

			int.TryParse(currentLevel, out level);

			level++;

			if(level < 10)
				SceneManager.LoadScene(level.ToString());
			else
				SceneManager.LoadScene("Menu");
		}
		
		if(GUILayout.Button("Quit")) {
			
			SceneManager.LoadScene("Menu");
		}
	}

    void LooseFunctions(int id) {

        if (GUILayout.Button("Try again"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (GUILayout.Button("Quit"))
        {
            SceneManager.LoadScene("Menu");
        }
    }
}