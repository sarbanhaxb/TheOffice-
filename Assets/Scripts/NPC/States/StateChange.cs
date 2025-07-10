using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/StateChange")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "StateChange", message: "[CurrentState] Is Changed", category: "Events", id: "90d6962f5e268a3a83d0652e66e330c9")]
public sealed partial class StateChange : EventChannel<NPCStates> { }

