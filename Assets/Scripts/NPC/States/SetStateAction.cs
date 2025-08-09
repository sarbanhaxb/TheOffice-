using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Set state", story: "Set [currentState] on [newState]", category: "Action", id: "07d31016102ec5edddfed73361e74b87")]
public partial class SetStateAction : Action
{
    [SerializeReference] public BlackboardVariable<NPCStates> CurrentState;
    [SerializeReference] public BlackboardVariable<NPCStates> NewState;

    protected override Status OnStart()
    {
        CurrentState.Value = NewState.Value;
        return Status.Success;
    }
}

