using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacter
{
    void TakeDamage(int value);

    void IncreaseHealth(int value);
}
