using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="Enemy Attack Combo")]
public class AttackCombo : ScriptableObject
{
    public AttackStep[] steps;
    public float[] durations;
    public float comboCooldown;



}
