using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "TurnOnOff collider", story: "Turn [OffOn] [ObjectCollider]", category: "Action", id: "94d85552dbbe0908bb5f22054d101ca4")]
public partial class TurnOnOffColliderAction : Action
{
    [SerializeReference] public BlackboardVariable<bool> OffOn;
    [SerializeReference] public BlackboardVariable<GameObject> ObjectCollider;

    protected override Status OnStart()
    {
        ObjectCollider.Value.GetComponent<BoxCollider2D>().enabled = OffOn.Value;
        return Status.Success;
    }
}

