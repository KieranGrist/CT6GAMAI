﻿using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
//https://visualstudiomagazine.com/articles/2012/11/01/priority-queues-with-c.aspx

public class PriorityQueue<T ,Q> where T : IComparable<T>
{
    private List<KeyValuePair<T,Q>> data;

    public PriorityQueue()
    {
        this.data = new List<KeyValuePair<T, Q>>();
    }

    public void Enqueue(KeyValuePair<T,Q> item)
    {
        data.Add(item);
        int ci = data.Count - 1; // child index; start at end
        while (ci > 0)
        {
            int pi = (ci - 1) / 2; // parent index
            if (data[ci].Key.CompareTo(data[pi].Key) >= 0) break; // child item is larger than (or equal) parent so we're done
            var tmp = data[ci]; data[ci] = data[pi]; data[pi] = tmp;
            ci = pi;
        }
    }

    public KeyValuePair<T, Q>  Dequeue()
    {
        // assumes pq is not empty; up to calling code
        int li = data.Count - 1; // last index (before removal)
        KeyValuePair<T, Q>frontItem = data[0];   // fetch the front
        data[0] = data[li];
        data.RemoveAt(li);

        --li; // last index (after removal)
        int pi = 0; // parent index. start at front of pq
        while (true)
        {
            int ci = pi * 2 + 1; // left child index of parent
            if (ci > li) break;  // no children so done
            int rc = ci + 1;     // right child
            if (rc <= li && data[rc].Key.CompareTo(data[ci].Key) < 0) // if there is a rc (ci + 1), and it is smaller than left child, use the rc instead
                ci = rc;
            if (data[pi].Key.CompareTo(data[ci].Key) <= 0) break; // parent is smaller than (or equal to) smallest child so done
            var tmp = data[pi]; data[pi] = data[ci]; data[ci] = tmp; // swap parent and child
            pi = ci;
        }
        return frontItem;
    }

    public T Peek()
    {
        T frontItem = data[0].Key;
        return frontItem;
    }

    public int Count()
    {
        return data.Count;
    }

    public override string ToString()
    {
        string s = "";
        for (int i = 0; i < data.Count; ++i)
            s += data[i].ToString() + " ";
        s += "count = " + data.Count;
        return s;
    }

    public bool IsConsistent()
    {
        // is the heap property true for all data?
        if (data.Count == 0) return true;
        int li = data.Count - 1; // last index
        for (int pi = 0; pi < data.Count; ++pi) // each parent index
        {
            int lci = 2 * pi + 1; // left child index
            int rci = 2 * pi + 2; // right child index

            if (lci <= li && data[pi].Key.CompareTo(data[lci].Key) > 0) return false; // if lc exists and it's greater than parent then bad.
            if (rci <= li && data[pi].Key.CompareTo(data[rci].Key) > 0) return false; // check the right child too.
        }
        return true; // passed all checks
    } // IsConsistent
} // PriorityQueue
