using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DFS : MonoBehaviour
{
    /*The DFS algorithm will need to create two lists of equivalent size to
the amount of nodes (pre-initializing this only once is a good idea)
 list of integers which stores the Route from one node to the other
 list of Booleans which stores whether a node has been Visited or not
 Remember our graph has a list of nodes?
 Node n will correspond with index n in both of these lists
     * 
     * 
     * 
     */
    /*
     * Next we are going initialise a stack of Graph Edges
 A stack is a type of Last In, First Out (LIFO) sequential container (like
a list)
 The last element to get added to a stack is the first one to get out
 Next let’s look at the actual algorithm...
*/

    /*
     * We pass in a SourceNode and TargetNode as arguments
     We add all adjacent edges of the source node to the stack and mark the source
    node as visited
     We will enter a while loop (while there are elements in our stack):
     We pop an edge element off the stack
     We find which node index this edge leads to (Edge.To) and change our route element at the
    same index to point to the edge source (in code: Route[Edge.To] = Edge.From)
     Mark that node as visited (Visited[Edge.To] = true)
     If Edge.To == TargetNode
     Exit the function, returning true because we found our target
     Otherwise, add all new adjacent edges to the stack, checking to make sure each one has
    not been visited yet (use a for loop)
     End of loop
     If the loop has ended and reached this point, it means no path to the target was
    found
    */
    public NavGraph Graph;
    public List<int> Route = new List<int>();
    public List<bool> Visited = new List<bool>();
    public Stack<GraphEdge> graphEdges = new Stack<GraphEdge>();
    public int Source;
    public int Target;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
