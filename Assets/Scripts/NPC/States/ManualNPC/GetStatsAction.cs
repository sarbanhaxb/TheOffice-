using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "GetStats", story: "Get stats [NPC_Stats] from [Agent]", category: "Action", id: "6da325c7e5bfa8f452b6b7ce77a3c205")]
public partial class GetStatsAction : Action
{
    [SerializeReference] public BlackboardVariable<NPC_Stats> NPC_Stats;
    [SerializeReference] public BlackboardVariable<GameObject> Agent;

    protected override Status OnStart()
    {
        NPC_Stats.Value = Agent.Value.GetComponent<NPC_Stats>();
        return NPC_Stats.Value != null ? Status.Success : Status.Failure;
    }
}

