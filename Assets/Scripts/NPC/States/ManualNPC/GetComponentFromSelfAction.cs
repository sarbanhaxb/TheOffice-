using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Get component from self", story: "Get component [component] from [self]", category: "Action", id: "ed3001765b5f544f04e1fa7f45029e79")]
public partial class GetComponentFromSelfAction : Action
{
    [SerializeReference] public BlackboardVariable<NPC_CurrentState> Component;
    [SerializeReference] public BlackboardVariable<GameObject> Self;

    protected override Status OnStart()
    {
        Component.Value = Self.Value.GetComponent<NPC_CurrentState>();
        return Status.Success;
    }
}

