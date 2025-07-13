using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Set Bool", story: "Set [Bool] [value]", category: "Action", id: "f18c8d0490a3482a7ae1e3f5bb397546")]
public partial class SetBoolAction : Action
{
    [SerializeReference] public BlackboardVariable<bool> Bool;
    [SerializeReference] public BlackboardVariable<bool> Value;

    protected override Status OnStart()
    {
        Bool.Value = Value.Value;
        return Status.Success;
    }
}

