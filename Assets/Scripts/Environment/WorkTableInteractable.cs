using NavMeshPlus.Components;
using TMPro;
using UnityEngine;

public class WorkTableInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private float interactionPriority = 10f;
    [SerializeField] private Transform Player;
    [SerializeField] private GameObject workTableHint;
    [SerializeField] private NavMeshSurface surface;


    [SerializeField] private bool isTaken = false;

    private Vector3 _originalPosition;
    private Transform _originalParent;
    [SerializeField] private float dropDistance = 5f;


    private void Awake()
    {
        Player = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    public void Interact()
    {
        if (isTaken)
        {
            Drop();
            workTableHint.GetComponent<TMP_Text>().text = "Press E to take the table";
        }
        else
        {
            PickUp();
            workTableHint.GetComponent<TMP_Text>().text = "Press E to drop the table";
        }
    }


    private void PickUp()
    {
        isTaken = true;
        _originalPosition = transform.position;
        _originalParent = transform.parent;
    }

    private void Update()
    {

        if (isTaken && Player != null)
        {
            transform.position = Vector3.Lerp(
                transform.position,
                Player.position + new Vector3(0, dropDistance, 0),
                Time.deltaTime * 10f
            );
        }
    }

    private void Drop()
    {
        isTaken = false;
        transform.SetParent(_originalParent);
        surface.BuildNavMesh();
    }

    public void ShowHint()
    {
        workTableHint.SetActive(true);
    }
    public void HideHint()
    {
        workTableHint.SetActive(false);
    }

    public float GetPriority() => interactionPriority;
    public bool IsPickedUp() => isTaken;

}
