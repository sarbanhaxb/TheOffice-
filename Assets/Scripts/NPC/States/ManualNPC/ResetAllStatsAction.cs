using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ResetAllStats", story: "Reset all agent [stats]", category: "Action", id: "a52b2a795b7aab96fa9245b4c1c6eed4")]
public partial class ResetAllStatsAction : Action
{
    [SerializeReference] public BlackboardVariable<NPC_Stats> Stats;

    protected override Status OnStart()
    {
        Stats.Value.ResetAllStats();
        return Status.Success;
    }
}

