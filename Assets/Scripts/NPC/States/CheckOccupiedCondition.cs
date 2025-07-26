using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "Check Occupied", story: "Check [chair] occupied", category: "Conditions", id: "c43fc752b62a04464a987927bf8b437f")]
public partial class CheckOccupiedCondition : Condition
{
    [SerializeReference] public BlackboardVariable<GameObject> Chair;

    public override bool IsTrue()
    {
        return Chair.Value.GetComponent<ChairInteractable>().IsOccupied;
    }
}
