using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Get current state", story: "Get current state [currentstate] from [self]", category: "Action", id: "13ecd079c6ec3e277df6be0275029646")]
public partial class GetCurrentStateAction : Action
{
    [SerializeReference] public BlackboardVariable<NPCStates> Currentstate;
    [SerializeReference] public BlackboardVariable<NPC_CurrentState> Self;

    protected override Status OnStart()
    {
        Currentstate.Value = Self.Value.GetCurrentState();
        return Status.Success;
    }
}

