using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/PlayerExitMeetingRoomEvent")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "PlayerExitMeetingRoomEvent", message: "[Player] Exit MeetingRoom", category: "Events", id: "6166b542ff01da07f56aa5b820829500")]
public sealed partial class PlayerExitMeetingRoomEvent : EventChannel<Collider2D> { }

