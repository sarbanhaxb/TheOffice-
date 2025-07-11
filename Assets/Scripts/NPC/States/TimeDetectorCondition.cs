using System;
using System.Globalization;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "TimeDetector", story: "Is [RealTIme] higher than [value]", category: "Conditions", id: "f9ed115c7a2e6136fff7db2c840da09c")]
public partial class TimeDetectorCondition : Condition
{
    [SerializeReference] public BlackboardVariable<string> RealTIme;
    [SerializeReference] public BlackboardVariable<string> Value;

    public override bool IsTrue()
    {
        DateTime RealTime;
        DateTime v;
        DateTime.TryParseExact(RealTIme.Value, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out RealTime);
        DateTime.TryParseExact(Value.Value, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out v);

        return RealTime > v;
    }
}
