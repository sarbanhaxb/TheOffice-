using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "UpdateStats", story: "Update [stress] [starve] [thirst] values for [NPCStats]", category: "Action", id: "3f7fb3b52b4608b3e5e841130e467622")]
public partial class UpdateStatsAction : Action
{
    [SerializeReference] public BlackboardVariable<float> Stress;
    [SerializeReference] public BlackboardVariable<float> Starve;
    [SerializeReference] public BlackboardVariable<float> Thirst;
    [SerializeReference] public BlackboardVariable<NPCStats> NPCStats;

    //protected override Status OnStart()
    //{
    //    return Status.Running;
    //}

    protected override Status OnUpdate()
    {
        Stress.Value = NPCStats.Value.GetCurrentStress();
        Starve.Value = NPCStats.Value.GetCurrentStarve();
        Thirst.Value = NPCStats.Value.GetCurrentThirst();
        return Status.Running;
    }
}

