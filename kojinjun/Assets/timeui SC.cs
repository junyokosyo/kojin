using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class timeuiSC : MonoBehaviour
{
    [SerializeField] float startTime = 15f; //スタートの時間
    private float timeRemeining;
    public Text timerText;
    private bool isRunning = true;
    // Start is called before the first frame update
    void Start()
    {
        timeRemeining = startTime;
        UpdateTimerDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning && timeRemeining > 0f)
        {
            timeRemeining -= Time.deltaTime;
            if (timeRemeining <= 0f)
            {
                timeRemeining = 0f;
                isRunning = false;
                OnTimeUp();
            }
            UpdateTimerDisplay();
        }
    }
    void UpdateTimerDisplay()
    {
        int timeToshow = Mathf.CeilToInt(timeRemeining);
        timerText.text = timeToshow.ToString("000");
    }
    void OnTimeUp()
    {
        Debug.Log("おわり");
    }

}
