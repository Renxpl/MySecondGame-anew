using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttackStep
{
    public string animation;
    public float delayBeforeHit;
    public float damage;
    public float range;
    public float postDelay;
    public Collider2D hitbox;

}
