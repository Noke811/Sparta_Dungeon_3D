using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;

    [SerializeField] PlayerInfo playerInfo;
    public PlayerInfo PlayerInfo => playerInfo;
    [SerializeField] UIManager uiManager;
    public UIManager UIManager => uiManager;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if(instance != this) Destroy(gameObject);
        }
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
    }
}
