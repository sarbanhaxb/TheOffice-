using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/OnPlayerInMeetingRoom")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "OnPlayerInMeetingRoom", message: "[Player] enter MeetingRoom", category: "Events", id: "6bd3733bc5397416960cbf736320fe77")]
public sealed partial class OnPlayerInMeetingRoom : EventChannel<Collider2D> { }

