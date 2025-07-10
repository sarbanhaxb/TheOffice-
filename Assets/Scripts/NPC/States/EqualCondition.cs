using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "EqualCondition", story: "[CurrentValue] equal [Value]", category: "Conditions", id: "60b7bb053790578fe208d0fcba496e80")]
public partial class EqualCondition : Condition
{
    [SerializeReference] public BlackboardVariable<float> CurrentValue;
    [SerializeReference] public BlackboardVariable<float> Value;

    public override bool IsTrue()
    {
        return CurrentValue == Value;
    }
}
