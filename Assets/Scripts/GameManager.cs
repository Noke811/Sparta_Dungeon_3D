using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;

    [SerializeField] GameObject player;
    public GameObject Player => player;
    public PlayerInfo PlayerInfo => player.GetComponent<PlayerInfo>();
    public PlayerController PlayerController => player.GetComponent<PlayerController>();
    [SerializeField] Inventory inventory;
    public Inventory Inventory => inventory;
    [SerializeField] UIManager uiManager;
    public UIManager UIManager => uiManager;

    public const int PLAYER_LAYER = 10;

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
