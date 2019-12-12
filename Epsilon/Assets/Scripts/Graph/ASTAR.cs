using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ASTAR
{
    static List<int> CalculateRoute(Node Source, Node Target)
    {
        var Route = new List<int>(Graph.map.Nodes.Count); //List of ints to store the route searched
        var Cost = new List<float>(Graph.map.Nodes.Count); //List of floats to store the cost of the node


        for (int i = 0; i < Graph.map.Nodes.Count; i++) //Loop through all the nodes 
        {
            Route.Add(-10); //Add minus 10 to the list 
            Cost.Add(int.MaxValue); //add a float with the max value to the list 
        }
        HashSet<Edge> TraveresedEdges = new HashSet<Edge>(); //Create a new hash set of traveresed edges, stores what edges have been searched 
      var MinPriorityQueue = new PriorityQueue<float, Edge>(); //Priority queue for edges to be searchd
        Cost[Source.Index] = 0; //Set cost of the source node to be 0

        foreach (var item in Source.Neighbours) //Loop through source neighbours
        {
            KeyValuePair<float, Edge> keyValuePair = new KeyValuePair<float, Edge>(0, item); //Create a new keyvalue pair of the cost and edge for item
            MinPriorityQueue.Enqueue(keyValuePair); //Enqueue this keyvalue pair 
        }

        while (MinPriorityQueue.count > 0) //While Queue has members
        {
            var ItemToSearch = MinPriorityQueue.Dequeue(); //Dequeue an item and store it
            TraveresedEdges.Add(ItemToSearch.Value);//Add the Item to search edge to the traveresed edge list
            var edge = ItemToSearch.Value; //Set the edge to be the item to search 
            if (Cost[edge.To.Index] > Cost[edge.From.Index] + edge.GetCost()) //If the cost to is more then the cost from added to the edge cost function
            {
                Route[edge.To.Index] = edge.From.Index; //Set route of the index of to be the edge from index
                float HCost = (Mathf.Abs(Target.transform.position.x - edge.To.transform.position.x)) + (Mathf.Abs(Target.transform.position.z - edge.To.transform.position.z)); //Create the h cost using manhaten distanace
                float GCost = Cost[edge.From.Index] + edge.GetCost(); //Set the g cost by getting the cost from the previous node and the value from the get cost function
                float FCost = GCost + HCost; //Add the g cost and h cost and set this to be the f cost
                Cost[edge.To.Index] = FCost; //Set the cost of the edge to to be the calculated f cost
            }
            if (edge.To.Index == Target.Index) //If the target has been found
                break; //Stop the function 
            foreach (var item in edge.To.Neighbours) //For each neihbour in the to node
            {
                float HCost = (Mathf.Abs(Target.transform.position.x - edge.To.transform.position.x)) + (Mathf.Abs(Target.transform.position.z - edge.To.transform.position.z)); //Create the h cost using manhaten distanace
                float GCost = Cost[edge.To.Index] + item.GetCost();//Set the g cost by getting the cost for node we are traveling to and the value of the current edge cost
                float FCost = GCost + HCost; //Add the g cost and h cost and set this to be the f cost//A
                MinPriorityQueue.UpdateCost(item, FCost); //Update any key value pairs that exist
                var valuepair = new KeyValuePair<float, Edge>(FCost, item); //Creates a new keyvalue pair of the cost and item
                if (!TraveresedEdges.Contains(item))     //Check if traveresed edges contains items before running the expensive contains for min priority queue
                {
                    if (!MinPriorityQueue.data.Contains(valuepair)) //Check if the min priority queue contains the value pair 
                    {
                        MinPriorityQueue.Enqueue(valuepair); //Enqueue the value pair to the list

                    }
                }
            }
        }

        return Route;
    }

    public static List<int> CalculatePath(Node Source, Node Target)
    {

     var       Route = CalculateRoute(Source, Target);

        var GeneratedPath = new List<int>();
        int currentNode = Target.Index;
        GeneratedPath.Add(currentNode);
        while (currentNode != Source.Index)
        {
            currentNode = Route[currentNode];
            GeneratedPath.Add(currentNode);
        }

        return GeneratedPath;
    }
}
