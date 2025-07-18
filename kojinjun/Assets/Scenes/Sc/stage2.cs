using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class stage2: MonoBehaviour
{



    private void OnCollisionEnter2D(Collision2D collision)
    {
        SceneManager.LoadScene("stage2", LoadSceneMode.Single);
    }
}
