using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Vacate the chair", story: "[Self] vacate the [chair]", category: "Action", id: "7600c6ac5221ed19c363f19d8752686e")]
public partial class VacateTheChairAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Chair;

    protected override Status OnStart()
    {
        Chair.Value.GetComponent<ChairInteractable>().Releaser();
        return Status.Success;
    }
}

