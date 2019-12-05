using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pit : State<AIAgent>
{
    bool FirstTime = true;

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
    void GoToPit(AIAgent agent)
    {

        if (agent.team == "Mercedes")
        {
 
            if (Physics.Raycast(Pitlane.pitlane.MercedesPit.transform.position + new Vector3(0, 20, 0), -agent.transform.up, out RaycastHit item, float.PositiveInfinity, agent.mask))
                agent.targetNode = item.transform.GetComponent<Node>();
            agent.recievedPath = false;
        }
        if (agent.team == "Ford")
        {
 
            if (Physics.Raycast(Pitlane.pitlane.FordPit.transform.position + new Vector3(0, 20, 0), -agent.transform.up, out RaycastHit item, float.PositiveInfinity, agent.mask))
                agent.targetNode = item.transform.GetComponent<Node>();
            agent.recievedPath = false;
        }
        if (agent.team == "Ferrari")
        {
            if (Physics.Raycast(Pitlane.pitlane.FerrariPit.transform.position + new Vector3(0, 20, 0), -agent.transform.up, out RaycastHit item, float.PositiveInfinity, agent.mask))
                agent.targetNode = item.transform.GetComponent<Node>();
            agent.recievedPath = false;
        }
        if (agent.team == "Renault")
        {
         
            if (Physics.Raycast(Pitlane.pitlane.RenaultPit.transform.position + new Vector3(0, 20, 0), -agent.transform.up, out RaycastHit item, float.PositiveInfinity, agent.mask))
                agent.targetNode = item.transform.GetComponent<Node>();
            agent.recievedPath = false;
        }
    }
    public override void Execute(AIAgent agent)
    {
        agent.steeringBehaviour.OvertakeOff();
        agent.vehicle.BoostOff();
        var Distance = 0.00f;
        if (agent.team == "Mercedes")
            Distance = Vector3.Distance(agent.transform.position, Pitlane.pitlane.MercedesPit.transform.position);
        if (agent.team == "Ford")
            Distance = Vector3.Distance(agent.transform.position, Pitlane.pitlane.FordPit.transform.position);
        if (agent.team == "Ferrari")
            Distance = Vector3.Distance(agent.transform.position, Pitlane.pitlane.FerrariPit.transform.position);

        if (agent.team == "Renault")
            Distance = Vector3.Distance(agent.transform.position, Pitlane.pitlane.RenaultPit.transform.position);


        if (Distance < 4)
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
               GoToPit(agent);
                FirstTime = false;
            }
            if(!agent.recievedPath)
                     GoToPit(agent);
            MoveOnRoute(agent);
        }
    }
}
