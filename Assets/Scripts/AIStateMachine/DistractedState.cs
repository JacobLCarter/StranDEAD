﻿using UnityEngine;
using GeneralStateMachine;

public class DistractedState : State<Enemy>
{
    private static DistractedState _instance;

    private DistractedState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static DistractedState Instance
    {
        get
        {
            if (_instance == null)
            {
                new DistractedState();
            }

            return _instance;
        }
    }
    
    public override void EnterState(Enemy enemy)
    {
        enemy.navmesh.speed = 1f;
        Debug.Log(enemy.getCurrentRock().name);
        enemy.navmesh.destination = enemy.getCurrentRock().transform.position;
    }

    public override void ExitState(Enemy enemy)
    {

    }

    public override void UpdateState(Enemy enemy)
    {
        
        if (enemy.isPlayerInSight())
        {
            enemy.stateMachine.switchState(ChaseState.Instance);
        }
        else if (enemy.navmesh.remainingDistance < 0.2f)
        {
            enemy.stateMachine.switchState(PathState.Instance);
        }
    }
}