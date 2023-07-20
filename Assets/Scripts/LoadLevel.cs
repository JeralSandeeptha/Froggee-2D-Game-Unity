using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    private bool levelCompleted = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !levelCompleted){
            levelCompleted = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Invoke("CompleteLevel", 2f);
        }
    }
}
