using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class IStatus : MonoBehaviour
{
    private TicksManager ticksManager;
    private StarterAssetsInputs _input;
    public float health, energy, hunger, thirst;
    public float healthTick, energyTick, hungerTick, thirstTick;
    public float energyTickWalk, hungerTickWalk, thirstTickWalk;
    public float energyTickSprint, hungerTickSprint, thirstTickSprint;
    public float healthMax, energyMax, hungerMax, thirstMax;
    

    private void Start()
    {
        _input = GetComponent<StarterAssetsInputs>();
        ticksManager = FindObjectOfType<TicksManager>();
    }
    private void Update()
    {
        
        
        ActualizarTick();



        if (energy <= 0)
        {
            _input.sprint = false;
        }
        //hunger = Mathf.Clamp(hunger += Time.deltaTime / hungerTick, 0, hungerMax);
    }

    private void ActualizarTick()
    {
        TicksManager.OnTick += TimeTickSystem_OnTick;
    }

    private void ReducirSed(float thirstTick)
    {
        if (thirst > 0) thirst -= 1 / thirstTick;
    }

    private void ReducirHambre(float hungerTick)
    {
        if (hunger > 0) hunger -= 1 / hungerTick;
    }

    private void UpdateEnergia(float energyTick)
    {
        if (energy <= energyMax && energy >= 0) energy += 1 / energyTick;
        if (energy < 0) energy = 0;
        if (energy > energyMax) energy = energyMax;
    }

    private void AumentarVida(float healthTick)
    {
        if (health < healthMax) health += 1 / healthTick;
        if (health >= healthMax) health = healthMax;
    }
    private void TimeTickSystem_OnTick(object sender, TicksManager.OnTickEventArgs e)
    {
        energyTick = _input.sprint ? energyTickSprint : energyTickWalk;
        hungerTick = _input.sprint ? hungerTickSprint : hungerTickWalk;
        thirstTick = _input.sprint ? thirstTickSprint : thirstTickWalk;
        AumentarVida(healthTick);
        UpdateEnergia(energyTick);
        ReducirHambre(hungerTick);
        ReducirSed(thirstTick);
    }
}
