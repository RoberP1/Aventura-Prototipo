using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicksManager : MonoBehaviour
{
    public class OnTickEventArgs : EventArgs
    {
        public int tick;
    }
    public static event EventHandler<OnTickEventArgs> OnTick;
    [Range(0, 10000)] public float TicksPerSec;
    public float TickTimerMax = 0.1f;
    public int tick;
    private float tickTimer;
    void Awake()
    {
        
        tick = 0;
    }

    // Update is called once per frame
    void Update()
    {
        TickTimerMax = 1 / TicksPerSec;
        tickTimer += Time.deltaTime;
        if (tickTimer >= TickTimerMax)
        {
            tickTimer -= TickTimerMax;
            tick++;
            if (OnTick != null) OnTick(this, new OnTickEventArgs { tick = tick });
        }
        //Debug.Log("Tick = " + tick);
    }
}
