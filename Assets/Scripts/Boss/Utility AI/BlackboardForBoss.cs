using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackboardForBoss : MonoBehaviour
{

    public static BossPurpose purpose;
    public static BossMode mode;
    public static BossState state;



}
public enum BossPurpose
{

    Idle,
    Heal,
    Attack,
    SpecialAttack


}
public enum BossMode
{
    Idle,
    Heal,
    Flee,
    Chase,
    Attack,
    SpecialAttack


}



public enum BossState
{
    Idle,
    Running,
    Jump,
    Airborne,
    ShadowStep,
    Attack,
    SpecialAttack



}