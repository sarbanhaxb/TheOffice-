using UnityEngine;

public class RoomScript : MonoBehaviour
{
    [SerializeField] private OnPlayerInMeetingRoom playerInMeetingRoom;
    public RoomScript Instance { get; private set; }
    public bool isPlayerIn = false;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        playerInMeetingRoom.Event += OnTriggerEnter2D;
    }

    private void OnDisable()
    {
        playerInMeetingRoom.Event -= OnTriggerEnter2D;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInMeetingRoom?.Invoke(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isPlayerIn = false;
        }
    }

    private void Update()
    {
    }

    public bool IsPlayerIn() => isPlayerIn;
}
