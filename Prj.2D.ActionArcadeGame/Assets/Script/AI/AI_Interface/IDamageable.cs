using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void PGotHit(float headPos);
    void EGotHit();
    void Dead();
}
