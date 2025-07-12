using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Check current event by time", story: "Check [current] by [time]", category: "Action", id: "c340077a9e26e416f837eebb4fea4cb3")]
public partial class CheckCurrentEventByTimeAction : Action
{
    [SerializeReference] public BlackboardVariable<DayPart> Current;
    [SerializeReference] public BlackboardVariable<string> Time;
    protected override Status OnStart()
    {
        TimeSpan currentTime = new (int.Parse(Time.Value.Split(':')[0]), int.Parse(Time.Value.Split(':')[1]), 0);
        TimeSpan morning = new(8, 0, 0);
        TimeSpan dinnerStart = new(12, 30, 0);
        TimeSpan dinnerEnd = new(13, 30, 0);
        TimeSpan evening = new(17, 0, 0);

        if (currentTime >= morning && currentTime < dinnerStart && currentTime > dinnerEnd && currentTime < evening)
        {
            Current.Value = DayPart.morning;
        }
        if (currentTime > dinnerStart && currentTime < dinnerEnd)
        {
            Current.Value = DayPart.dinner;
        }
        if (currentTime > evening && currentTime < morning)
        {
            Current.Value = DayPart.evening;
        }
        return Status.Success;
    }
}

