using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class RandomNumberScript : MonoBehaviour
{
    public int[] Array;
    public int sum = 0;
    public int average = 0;
    public float SleepTimer =0;
    public int Max;
    public int Min;
    public int NumberOfTimesReachedTarget =0;
    // Start is called before the first frame update
    void Start()
    {
        Array = new int[200];
    }

    // Update is called once per frame
    void Update()
    {
        SleepTimer += Time.deltaTime;
        if (SleepTimer >= 0.5)
        {
            SleepTimer = 0;
            for (int i = 0; i < Array.Length; i++)
            {
                Array[i] = Random.Range(0, 100);
            }
            sum = 0;
            Max = int.MinValue;
            Min = int.MaxValue;
            for (int i = 0; i < Array.Length; i++)
            {   
                if (Array[i] > Max)
                {
                    Max = Array[i];
                }
                if (Array[i] < Min)
                {
                    Min = Array[i];
                }
                sum += Array[i];
            }
            average = sum / Array.Length;
            if (average >=90)
            {
                NumberOfTimesReachedTarget++;
            }

        }
    }
}
