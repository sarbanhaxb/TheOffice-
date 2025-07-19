using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using TMPro;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "GetAllNeedComponents", story: "Take [NPCVisualScript] [NPCCurrentStateScript] [NPSStats] [Helper] from [Self] and [Boss]", category: "Action", id: "15388802e9f549a9eefc5de83e9d8333")]
public partial class GetAllNeedComponentsAction : Action
{
    [SerializeReference] public BlackboardVariable<NPC_VisualScript> NPCVisualScript;
    [SerializeReference] public BlackboardVariable<NPC_CurrentState> NPCCurrentStateScript;
    [SerializeReference] public BlackboardVariable<NPCStats> NPSStats;
    [SerializeReference] public BlackboardVariable<TextMeshPro> Helper;
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Boss;
    protected override Status OnStart()
    {
        NPCVisualScript.Value = Self.Value.GetComponent<NPC_VisualScript>();
        NPCCurrentStateScript.Value = Self.Value.GetComponent<NPC_CurrentState>();
        NPSStats.Value = Self.Value.GetComponent<NPCStats>();
        Boss.Value = GameObject.FindGameObjectWithTag("Player");
        Helper.Value = Self.Value.GetComponentInChildren<TextMeshPro>();
        return Status.Success;
    }
}

