using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "UpdateRealTime", story: "Get [RealTime]", category: "Action", id: "02eccf7d22df632fe2b7e1cbf9731008")]
public partial class UpdateRealTimeAction : Action
{
    [SerializeReference] public BlackboardVariable<DayPart> RealTime;
    protected override Status OnStart()
    {
        RealTime.Value = GameTime.Instance.GetCurrentDayPart();
        return Status.Success;
    }
}

