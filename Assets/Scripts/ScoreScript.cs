using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreScript : MonoBehaviour
{

    public static int scoreValue = 0;
    public int winValue;
    public GameObject player;
    public GameObject winUI;
    private bool winUIisOn;
    Text score;

    // Start is called before the first frame update
    void Start()
    {
        score = GetComponent<Text>();
        winUI.SetActive(false);
        winUIisOn = false;
        
    }

    IEnumerator Win()
    {
        yield return new WaitForSeconds(1);
        winUI.SetActive(true);
        winUIisOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "Score: " + scoreValue;
        if (Input.GetKey("return"))
        {
            if (winUIisOn == true)
            {
                scoreValue = 0;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        if (scoreValue >= winValue)
        {
            player.SetActive(false);
            StartCoroutine(Win());
        }
    }
}
