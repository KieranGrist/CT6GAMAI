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




        //THIS SHOULD NEVER RUN!!!!!!
        return item.transform.GetComponent<Node>();

    }
    void MoveOnRoute(AIAgent agent)
    {
        if (!agent.recievedPath)
        {
            if (Physics.Raycast(agent.transform.position, -agent.transform.up, out RaycastHit hit, 20.0f, agent.mask))
            {
                var Temp = hit.collider.gameObject.GetComponent<Node>();
                if (Temp)
                    agent.sourceNode = Temp;
            }
            agent.targetNode = NextTarget(agent);
            if (agent.sourceNode && agent.targetNode)
            {
                agent.targetLocation.Clear();
                agent.targetLocation = new List<Vector3>();
                agent.recievedPath = true;
                var Path = ASTAR.aSTAR.CalculatePath(agent.sourceNode, agent.targetNode);
                if (Path.Count > 0)
                    for (int i = Path.Count - 1; i > 0; i--)
                    {
                        agent.targetLocation.Add(NavGraph.map.Nodes[Path[i]].transform.position);
                    }
                agent.targetLocation.Add(agent.targetNode.transform.position);
            }
        }

        if (agent.targetLocation.Count > 0)
        {
            agent.steeringBehaviour.SeekOn(new Vector2(agent.targetLocation[0].x, agent.targetLocation[0].z));
            if (Vector3.Distance(agent.targetLocation[0], agent.transform.position) <= 2)
                agent.targetLocation.Remove(agent.targetLocation[0]);
        }
        else
            agent.recievedPath = false;
    }
    public override void Execute(AIAgent agent)
    {

        var Reference = agent.steeringBehaviour.ProjectedCube.CheckForAI(agent.vehicle);
        if (Reference)
        {
            agent.steeringBehaviour.OvertakeOn();
            agent.vehicle.BoostOn();
        }
        MoveOnRoute(agent);
        agent.vehicle.Accelerate(agent.steeringBehaviour.Calculate());

    }
}


