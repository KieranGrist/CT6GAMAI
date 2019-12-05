using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pit : State<AIAgent>
{
    bool FirstTime = true;
    void SetTargetToGarrage(AIAgent agent)
    {
        if (agent.team == "Mercedes")
        {

            if (Physics.Raycast(Pitlane.pitlane.MercedesPit.transform.position + new Vector3(0, 20, 0), -agent.transform.up, out RaycastHit item, float.PositiveInfinity, agent.mask))
                agent.targetNode = item.transform.GetComponent<Node>();
      
        }
        if (agent.team == "Ford")
        {

            if (Physics.Raycast(Pitlane.pitlane.FordPit.transform.position + new Vector3(0, 20, 0), -agent.transform.up, out RaycastHit item, float.PositiveInfinity, agent.mask))
                agent.targetNode = item.transform.GetComponent<Node>();
         
        }
        if (agent.team == "Ferrari")
        {
            if (Physics.Raycast(Pitlane.pitlane.FerrariPit.transform.position + new Vector3(0, 20, 0), -agent.transform.up, out RaycastHit item, float.PositiveInfinity, agent.mask))
                agent.targetNode = item.transform.GetComponent<Node>();
 
        }
        if (agent.team == "Renault")
        {

            if (Physics.Raycast(Pitlane.pitlane.RenaultPit.transform.position + new Vector3(0, 20, 0), -agent.transform.up, out RaycastHit item, float.PositiveInfinity, agent.mask))
                agent.targetNode = item.transform.GetComponent<Node>();
   
        }
    }

    void PathToPit(AIAgent agent)
    {
        SetTargetToGarrage(agent);
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
    float GetDistanceToGarrage(AIAgent agent)
    {
        float Distance = float.MaxValue;
        if (agent.team == "Mercedes")
            Distance = Vector3.Distance(agent.transform.position, Pitlane.pitlane.MercedesPit.transform.position);
        if (agent.team == "Ford")
            Distance = Vector3.Distance(agent.transform.position, Pitlane.pitlane.FordPit.transform.position);
        if (agent.team == "Ferrari")
            Distance = Vector3.Distance(agent.transform.position, Pitlane.pitlane.FerrariPit.transform.position);
        if (agent.team == "Renault")
            Distance = Vector3.Distance(agent.transform.position, Pitlane.pitlane.RenaultPit.transform.position);
        return Distance;
    }
    public override void Execute(AIAgent agent)
    {
        agent.steeringBehaviour.OvertakeOff();
        agent.vehicle.BoostOff();

        if (GetDistanceToGarrage(agent) < 4)
        {
            agent.artiePit = 1;
            agent.artieDrive = 0;
            agent.vehicle.PerformStop();

            if (agent.vehicle.Fuel >= 98)
            {
                FirstTime = true;
                agent.artiePit = 0;
                agent.artieDrive = 1;
            }
        }
        else
        {
            agent.vehicle.Accelerate(agent.steeringBehaviour.Calculate());
            if (FirstTime)
            {
               PathToPit(agent);
                FirstTime = false;
            }
            if(!agent.RecievedPath)
                     PathToPit(agent);
            agent.MoveOnRoute();
        }
    }
}
