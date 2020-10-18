using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathPlane : MonoBehaviour {

    public GameObject player;
    public GameObject deathUI;
    private bool deathUIisOn;

    // Start is called before the first frame update
    public void Start () {
        deathUI.SetActive (false);
        deathUIisOn = false;
    }

    // Update is called once per frame
    public void Update () {
        if (Input.GetKey ("return")) {
            if (deathUIisOn == true) {
                SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
            }
        }
    }

    private void OnTriggerEnter2D (Collider2D other) {
        if (other.CompareTag ("Player")) {
            other.gameObject.SetActive (false);
            StartCoroutine (Death ());
        }
    }

    IEnumerator Death () {
        yield return new WaitForSeconds (2);
        deathUI.SetActive (true);
        deathUIisOn = true;
    }

}