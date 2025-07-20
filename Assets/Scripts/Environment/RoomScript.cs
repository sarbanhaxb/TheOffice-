using UnityEngine;

public class RoomScript : MonoBehaviour
{
    [SerializeField] private PlayerInMeetingRoomEvent playerInMeetingRoom;
    [SerializeField] private PlayerExitMeetingRoomEvent playerExitMeetingRoom;
    public bool isPlayerIn = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInMeetingRoom.SendEventMessage(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerExitMeetingRoom.SendEventMessage(collision);
        }
    }

    public bool IsPlayerIn() => isPlayerIn;
}
