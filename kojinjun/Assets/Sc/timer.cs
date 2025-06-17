using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class timer : MonoBehaviour
{


     float time = 0f;
    [SerializeField] float gameover = 15f; //gameover‚É‚È‚éŽžŠÔ
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > gameover)
        {
            SceneManager.LoadScene("gameover" ,LoadSceneMode.Single);
        }
    }
}
