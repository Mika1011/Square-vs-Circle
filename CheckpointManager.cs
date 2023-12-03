using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CheckpointManager : MonoBehaviour
{

    [SerializeField] Stack<Checkpoint> checkpoints = new Stack<Checkpoint>();

    public void add(Checkpoint checkpoint)
    {
        checkpoints.Push(checkpoint);
    }

    public Checkpoint getLastCheck() 
    {
        if (checkpoints.Count != 0)
        {
            return checkpoints.Peek();
        } else { return null; }
    }  
}
