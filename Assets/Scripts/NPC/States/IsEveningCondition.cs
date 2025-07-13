using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "Is Evening", story: "Is [Evening]", category: "Conditions", id: "424d91cacdae7ce2bafb57d6e19ddb70")]
public partial class IsEveningCondition : Condition
{
    [SerializeReference] public BlackboardVariable<bool> Evening;
    public override bool IsTrue()
    {
        return Evening.Value;
    }
}
