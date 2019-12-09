using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
/// <summary>
/// Drive state allows the AI to race arround the track. 
/// This just drives the AI around the track.
/// </summary>
public class Drive : State<AIAgent>
{
    /// <summary>
    /// Returns the next target for the AI to pathfind to
    /// </summary>
    /// <param name="agent"></param>
    /// <returns></returns>
    private Node NextTarget(AIAgent agent)
    {
        var Num = agent.vehicle.LastCheckPoint + 1; //Increases the current checkpoint tag by one as this will get the next checkpoint
       //e.g. Finish line is tag 1 so the next checkpoint 2 would be sector 1

        //Raycast hit used to get the nodes information
        RaycastHit item;
        if (Num == 2) //If Agent is comming from the finish line
            if (Physics.Raycast(RaceTrack.raceTrack.Sector1.transform.position + new Vector3(0,20,0), -agent.transform.up, out item, float.PositiveInfinity, agent.mask)) //Raycast to get the node bellow sector 1
                return item.transform.GetComponent<Node>(); //Return the hit node

        if (Num == 3) //If Agent is comming from Sector 1
            if (Physics.Raycast(RaceTrack.raceTrack.Sector2.transform.position + new Vector3(0, 20, 0), -agent.transform.up, out item, float.PositiveInfinity, agent.mask)) //Raycast to get the node bellow sector 2
                return item.transform.GetComponent<Node>(); //Return the hit node

        if (Num == 4) //Comming from pit lane or Sector 2
            if (Physics.Raycast(RaceTrack.raceTrack.FinishLine.transform.position + new Vector3(0, 20, 0), -agent.transform.up, out item, float.PositiveInfinity, agent.mask)) //Raycast to get the node bellow Finish line
                return item.transform.GetComponent<Node>(); //Return the hit node

        //If none of these nums are true it means a checkpoint has been taged wrong so it gets sent to the finish line
        if (Physics.Raycast(RaceTrack.raceTrack.FinishLine.transform.position + new Vector3(0, 20, 0), -agent.transform.up, out item, float.PositiveInfinity, agent.mask))  //Raycast to get the node bellow Finish line
            return item.transform.GetComponent<Node>(); //Return the hit node

//Beyond this point is error handling

        Debug.LogError("Failed to find any nodes!"); //Log that a node has not been found
        //THIS SHOULD NEVER RUN!!!!!!
        return item.transform.GetComponent<Node>(); //Try to return a hit node
    }
    /// <summary>
    /// Creates the target path to the new target location
    /// </summary>
    /// <param name="agent"></param>
    void PathToCheckPoint(AIAgent agent)
    { 
        agent.SetSourceNode(); //Calls the Set Source Function

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
    /// Executes the drive state.
    /// Drives the AI around the track.
    /// </summary>
    /// <param name="agent"></param>
    public override void Execute(AIAgent agent)
    {
        agent.steeringBehaviour.OvertakeOff(); //turns of overtaking
        agent.vehicle.BoostOff(); //Turns of vehicle boos
 
        agent.vehicle.Accelerate(agent.steeringBehaviour.Calculate()); //Calculates the velocity with steering behaviors then acclerates in that direction
        if (!agent.RecievedPath) //If the AI hasnt recieved a path calculate one
        {
            agent.targetNode = NextTarget(agent); //Set the target node to be the next checkpoint
            PathToCheckPoint(agent); //Create a path to that target
        }
        agent.MoveOnRoute(); //Move the AI allong the generated path
    }

}

