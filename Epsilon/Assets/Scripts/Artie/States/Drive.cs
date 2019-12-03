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
            if (Physics.Raycast(RaceTrack.raceTrack.Sector1.transform.position, -agent.transform.up, out item, float.PositiveInfinity, agent.Mask))
                return item.transform.GetComponent<Node>();
        if (Num == 3)
            if (Physics.Raycast(RaceTrack.raceTrack.Sector2.transform.position, -agent.transform.up, out item, float.PositiveInfinity, agent.Mask))
                return item.transform.GetComponent<Node>();
        if (Num == 4) //Comming from pit lane
            if (Physics.Raycast(RaceTrack.raceTrack.Sector1.transform.position, -agent.transform.up, out item, float.PositiveInfinity, agent.Mask))
                return item.transform.GetComponent<Node>();


        if (Physics.Raycast(RaceTrack.raceTrack.FinishLine.transform.position, -agent.transform.up, out item, float.PositiveInfinity, agent.Mask))
            return item.transform.GetComponent<Node>();




        //THIS SHOULD NEVER RUN!!!!!!
        return item.transform.GetComponent<Node>();

    }
    void MoveOnRoute(AIAgent agent)
    {
        if (!agent.RecievedPath)
        {
            if (Physics.Raycast(agent.transform.position, -agent.transform.up, out RaycastHit hit, 20.0f, agent.Mask))
            {
                var Temp = hit.collider.gameObject.GetComponent<Node>();
                if (Temp)
                    agent.SourceNode = Temp;
            }
            agent.TargetNode = NextTarget(agent);
            if (agent.SourceNode && agent.TargetNode)
            {
                agent.TargetLocation.Clear();
                agent.TargetLocation = new List<Vector3>();
                agent.RecievedPath = true;
                var Path = ASTAR.aSTAR.CalculatePath(agent.SourceNode, agent.TargetNode);
                if (Path.Count > 0)
                    for (int i = Path.Count - 1; i > 0; i--)
                    {
                        agent.TargetLocation.Add(NavGraph.map.Nodes[Path[i]].transform.position);
                    }
                agent.TargetLocation.Add(agent.TargetNode.transform.position);
            }
        }

        if (agent.TargetLocation.Count > 0)
        {
            agent.SB.SeekOn(new Vector2(agent.TargetLocation[0].x, agent.TargetLocation[0].z));
            if (Vector3.Distance(agent.TargetLocation[0], agent.transform.position) <= 2)
                agent.TargetLocation.Remove(agent.TargetLocation[0]);
        }
        else
            agent.RecievedPath = false;
    }
    public override void Execute(AIAgent agent)
    {
      MoveOnRoute(agent);
        agent.vehicle.Accelerate(agent.SB.Calculate());
    }

}

