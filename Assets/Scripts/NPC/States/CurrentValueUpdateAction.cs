using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Current Value update", story: "[CurrentValue] update if [CurrentState]", category: "Action", id: "803032ed6a259a2d49ade605e3bc0c1d")]
public partial class CurrentValueUpdateAction : Action
{
    [SerializeReference] public BlackboardVariable<float> CurrentValue;
    [SerializeReference] public BlackboardVariable<NPCStates> CurrentState;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

