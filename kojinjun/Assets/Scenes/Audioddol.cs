using UnityEngine;
using UnityEngine.SceneManagement;

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance;

    public string taskInstruction;
    public string taskInstruction2;
    public string taskInstruction3;

    public float taskResult;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded; // シーンが変わるたびにチェック
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Title"||scene.name=="gameover") // ←←← 消したいシーン名
        {
            SceneManager.sceneLoaded -= OnSceneLoaded; // リスナー解除
            Destroy(gameObject);
        }
    }
    void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}
