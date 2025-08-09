using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "Check NPC SpriteRenderer on off", story: "Check [SpriteRenderer] OnOff", category: "Conditions", id: "501ea45be8bd2607977a3be130fffe31")]
public partial class CheckNpcSpriteRendererOnOffCondition : Condition
{
    [SerializeReference] public BlackboardVariable<GameObject> SpriteRenderer;

    public override bool IsTrue()
    {
        return SpriteRenderer.Value.GetComponent<SpriteRenderer>().enabled;
    }
}
