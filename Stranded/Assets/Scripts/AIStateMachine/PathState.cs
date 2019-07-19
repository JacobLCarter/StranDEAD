﻿using UnityEngine;
using GeneralStateMachine;

public class PathState : State<Enemy>
{
    private static PathState _instance;

    private PathState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static PathState Instance
    {
        get
        {
            if (_instance == null)
            {
                new PathState();
            }

            return _instance;
        }
    }
    
    public override void EnterState(Enemy enemy)
    {
        enemy.navmesh.speed = .75f;
    }

    public override void ExitState(Enemy enemy)
    {
        
    }

    public override void UpdateState(Enemy enemy)
    {
        enemy.isPlayerInSight();

        if (!enemy.navmesh.pathPending && enemy.navmesh.remainingDistance < 0.2f)
        {
            WalkPath(enemy);
        }
    }

    private void WalkPath(Enemy enemy)
    {
        enemy.navmesh.speed = .75f;

        if (enemy.stops.Length == 0)
        {
            return;
        }

        enemy.navmesh.destination = enemy.stops[enemy.stop].position;

        //stop = (stop + 1) % stops.Length;
        enemy.stop = Random.Range(0, enemy.stops.Length - 1);
    }
}
