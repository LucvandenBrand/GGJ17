using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatAverage {
    private int windowSize;
    private Queue<float> floatQueue;
    private float sum = 0;

	public FloatAverage(int windowSize)
    {
        this.windowSize = windowSize;
        this.floatQueue = new Queue<float>();
    }

    public float GetAverage()
    {
        return sum / floatQueue.Count;
    }
	
	public void Add(float f)
    {
        floatQueue.Enqueue(f);
        sum += f;
        if (floatQueue.Count > windowSize)
        {
            sum -= floatQueue.Dequeue();
        }
    }
}
