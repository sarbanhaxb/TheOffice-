using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "CheckHigherCondition", story: "[CurrentValue] higher than [value]", category: "Conditions", id: "8a69b9a1400a31a46e0d0b5701153c59")]
public partial class CheckHigherCondition : Condition
{
    [SerializeReference] public BlackboardVariable<float> CurrentValue;
    [SerializeReference] public BlackboardVariable<float> Value;

    public override bool IsTrue()
    {
        return CurrentValue > Value;
    }
}
