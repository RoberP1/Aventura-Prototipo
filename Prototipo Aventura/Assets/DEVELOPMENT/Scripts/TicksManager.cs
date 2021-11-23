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
    [Range(0f,10000)]public float TickPerSeconds;
    public int tick;
    private float tickTimer;
    void Awake()
    {
        tick = 0;
    }

    // Update is called once per frame
    void Update()
    {
        tickTimer += Time.deltaTime;
        if (tickTimer >= (1/TickPerSeconds))
        {
            tickTimer -= (1 / TickPerSeconds);
            tick++;
            if (OnTick != null) OnTick(this, new OnTickEventArgs { tick = tick });
        }
        //Debug.Log("Tick = " + tick);
    }
}
