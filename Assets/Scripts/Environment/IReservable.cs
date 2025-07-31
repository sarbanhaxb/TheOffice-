using UnityEngine;

public interface IReservable
{
    public bool IsReserved();
    public bool Reserve(GameObject gameObject);
    public void Releaser();
}
