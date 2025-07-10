using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "Is new state equal current state", story: "Is [currentState] is equal [newState]", category: "Conditions", id: "2f21c96a1b203bb77b33711ac64a1f3d")]
public partial class IsNewStateEqualCurrentStateCondition : Condition
{
    [SerializeReference] public BlackboardVariable<NPCStates> CurrentState;
    [SerializeReference] public BlackboardVariable<NPCStates> NewState;

    public override bool IsTrue()
    {
        return CurrentState.Value.Equals(NewState.Value);
    }
}
