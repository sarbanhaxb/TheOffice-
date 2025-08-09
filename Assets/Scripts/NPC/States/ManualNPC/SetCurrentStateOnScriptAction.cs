using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Set current state on script", story: "Set [currentstate] on [script]", category: "Action", id: "4d9e7212e5d57462a0f8fc6b7018189c")]
public partial class SetCurrentStateOnScriptAction : Action
{
    [SerializeReference] public BlackboardVariable<NPCStates> Currentstate;
    [SerializeReference] public BlackboardVariable<NPC_CurrentState> Script;
    protected override Status OnStart()
    {
        Script.Value.SetState(Currentstate.Value);
        return Status.Success;
    }
}

