using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttractable
{
    void Attract(Vector2 direction, float power);
    void Lock(Transform parent, float orbitRadius, float orbitSpeed);
    void Unlock();
}