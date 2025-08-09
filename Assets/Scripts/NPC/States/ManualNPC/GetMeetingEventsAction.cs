using System;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.EventSystems;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "GetMeetingEvents", story: "Get [EventIn] and [EventOut]", category: "Action", id: "b9229d3c488f4e5524c8258513f1fc4b")]
public partial class GetMeetingEventsAction : Action
{
    [SerializeReference] public BlackboardVariable<PlayerInMeetingRoomEvent> EventIn;
    [SerializeReference] public BlackboardVariable<PlayerExitMeetingRoomEvent> EventOut;

    protected override Status OnStart()
    {
        EventIn.Value = GameObject.FindGameObjectWithTag("MeetingRoom").GetComponent<RoomScript>().playerInMeetingRoom;
        EventOut.Value = GameObject.FindGameObjectWithTag("MeetingRoom").GetComponent<RoomScript>().playerExitMeetingRoom;
        bool result = EventIn.Value != null && EventOut.Value != null;
        return result ? Status.Success : Status.Failure;
    }
}

