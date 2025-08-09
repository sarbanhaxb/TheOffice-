using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Reset animator", story: "Reset animator: [Agent]", category: "Action", id: "e76d4013b94b8eb77c37fecf2aa290e3")]
public partial class ResetAnimatorAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;

    protected override Status OnStart()
    {
        Animator agentAnim = Agent.Value.GetComponent<Animator>();
        agentAnim.Rebind();
        return Status.Success;
    }
}

