using System;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Try reserved chair", story: "[NPC] try reserved [chair] from [chairs]", category: "Action", id: "16fa8e2d5a2b0f79407fd3071ad4709c")]
public partial class TryReservedChairAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> NPC;
    [SerializeReference] public BlackboardVariable<GameObject> Chair;
    [SerializeReference] public BlackboardVariable<List<GameObject>> Chairs;
    protected override Status OnStart()
    {
        foreach (var c in Chairs.Value)
        {
            if (c.GetComponent<IReservable>().Reserve(NPC.Value))
            {
                Chair.Value = c;
                return Status.Success;
            }
            continue;
        }
        return Status.Failure;
    }
}

