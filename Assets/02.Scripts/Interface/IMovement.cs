using UnityEngine;

public interface IMovement
{
    bool Move(Direction dir, int point);
    Direction KnockbackCheck(Direction dir);
}