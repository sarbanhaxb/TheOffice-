using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "GetRandom", story: "Get [randomValue] from [Animator] for [tableAnimator] if [currentState] is [NPCStates]", category: "Action", id: "699a8cb3783df01017b261a5a36d337f")]
public partial class GetRandomAction : Action
{
    [SerializeReference] public BlackboardVariable<float> RandomValue;
    [SerializeReference] public BlackboardVariable<Animator> Animator;
    [SerializeReference] public BlackboardVariable<Animator> TableAnimator;
    [SerializeReference] public BlackboardVariable<NPCStates> CurrentState;
    [SerializeReference] public BlackboardVariable<NPCStates> NPCStates;
    protected override Status OnStart()
    {
        if (CurrentState.Value.Equals(NPCStates.Value))
        {
            float valNPC = Animator.Value.GetFloat("Random");
            float valTable = TableAnimator.Value.GetFloat("Random");
            if (valNPC != valTable)
            {
                TableAnimator.Value.SetFloat("Random", valNPC);
            }
        }
        else
        {
            TableAnimator.Value.SetBool("IsWorking", false);
            TableAnimator.Value.enabled = false;
            TableAnimator.Value.enabled = true;
        }
        return Status.Success;
    }

}

