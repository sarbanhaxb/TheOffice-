using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Change current state", story: "Change [currentState] on [newState] and change script [CodeState] and reset animation [VisualReset]", category: "Action", id: "674419358df8eba576c0467564b7ec61")]
public partial class ChangeCurrentStateAction : Action
{
    [SerializeReference] public BlackboardVariable<NPCStates> CurrentState;
    [SerializeReference] public BlackboardVariable<NPCStates> NewState;
    [SerializeReference] public BlackboardVariable<NPC_CurrentState> CodeState;
    [SerializeReference] public BlackboardVariable<NPC_VisualScript> VisualReset;
    protected override Status OnStart()
    {
        CurrentState.Value = NewState.Value;
        CodeState.Value.SetState(NewState.Value);
        VisualReset.Value.ResetAnimator();
        return Status.Success;
    }
}

