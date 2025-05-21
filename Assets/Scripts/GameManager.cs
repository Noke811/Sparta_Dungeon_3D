using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;

    [SerializeField] PlayerInfo playerInfo;
    public PlayerInfo PlayerInfo => playerInfo;
    [SerializeField] UIManager uiManager;
    public UIManager UIManager => uiManager;

    public string PlayerTag => "Player";

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
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
        // test용
        SceneManager.LoadScene(0);
    }
}
