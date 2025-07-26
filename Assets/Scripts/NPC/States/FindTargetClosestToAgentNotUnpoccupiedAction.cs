using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Find target closest to agent not unpoccupied", story: "Find [target] closest to [agent] with tag [tag] not occupied", category: "Action", id: "7a84c02c0a4b045f3188a349a7ee62d4")]
public partial class FindTargetClosestToAgentNotUnpoccupiedAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<string> Tag;
    protected override Status OnStart()
    {
        if (Agent.Value == null)
        {
            LogFailure("No agent provided.");
            return Status.Failure;
        }

        Vector3 agentPosition = Agent.Value.transform.position;

        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(Tag.Value);
        float closestDistanceSq = Mathf.Infinity;
        GameObject closestGameObject = null;
        foreach (GameObject gameObject in gameObjects)
        {
            float distanceSq = Vector3.SqrMagnitude(agentPosition - gameObject.transform.position);
            if ((closestGameObject == null || distanceSq < closestDistanceSq) && !gameObject.GetComponent<ChairInteractable>().IsOccupied)
            {
                closestDistanceSq = distanceSq;
                closestGameObject = gameObject;
            }
        }

        Target.Value = closestGameObject;

        return Target.Value == null ? Status.Failure : Status.Success;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}