using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour, IPlant
{
    public enum GrowthStage
    {
        Sprout,
        Mature
    }
    public GrowthStage growthStage;
    public PlantGenes plantGenes;

    private float energyRecivedThisFrame;
    public float Energy;
    public float Growth;
    private float scale;

    [Space]
    [Header("Fruits")]
    [SerializeField] private Transform fruitPointsParent;
    public int amountOfFruits = 0;
    private IFruit[] fruits;

    public void SetUp(PlantGenes plantGenes, float energyLeftFromSeedState)
    {
        this.plantGenes = plantGenes;
        growthStage = GrowthStage.Sprout;
        Energy += energyLeftFromSeedState;
        fruits = new IFruit[fruitPointsParent.childCount];
        scale = 0.01f;
        this.transform.localScale = new Vector3(scale, scale, 1);
    }
    public void Start()
    {
        Sun.current.sunOutputCallback += Photosynthesize;
    }

    public void Update()
    {
        switch (growthStage)
        {
            case GrowthStage.Sprout:
                float energyForGrowth = energyRecivedThisFrame * plantGenes.GrowthEnergyUse;
                Grow(energyForGrowth);
                break;
            case GrowthStage.Mature:
                float M_energyForGrowth = energyRecivedThisFrame * plantGenes.MatureGrowthEnergyUse;
                float energyForFruit = (energyRecivedThisFrame - (energyRecivedThisFrame * plantGenes.MatureGrowthEnergyUse)) * 0.5f;
                Grow(M_energyForGrowth);
                FeedOffspring(energyForFruit);
                break;
        }
    }

    private void Photosynthesize(float sunEnergy)
    {
        energyRecivedThisFrame = sunEnergy * plantGenes.EnergryConversionEfficency;
        this.Energy += energyRecivedThisFrame;
    }

    private void Grow(float energyForGrowth)
    {
        Energy -= energyForGrowth;
        Growth += energyForGrowth * plantGenes.EnergryConversionEfficency;

        if (growthStage == GrowthStage.Sprout)
        {
            scale = plantGenes.PlantMaxSize * (Growth / plantGenes.MatureGrowthStage);
            this.transform.localScale = new Vector3(scale, scale, 1);
        }
        if (growthStage != GrowthStage.Mature && Growth > plantGenes.MatureGrowthStage)
        {
            growthStage = GrowthStage.Mature;
            this.transform.localScale = new Vector3(plantGenes.PlantMaxSize, plantGenes.PlantMaxSize, 1);
        }
        if(Growth >= plantGenes.MaxGrowth)
        {
            if(amountOfFruits > 0)
            {
                float emergencyEnergy = Energy / amountOfFruits;

                for (int i = 0; i < fruits.Length; i++)
                {
                    if (fruits[i] != null)
                    {
                        IFruit fruit = fruits[i];
                        fruit.EnergyTransfer(emergencyEnergy * plantGenes.EnergryConversionEfficency);
                    }
                }
            }
            Destroy(this.gameObject);
        }
    }

    void CreateFruit()
    {
        List<int> openSpots = new List<int>();
        for (int i = 0; i < fruits.Length; i++)
        {
            if (fruits[i] == null)
            {
                openSpots.Add(i);
            }
        } //finds empty position in array

        if (openSpots.Count <= 0)
        {
            Debug.LogError("amount of fruits is less than fruit positions but we cant find an empty position?");
        }
        int position = openSpots[Random.Range(0, openSpots.Count-1)];

        Transform fruitParent = fruitPointsParent.GetChild(position);
        GameObject GOtoSpawn = WorldController.current.GetPlantBasedGO(plantGenes.SpeciesName, WorldController.PlantBasedGOType.Fruit);
        GameObject fruitGO = Instantiate(GOtoSpawn, fruitParent, false);

        IFruit fruit = fruitGO.GetComponent<IFruit>();
        float energyUsed = plantGenes.FruitCreationEnergy;
        Energy -= energyUsed;
        fruit.SetUp(plantGenes, energyUsed / 2, OnFruitGone);
        fruits[position] = fruit;
        amountOfFruits++;
    }

    private void FeedOffspring(float energyForFruit)
    {
        float energyToTransfer = energyForFruit / amountOfFruits;
        for (int i = 0; i < fruits.Length; i++)
        {
            if (fruits[i] != null)
            {
                IFruit fruit = fruits[i];
                Energy -= energyToTransfer;
                fruit.EnergyTransfer(energyToTransfer * plantGenes.EnergryConversionEfficency);
            }
        }

        if (amountOfFruits < fruitPointsParent.childCount)
        {
            if(Energy > plantGenes.FruitCreationEnergy)
            {
                CreateFruit();
            }
        }
    }

    private void OnFruitGone(IFruit fruit)
    {
        int position = -1;
        for (int i = 0; i < fruits.Length; i++)
        {
            if(fruits[i] == fruit)
            {
                position = i ;
                break;
            }
        } //Finds Position in array

        if (position == -1)
        {
            Debug.LogError("We recived fruit gone but we cant find the fruit in the list");
        }

        fruits[position] = null;
        amountOfFruits--;
    }
}
