using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using TMPro;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Says something", story: "[NPC] says [something]", category: "Action", id: "e81d6e6798ca0785941e5277b3e244df")]
public partial class SaysSomethingAction : Action
{
    [SerializeReference] public BlackboardVariable<TextMeshPro> NPC;
    [SerializeReference] public BlackboardVariable<string> Something;
    protected override Status OnStart()
    {
        NPC.Value.text = Something.Value.ToString();
        NPC.Value.enabled = true;
        return Status.Success;
    }
}

