using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Set Occupied value", story: "Set [Occupied] [value]", category: "Action", id: "8fc7e65822fdab6b610cc8f91c17d38e")]
public partial class SetOccupiedValueAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Occupied;
    [SerializeReference] public BlackboardVariable<bool> Value;

    protected override Status OnStart()
    {
        Occupied.Value.GetComponent<ChairInteractable>().SetOccupied(Value.Value);
        return Status.Success;
    }
}

