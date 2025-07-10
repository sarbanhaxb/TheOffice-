using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "CheckLowerCondition", story: "[CurrentValue] lower than [value]", category: "Conditions", id: "45c675b191160138c1aa9c753594a3f8")]
public partial class CheckCondition : Condition
{
    [SerializeReference] public BlackboardVariable<float> CurrentValue;
    [SerializeReference] public BlackboardVariable<float> Value;

    public override bool IsTrue()
    {
        return CurrentValue < Value;
    }
}
