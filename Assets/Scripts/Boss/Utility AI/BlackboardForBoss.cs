using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackboardForBoss : MonoBehaviour
{

    public BossPurpose purpose;
    public BossMode mode;
    public BossState state;



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