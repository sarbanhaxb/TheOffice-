using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Switch object", story: "[Switch] [object]", category: "Action", id: "62b6facb1a8e9356ac29d592b286878c")]
public partial class SwitchObjectAction : Action
{
    [SerializeReference] public BlackboardVariable<bool> Switch;
    [SerializeReference] public BlackboardVariable<GameObject> Object;

    protected override Status OnStart()
    {
        Object.Value.GetComponent<SpriteRenderer>().enabled = Switch.Value;
        return Status.Success;
    }
}

