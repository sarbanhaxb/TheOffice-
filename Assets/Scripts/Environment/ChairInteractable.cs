using TMPro;
using UnityEngine;

public class ChairInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private float interactionPriority = 1f;
    [SerializeField] private Transform Player;
    [SerializeField] private GameObject chairHint;

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
            chairHint.GetComponent<TMP_Text>().text = "Press E to take the chair";
        }
        else
        {
            PickUp();
            chairHint.GetComponent<TMP_Text>().text = "Press E to drop the chair";

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
    }

    public void ShowHint()
    {
        chairHint.SetActive(true);
    }
    public void HideHint()
    {
        chairHint.SetActive(false);
    }

    public float GetPriority() => interactionPriority;
    public bool IsPickedUp() => isTaken;


}
