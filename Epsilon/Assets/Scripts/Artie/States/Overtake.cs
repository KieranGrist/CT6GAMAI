using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overtake : State<AIAgent>
{
    private Node NextTarget(AIAgent agent)
    {
        var Num = agent.vehicle.LastCheckPoint + 1;
        RaycastHit item;
        if (Num == 2)
            if (Physics.Raycast(RaceTrack.raceTrack.Sector1.transform.position, -agent.transform.up, out item, float.PositiveInfinity, agent.mask))
                return item.transform.GetComponent<Node>();
        if (Num == 3)
            if (Physics.Raycast(RaceTrack.raceTrack.Sector2.transform.position, -agent.transform.up, out item, float.PositiveInfinity, agent.mask))
                return item.transform.GetComponent<Node>();
        if (Num == 4) //Comming from pit lane
            if (Physics.Raycast(RaceTrack.raceTrack.Sector1.transform.position, -agent.transform.up, out item, float.PositiveInfinity, agent.mask))
                return item.transform.GetComponent<Node>();


        if (Physics.Raycast(RaceTrack.raceTrack.FinishLine.transform.position, -agent.transform.up, out item, float.PositiveInfinity, agent.mask))
            return item.transform.GetComponent<Node>();



        Debug.LogError("Failed to find any nodes!");
        //THIS SHOULD NEVER RUN!!!!!!
        return item.transform.GetComponent<Node>();

    }

    public override void Execute(AIAgent agent)
    {
        if (!agent.RecievedPath)
            agent.targetNode = NextTarget(agent);
        var Reference = agent.steeringBehaviour.ProjectedCube.CheckForAI(agent.vehicle);
        if (Reference)
        {
            agent.steeringBehaviour.OvertakeOn();
            agent.vehicle.BoostOn();
        }
        agent.MoveOnRoute();
        agent.vehicle.Accelerate(agent.steeringBehaviour.Calculate());

    }
}


