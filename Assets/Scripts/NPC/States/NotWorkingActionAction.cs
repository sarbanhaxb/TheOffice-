using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "NotWorking action", story: "If [currentState] not [NPCStates] [Worktable] off", category: "Action", id: "ec02e769c48f7ad4fd5a808919bfb607")]
public partial class NotWorkingAction : Action
{
    [SerializeReference] public BlackboardVariable<NPCStates> CurrentState;
    [SerializeReference] public BlackboardVariable<NPCStates> NPCStates;
    [SerializeReference] public BlackboardVariable<Animator> Worktable;
    protected override Status OnStart()
    {
        if (!CurrentState.Value.Equals(NPCStates.Value) && Worktable.Value.GetBool("IsWorking"))
        {
            Worktable.Value.SetBool("IsWorking", false);
            Worktable.Value.enabled = false;
            Worktable.Value.enabled = true;
        }
        return Status.Success;
    }
}

