using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class stage1ch : MonoBehaviour
{



    private void OnCollisionEnter2D(Collision2D collision)
    {
        SceneManager.LoadScene("stage1", LoadSceneMode.Single);
    }
    
}
