using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
//https://visualstudiomagazine.com/articles/2012/11/01/priority-queues-with-c.aspx

public class PriorityQueue<T ,Q> where T : IComparable<T>
{

    public int count { get => data.Count; }

    public List<KeyValuePair<T,Q>> data;

    public PriorityQueue()
    {
        this.data = new List<KeyValuePair<T, Q>>();
    }

    public void Enqueue(KeyValuePair<T,Q> item)
    {

        data.Add(item);
    }

    public KeyValuePair<T, Q>  Dequeue()
    {
        data = data.OrderBy(x => x.Key).ToList();
        var Ret = data[0];
           data.Remove(data[0]);
        return Ret;
    }

} // PriorityQueue
