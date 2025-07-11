using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "GetAllNeedComponents", story: "Take [NPCVisualScript] [NPCCurrentStateScript] [NPSStats] from [Self]", category: "Action", id: "15388802e9f549a9eefc5de83e9d8333")]
public partial class GetAllNeedComponentsAction : Action
{
    [SerializeReference] public BlackboardVariable<NPC_VisualScript> NPCVisualScript;
    [SerializeReference] public BlackboardVariable<NPC_CurrentState> NPCCurrentStateScript;
    [SerializeReference] public BlackboardVariable<NPCStats> NPSStats;
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    protected override Status OnStart()
    {
        NPCVisualScript.Value = Self.Value.GetComponent<NPC_VisualScript>();
        NPCCurrentStateScript.Value = Self.Value.GetComponent<NPC_CurrentState>();
        NPSStats.Value = Self.Value.GetComponent<NPCStats>();
        return Status.Success;
    }
}

