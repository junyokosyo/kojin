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
            SceneManager.sceneLoaded += OnSceneLoaded; // �V�[�����ς�邽�тɃ`�F�b�N
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Title"||scene.name=="gameover") // ������ ���������V�[����
        {
            SceneManager.sceneLoaded -= OnSceneLoaded; // ���X�i�[����
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
