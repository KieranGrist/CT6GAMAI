using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Pit state runs when the AI needs to refuel and allows the AI to complete the strategy
/// </summary>
public class Pit : State<AIAgent>
{
    bool FirstTime = true; //Stores if this is the first time this function has been run
    
    /// <summary>
    /// Sets the target to be the AIS team garage
    /// </summary>
    /// <param name="agent"></param>
    void SetTargetToGarrage(AIAgent agent)
    {
        if (agent.team == "Mercedes") //If the AI is with the team Mercedes path to the garage
        {

            if (Physics.Raycast(Pitlane.pitlane.MercedesPit.transform.position + new Vector3(0, 20, 0), -agent.transform.up, out RaycastHit item, float.PositiveInfinity, agent.mask)) //Raycast to get the node bellow the Mercedes Pit
                agent.targetNode = item.transform.GetComponent<Node>(); //Set the target node to the one that was hit
      
        }
        if (agent.team == "Ford") //If the AI is with the team Ford path to the garage
        {

            if (Physics.Raycast(Pitlane.pitlane.FordPit.transform.position + new Vector3(0, 20, 0), -agent.transform.up, out RaycastHit item, float.PositiveInfinity, agent.mask)) //Raycast to get the node bellow the Ford Pit
                agent.targetNode = item.transform.GetComponent<Node>(); //Set the target node to the one that was hit

        }
        if (agent.team == "Ferrari") //If the AI is with the team Ferrari path to the garage
        {
            if (Physics.Raycast(Pitlane.pitlane.FerrariPit.transform.position + new Vector3(0, 20, 0), -agent.transform.up, out RaycastHit item, float.PositiveInfinity, agent.mask))  //Raycast to get the node bellow the Ferrari Pit
                agent.targetNode = item.transform.GetComponent<Node>(); //Set the target node to the one that was hit

        }
        if (agent.team == "Renault") //If the AI is with the team Renault path to the garage
        {

            if (Physics.Raycast(Pitlane.pitlane.RenaultPit.transform.position + new Vector3(0, 20, 0), -agent.transform.up, out RaycastHit item, float.PositiveInfinity, agent.mask)) //Raycast to get the node Renault the Ferrari Pit
                agent.targetNode = item.transform.GetComponent<Node>(); //Set the target node to the one that was hit

        }
    }
    /// <summary>
    /// Create a Path to the pit
    /// </summary>
    /// <param name="agent"></param>
    void PathToPit(AIAgent agent)
    {
        SetTargetToGarrage(agent); // Set the target to the agents team garage
        agent.SetSourceNode();  //Calls the Set Source Function

        if (agent.SourceNode && agent.targetNode) //Checks that both source node and target node exist otherwise there is errors
        {
            agent.targetLocation.Clear(); //Clear the current Target location 
            agent.targetLocation = new List<Transform>(); //Create a new list 
            var Path = ASTAR.AStar.CalculatePath(agent.SourceNode, agent.targetNode); //Calculate the path then store it
            if (Path.Count > 0) //If Path Exists loop it
                for (int i = Path.Count - 1; i > 0; i--) //Loop it backwads as the calculate path function returns a path where 1 is node before the target node
                {
                    agent.targetLocation.Add(NavGraph.map.Nodes[Path[i]].transform); // add the  path nodes tranform
                }
            agent.targetLocation.Add(agent.targetNode.transform);// Add the target node last
        }
    }

    /// <summary>
    /// Get the distance to the teams garage
    /// </summary>
    /// <param name="agent"></param>
    /// <returns></returns>
    float GetDistanceToGarrage(AIAgent agent)
    {
        float Distance = float.MaxValue; //Set the distance to the max value 
        if (agent.team == "Mercedes") // IF ai is in mercedes team get the distance to the Mercedes pit
            Distance = Vector3.Distance(agent.transform.position, Pitlane.pitlane.MercedesPit.transform.position); //Sets distance to be from the AI to the Mercedes pit 
        if (agent.team == "Ford") // IF ai is in Ford team get the distance to the Ford pit
            Distance = Vector3.Distance(agent.transform.position, Pitlane.pitlane.FordPit.transform.position); //Sets distance to be from the AI to the Ford pit
        if (agent.team == "Ferrari") // IF ai is in Ferrari team get the distance to the Ferrari pit
            Distance = Vector3.Distance(agent.transform.position, Pitlane.pitlane.FerrariPit.transform.position); //Sets distance to be from the AI to the Ferrari pit
        if (agent.team == "Renault") // IF ai is in Renault team get the distance to the Renault pit
            Distance = Vector3.Distance(agent.transform.position, Pitlane.pitlane.RenaultPit.transform.position); //Sets distance to be from the AI to the Renault pit
        return Distance; //Return the distance
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
