using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Flip reverse", story: "Set [SpriteRenderer] [reverse]", category: "Action", id: "2d9db9e249e654f53d5aa1fdc6f72574")]
public partial class FlipReverseAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> SpriteRenderer;
    [SerializeReference] public BlackboardVariable<bool> Reverse;

    protected override Status OnStart()
    {
        SpriteRenderer.Value.GetComponent<SpriteRenderer>().flipX = Reverse;
        return Status.Success;
    }
}

