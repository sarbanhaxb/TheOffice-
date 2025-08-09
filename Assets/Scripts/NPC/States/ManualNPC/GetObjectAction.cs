using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Get object", story: "Get [object] with tag: [tag]", category: "Action", id: "117654ad4320fd50b46c4b9d85868cbc")]
public partial class GetObjectAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Object;
    [SerializeReference] public BlackboardVariable<string> Tag;

    protected override Status OnStart()
    {
        Object.Value = GameObject.FindGameObjectWithTag(Tag.Value);
        return Object.Value != null ? Status.Success : Status.Failure;
    }
}

