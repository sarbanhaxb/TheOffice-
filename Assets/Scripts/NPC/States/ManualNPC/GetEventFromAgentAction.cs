using System;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.EventSystems;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Get Event from Agent", story: "Get [Event] from agent: [Agent]", category: "Action", id: "1526293bfb498ea620e34621983e7f4d")]
public partial class GetEventFromAgentAction : Action
{
    [SerializeReference] public BlackboardVariable<HandleStateChange> Event;
    [SerializeReference] public BlackboardVariable<GameObject> Agent;

    protected override Status OnStart()
    {
        Event.Value = Agent.Value.GetComponent<NPC_CurrentState>().handleStateChange;
        return Event.Value == null ? Status.Failure : Status.Success;
    }
}

