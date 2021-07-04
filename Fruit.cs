using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour, IFruit
{
    PlantGenes plantGenes;

    private string speciesName;
    public float Energy = 0;
    public int EnergyForSeedCreation;
    public int launchEnergy;
    private int numberOfSeeds;
    private float sizeMultiplier;

    private bool allSeedsReady = false;
    private float seedsCreated;
    private ISeed[] seeds;
    private GameObject[] seedGOs;
    private Action<IFruit> OnDestroyedCallback;

    public void SetUp(PlantGenes plantGenes, float energyFromCreation, Action<IFruit> onDestroyedCallback)
    {
        this.plantGenes = plantGenes;
        this.speciesName = plantGenes.SpeciesName;
        Energy += energyFromCreation;
        EnergyForSeedCreation = plantGenes.SeedCreationEnergy;
        launchEnergy = plantGenes.LaunchSeedEnergy;
        if(sizeMultiplier <= 0)
        {
            numberOfSeeds = 1;
        }
        else
        {
            numberOfSeeds = Mathf.RoundToInt(1 + (10 * (sizeMultiplier - 1)));//At least 1 seed. + 1 for each 0.1 on size multiplier
        }
        seeds = new ISeed[numberOfSeeds];
        seedGOs = new GameObject[numberOfSeeds];
        OnDestroyedCallback += onDestroyedCallback;
    }

    public void EnergyTransfer(float energyTransferred)
    {
        Energy += energyTransferred;

        if(allSeedsReady == false && seedsCreated < numberOfSeeds && Energy > EnergyForSeedCreation)
        {
            CreateSeed();
        }
        else if(allSeedsReady == true)
        {
            if((Energy-(numberOfSeeds * EnergyForSeedCreation)) >= (launchEnergy * numberOfSeeds))
            {
                BLASTOFF();
            }
        }
    }

    void CreateSeed()
    {
        GameObject seedGO = Instantiate(WorldController.current.GetPlantBasedGO(speciesName, WorldController.PlantBasedGOType.Seed));
        seedGO.transform.position = this.transform.position;
        seedGO.transform.SetParent(WorldController.current.plantParent);
        ISeed seed = seedGO.GetComponent<ISeed>();

        int position = -1;
        for (int i = 0; i < seeds.Length; i++)
        {
            if (seeds[i] == null)
            {
                position = i;
                break;
            }
        } //Finds a position in array for the items
        seed.SetUp(plantGenes, EnergyForSeedCreation * plantGenes.EnergryConversionEfficency);
        seeds[position] = seed;
        seedGOs[position] = seedGO;
        seedsCreated++;

        if (seedsCreated == numberOfSeeds)
        {
            allSeedsReady = true;
        }
    }

    private void BLASTOFF()
    {
        float energyForThrow = launchEnergy * numberOfSeeds;

        for (int i = 0; i < seeds.Length; i++)
        {
            ISeed seed = seeds[i];
            seed.Launch(energyForThrow);
        }

        OnDestroyedCallback(this);
        Destroy(this.gameObject);
    }

    public void OnDeath(float emergencyEnergy)
    {
        if(emergencyEnergy < seedsCreated*launchEnergy || emergencyEnergy < EnergyForSeedCreation+(seedsCreated * launchEnergy))
        {
            if(seedsCreated > 0)
            {
                float energyForThrow = emergencyEnergy / seedsCreated;
                for (int i = 0; i < seeds.Length; i++)
                {
                    ISeed seed = seeds[i];
                    seed.Launch(energyForThrow);
                }
            }

            Destroy(this.gameObject);
            return;
        }
        else if(emergencyEnergy > EnergyForSeedCreation + (seedsCreated * launchEnergy))
        {
            GameObject seedGO = Instantiate(WorldController.current.GetPlantBasedGO(speciesName, WorldController.PlantBasedGOType.Seed));
            seedGO.transform.position = this.transform.position;
            ISeed seed = seedGO.GetComponent<ISeed>();

            seed.SetUp(plantGenes, EnergyForSeedCreation * plantGenes.EnergryConversionEfficency);
            seed.Launch(emergencyEnergy - EnergyForSeedCreation);

            Destroy(this.gameObject);
            return;
        }
    }
}
