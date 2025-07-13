using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class BusController : MonoBehaviour
{
    NavMeshAgent agent;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

    }
}