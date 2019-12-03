using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pit : State<AIAgent>
{
    bool FirstTime = true;

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

        var Distance = 0.00f;
        if (FirstTime)
        {
            if (agent.Team == "Mercedes")
            {
                Distance = Vector3.Distance(agent.transform.position, Pitlane.pitlane.MercedesPit.transform.position);
                if (Physics.Raycast(Pitlane.pitlane.MercedesPit.transform.position + new Vector3(0, 20, 0), -agent.transform.up, out RaycastHit item, float.PositiveInfinity, agent.Mask))
                    agent.TargetNode = item.transform.GetComponent<Node>();
                agent.RecievedPath = false;
            }
            if (agent.Team == "Ford")
            {
                Distance = Vector3.Distance(agent.transform.position, Pitlane.pitlane.FordPit.transform.position);
                if (Physics.Raycast(Pitlane.pitlane.FordPit.transform.position + new Vector3(0, 20, 0), -agent.transform.up, out RaycastHit item, float.PositiveInfinity, agent.Mask))
                    agent.TargetNode = item.transform.GetComponent<Node>();
                agent.RecievedPath = false;
            }
            if (agent.Team == "Ferrari")
            {
                Distance = Vector3.Distance(agent.transform.position, Pitlane.pitlane.FerrariPit.transform.position);
                if (Physics.Raycast(Pitlane.pitlane.FerrariPit.transform.position + new Vector3(0, 20, 0), -agent.transform.up, out RaycastHit item, float.PositiveInfinity, agent.Mask))
                    agent.TargetNode = item.transform.GetComponent<Node>();
                agent.RecievedPath = false;
            }
            if (agent.Team == "Renault")
            {
                Distance = Vector3.Distance(agent.transform.position, Pitlane.pitlane.RenaultPit.transform.position);
                if (Physics.Raycast(Pitlane.pitlane.RenaultPit.transform.position + new Vector3(0, 20, 0), -agent.transform.up, out RaycastHit item, float.PositiveInfinity, agent.Mask))
                    agent.TargetNode = item.transform.GetComponent<Node>();
                agent.RecievedPath = false;
            }
            FirstTime = false;
        }
        MoveOnRoute(agent);
        agent.vehicle.Accelerate(agent.SB.Calculate());
        if (agent.Team == "Mercedes")
            Distance = Vector3.Distance(agent.transform.position, Pitlane.pitlane.MercedesPit.transform.position);
        if (agent.Team == "Ford")
            Distance = Vector3.Distance(agent.transform.position, Pitlane.pitlane.FordPit.transform.position);
        if (agent.Team == "Ferrari")
            Distance = Vector3.Distance(agent.transform.position, Pitlane.pitlane.FerrariPit.transform.position);

        if (agent.Team == "Renault")
            Distance = Vector3.Distance(agent.transform.position, Pitlane.pitlane.RenaultPit.transform.position);


        if (Distance < 4)
        {
            agent.Artie_Pit = 1;
            agent.Artie_Drive = 0;
            agent.vehicle.PerformStop();

            if (agent.vehicle.Fuel >= 98)
            {
                FirstTime = true;
                agent.Artie_Pit = 0;
                agent.Artie_Drive = 1;
            }
        }
    }
}
