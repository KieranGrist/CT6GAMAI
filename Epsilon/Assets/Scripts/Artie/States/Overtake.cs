using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Overtake state allows the ai to overtake by treeting the person they are overtaking as an obstabcle
/// </summary>
public class Overtake : State<AIAgent>
{    /// <summary>
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
            if (Physics.Raycast(RaceTrack.raceTrack.Sector1.transform.position + new Vector3(0, 20, 0), -agent.transform.up, out item, float.PositiveInfinity, agent.mask)) //Raycast to get the node bellow sector 1
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
    /// Execute the overtake state
    /// Overtake the car infront
    /// </summary>
    /// <param name="agent"></param>
    public override void Execute(AIAgent agent)
    {
        if (!agent.RecievedPath) //If the AI hasnt recieved a path calculate one        
            agent.targetNode = NextTarget(agent); //Set the target node to be the next checkpoint
        var Reference = agent.steeringBehaviour.ProjectedCube.CheckForAI(agent.vehicle); //Gets the closest ai in the projected cube and gets the transform reference
        if (Reference) //If an ai is in the cube
        {
            agent.steeringBehaviour.OvertakeOn(); //Turn on overtaking steering behaviors
            agent.vehicle.BoostOn(); //Turn on the vehicles boost
        }
        agent.vehicle.Accelerate(agent.steeringBehaviour.Calculate()); //Calculates the velocity with steering behaviors then acclerates in that direction
        agent.MoveOnRoute(); //Move the AI allong the generated path      

    }
}


