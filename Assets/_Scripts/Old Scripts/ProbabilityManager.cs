using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProbabilityManager : MonoBehaviour
{

    public int[] counts = { 0, 0, 0, 0, 0, 0, 0 };
    public int total = 0;
    public float[] probabilties = { 0, 0, 0, 0, 0, 0, 0 };
    // Start is called before the first frame update
    void Start()
    {
        MultiplierManager.AddProbability += AddProbability;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddProbability(int index)
    {
        counts[index]++;
        total++;
        probabilties[index] = (float) (counts[index]) / total;
    }
}
