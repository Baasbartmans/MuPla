using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

    bool dead = false;
    public GUIStyle style;
    private float time;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (dead && time <= 0)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        time--;
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Time.timeScale = 0;
            time = 200;
            dead = true;
        }
    }

    private void OnGUI()
    {
        if(dead)
        GUI.Label(new Rect(Screen.width/2-50, Screen.height/2-50, 100, 100), "Game over", style);
    }
}
