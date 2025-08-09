using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Change Agent CurrentState", story: "Change [Agent] state on [state]", category: "Action", id: "957445cfb87c654c6db2422420e62b20")]
public partial class ChangeAgentCurrentStateAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<NPCStates> State;

    protected override Status OnStart()
    {
        Agent.Value.GetComponent<NPC_CurrentState>().SetState(State.Value);
        return Status.Success;
    }
}

