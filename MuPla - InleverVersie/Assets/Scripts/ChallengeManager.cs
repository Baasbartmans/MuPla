using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChallengeManager : MonoBehaviour {

    public List<GameObject> challenges;

    public float scrollSpeed;
    public float increaseSpeed;

    public bool challengeActive;

    System.Random r = new System.Random();

    public Transform spawn;

    GameObject currentChallenge;
    GameObject nextChallenge;
    GameObject thirdChallenge;

    public Transform center;

    public GameObject backgroundSprite;
    private GameObject background;
    private GameObject nextBackground;
    public float backgroundScrollSpeed;
    bool spawnBackground = true;

    private void Start()
    {
        background = Instantiate(backgroundSprite, center.position + new Vector3(0, 0, 10), Quaternion.identity);
        currentChallenge = Instantiate(challenges[0], center);
        nextChallenge = Instantiate(challenges[1], new Vector3(center.position.x + 20, center.position.y, center.position.z), Quaternion.identity);
        thirdChallenge = Instantiate(challenges[2], new Vector3(center.position.x + 40, center.position.y, center.position.z), Quaternion.identity);
    }

    private void Update()
    {
        scrollSpeed += increaseSpeed * 0.001f;
        backgroundScrollSpeed += increaseSpeed * 0.001f;

        if(currentChallenge != null) currentChallenge.transform.position -= Vector3.right * (scrollSpeed * Time.deltaTime);
        if(nextChallenge != null) nextChallenge.transform.position -= Vector3.right * (scrollSpeed * Time.deltaTime);
        if(thirdChallenge != null) thirdChallenge.transform.position -= Vector3.right * (scrollSpeed * Time.deltaTime);
        if(background != null) background.transform.position -= Vector3.right * (backgroundScrollSpeed * Time.deltaTime);
        if(nextBackground != null) nextBackground.transform.position -= Vector3.right * (backgroundScrollSpeed * Time.deltaTime);


        if (currentChallenge.transform.position.x <= -20)
        {
            DestroyImmediate(currentChallenge);
            currentChallenge = nextChallenge;
            nextChallenge = thirdChallenge;
            thirdChallenge = Spawn();
        }

        if (background.transform.position.x <= 0 && spawnBackground)
        {
            spawnBackground = false;
            nextBackground = Instantiate(backgroundSprite, background.transform.position + new Vector3(26, 0, 0), Quaternion.identity);
        }

        if (background.transform.position.x <= -26)
        {
            DestroyImmediate(background);
            background = nextBackground;
            spawnBackground = true;
        }
    }

    GameObject Spawn()
    {
        int i = r.Next(0, challenges.Count);
        var gameObject = Instantiate(challenges[i], new Vector3(nextChallenge.transform.position.x + 20, nextChallenge.transform.position.y, nextChallenge.transform.position.z), Quaternion.identity);
        return gameObject;
    }

    /*
	// Use this for initialization
	void Start () {
        currentChallenge = challenges[0];
        nextChallenge = challenges[1];
	}
	
	// Update is called once per frame
	void Update () {
        currentChallenge.transform.position -= Vector3.right * (scrollSpeed * Time.deltaTime);
        nextChallenge.transform.position -= Vector3.right * (scrollSpeed * Time.deltaTime);

        if(thirdChallenge != null)
        {
            
        }
        

        if (currentChallenge.transform.position.x < 20)
        {
            DestroyImmediate(currentChallenge);
            currentChallenge = nextChallenge;
            nextChallenge = thirdChallenge;
            thirdChallenge = Spawn();
        }
	}

    GameObject Spawn()
    {
        int i = r.Next(0, challenges.Count);
        var pos = currentChallenge.transform.position;
        GameObject newChallenge = Instantiate(challenges[i], new Vector3(pos.x + 40f, pos.y, pos.z), Quaternion.identity);

        return newChallenge;
    }*/
}
