using UnityEditor;
using UnityEngine;


public class RoomScript : MonoBehaviour
{
    public PlayerInMeetingRoomEvent playerInMeetingRoom;
    public PlayerExitMeetingRoomEvent playerExitMeetingRoom;
    public bool isPlayerIn = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && GameTime.Instance.GetCurrentDayPart()==DayPart.morning)
        {
            playerInMeetingRoom.SendEventMessage(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && GameTime.Instance.GetCurrentDayPart() == DayPart.morning)
        {
            playerExitMeetingRoom.SendEventMessage(collision);
        }
    }

    public bool IsPlayerIn() => isPlayerIn;
}
