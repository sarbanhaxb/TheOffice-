using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using System.Linq;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Find closest GameObject except self", story: "Find [Gameobject] closest to [Agent] with tag: [string]", category: "Action", id: "8afc71eef1f45fe793abf64d99a9300d")]
public partial class FindClosestGameObjectExceptSelfAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Gameobject;
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<string> String;

    protected override Status OnStart()
    {
        Gameobject.Value = GameObject.FindGameObjectsWithTag(String.Value).Where(npc => npc != Agent.Value).OrderBy(npc => Vector3.Distance(Agent.Value.transform.position, npc.transform.position)).FirstOrDefault();
        return Gameobject.Value != null ? Status.Success : Status.Failure;
    }
}

