using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Set animator state", story: "Set [parametr] in [gObject] to [bool]", category: "Action", id: "747e7b5a9baaaaaab72d3e025b3b214b")]
public partial class SetAnimatorStateAction : Action
{
    [SerializeReference] public BlackboardVariable<string> Parametr;
    [SerializeReference] public BlackboardVariable<GameObject> gObject;
    [SerializeReference] public BlackboardVariable<bool> Bool;

    protected override Status OnStart()
    {
        gObject.Value.GetComponent<Animator>().SetBool(Parametr.Value, Bool.Value);
        gObject.Value.GetComponent<SpriteRenderer>().flipX = false;
        return Status.Success;
    }
}

