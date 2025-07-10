using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "RandomForAnimation", story: "Get randoming [value] [within]", category: "Action", id: "0bbea55353521b80826033e9c7fc5480")]
public partial class RandomForAnimationAction : Action
{
    [SerializeReference] public BlackboardVariable<int> Value;
    [SerializeReference] public BlackboardVariable<int> Within;
    protected override Status OnStart()
    {
        Value.Value = UnityEngine.Random.Range(0, Within);
        return Status.Success;
    }
}

