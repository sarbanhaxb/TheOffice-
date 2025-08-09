using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/HandleStateChange")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "HandleStateChange", message: "Currentstate is changes", category: "Events", id: "b266ddf51a7fa8afa2805bf6bfa3bc7e")]
public sealed partial class HandleStateChange : EventChannel { }

