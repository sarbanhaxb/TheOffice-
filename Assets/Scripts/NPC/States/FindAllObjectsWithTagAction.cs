using System;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using System.Linq;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Find All Objects with tag", story: "Find all [Objects] with [tag]", category: "Action", id: "bffe33a064d72475af7add80453f8db3")]
public partial class FindAllObjectsWithTagAction : Action
{
    [SerializeReference] public BlackboardVariable<List<GameObject>> Objects;
    [SerializeReference] public BlackboardVariable<string> Tag;
    protected override Status OnStart()
    {
        Objects.Value = GameObject.FindGameObjectsWithTag(Tag.Value).ToList();
        return Status.Success;
    }
}

