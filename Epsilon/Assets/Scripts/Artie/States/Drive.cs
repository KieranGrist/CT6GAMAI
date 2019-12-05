using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Drive : State<AIAgent>
{
    private Node NextTarget(AIAgent agent)
    {
        var Num = agent.vehicle.LastCheckPoint + 1;
        RaycastHit item;
        if (Num == 2)
            if (Physics.Raycast(RaceTrack.raceTrack.Sector1.transform.position + new Vector3(0,20,0), -agent.transform.up, out item, float.PositiveInfinity, agent.mask))
                return item.transform.GetComponent<Node>();
        if (Num == 3)
            if (Physics.Raycast(RaceTrack.raceTrack.Sector2.transform.position + new Vector3(0, 20, 0), -agent.transform.up, out item, float.PositiveInfinity, agent.mask))
                return item.transform.GetComponent<Node>();
        if (Num == 4) //Comming from pit lane
            if (Physics.Raycast(RaceTrack.raceTrack.Sector1.transform.position + new Vector3(0, 20, 0), -agent.transform.up, out item, float.PositiveInfinity, agent.mask))
                return item.transform.GetComponent<Node>();


        if (Physics.Raycast(RaceTrack.raceTrack.FinishLine.transform.position + new Vector3(0, 20, 0), -agent.transform.up, out item, float.PositiveInfinity, agent.mask))
            return item.transform.GetComponent<Node>();



        Debug.LogError("Failed to find any nodes!");
        //THIS SHOULD NEVER RUN!!!!!!
        return item.transform.GetComponent<Node>();

    }

    void PathToCheckPoint(AIAgent agent)
    { 
        agent.SetSourceNode();
        if (agent.SourceNode && agent.targetNode)
        {
            agent.targetLocation.Clear();
            agent.targetLocation = new List<Transform>();
            var Path = ASTAR.AStar.CalculatePath(agent.SourceNode, agent.targetNode);
            if (Path.Count > 0)
                for (int i = Path.Count - 1; i > 0; i--)
                {
                    agent.targetLocation.Add(NavGraph.map.Nodes[Path[i]].transform);
                }
            agent.targetLocation.Add(agent.targetNode.transform);
        }
    }

    public override void Execute(AIAgent agent)
    {
        agent.steeringBehaviour.OvertakeOff();
        agent.vehicle.BoostOff();
 
        agent.vehicle.Accelerate(agent.steeringBehaviour.Calculate());
        if (!agent.RecievedPath)
        {
            agent.targetNode = NextTarget(agent);
            PathToCheckPoint(agent);
        }
        agent.MoveOnRoute();
    }

}

