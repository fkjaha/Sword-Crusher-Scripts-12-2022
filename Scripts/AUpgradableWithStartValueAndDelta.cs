using UnityEngine;

public abstract class AUpgradableWithStartValueAndDelta<T>: AUpgradable
{
    [SerializeField] private protected T startValue;
    [SerializeField] private protected float delta;
}
