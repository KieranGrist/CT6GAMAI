using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
//https://visualstudiomagazine.com/articles/2012/11/01/priority-queues-with-c.aspx
/// <summary>
/// Priority queue to be used by ASTAR
/// Dequeues the item with the lowest valu
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="Q"></typeparam>
public class PriorityQueue<T, Q> where T : IComparable<T>
{

    public int count { get => data.Count; } //Returns the count of data
    /// <summary>
    /// List of keyvalue pairs in the priority queue
    /// </summary>
    public List<KeyValuePair<T, Q>> data;

    public PriorityQueue()
    {
        this.data = new List<KeyValuePair<T, Q>>(); //Creates a new list
    }
    /// <summary>
    /// Searches for any values within the data that contain the item
    /// If it exists it will update it with the new value
    /// </summary>
    /// <param name="item"></param>
    /// <param name="NewCost"></param>
    public void UpdateCost(Q item, T NewCost)
    {
        for (var i = 0; i < data.Count; i++) //Loop through the list
        {
            if (data[i].Value.Equals(item)) //if current datas value equals the item we are checking for 
            {
                data.Remove(data[i]); //remove that data from the list 
                Enqueue(new KeyValuePair<T, Q>(NewCost, item)); //Enqueue the new keyvalue pair with the updated cost
                break;
            }
        }


    }
    /// <summary>
    /// Adds new keyvalue pairs to the list
    /// </summary>
    /// <param name="item"></param>
    public void Enqueue(KeyValuePair<T, Q> item)
    {
        data.Add(item); //Adds item to the list
    }
    /// <summary>
    /// Sorts the data list and returns an item with the lowest cost
    /// </summary>
    /// <returns></returns>
    public KeyValuePair<T, Q> Dequeue()
    {
        data = data.OrderBy(x => x.Key).ToList(); //Sorts the list by the key 
        var Ret = data[0]; //Store data to be returned 
        data.Remove(data[0]); //Remove data from list
        return Ret; //Return Ret
    }

}
