using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlantGenes
{
    public string SpeciesName;

    public GameObject Plant;
    public GameObject Fruit;
    public GameObject Seed;

    [Space]
    [Header("Energy")]
    public int MaxEnergy;
    [Range(0, 1)] public float EnergryConversionEfficency; /* How effecient will it get the suns energy. 0=0%, 1=100% */

    [Space]
    [Header("Growth")]
    public int MaxGrowth;
    [Range(0, 1)]public float GrowthEnergyUse; /* % of Energy this used this frame. 0=0%, 1=100% */
    [Range(0, 1)] public float MatureGrowthEnergyUse; /* % of Energy this us */
    public float PlantMaxSize; //This will be used for the image scale for now till i come up with a better way of doing this. 

    [Space]
    [Header("Fruit")]
    public int FruitCreationEnergy;
    public float FruitSizeMultiplier;

    [Space]
    [Header("Seed")]
    public int SeedCreationEnergy;
    public int LaunchSeedEnergy;

    #region MinMax's
    private int minEnergyRange;
    private int maxEnergyRange;
    private int maxGrowthRange;
    private int minGrowthRange;
    public int MatureGrowthStage;
    #endregion

    #region BuildFuncs
    public PlantGenes()
    {

    }

    protected PlantGenes(PlantGenes other)
    {
        this.SpeciesName = other.SpeciesName;
        this.minEnergyRange = other.minEnergyRange;
        this.maxEnergyRange = other.maxEnergyRange;
        this.minGrowthRange = other.minGrowthRange;
        this.maxGrowthRange = other.maxGrowthRange;
        this.MatureGrowthStage = other.MatureGrowthStage;
        this.GrowthEnergyUse = other.GrowthEnergyUse;
        this.MatureGrowthEnergyUse = other.MatureGrowthEnergyUse;
        this.PlantMaxSize = other.PlantMaxSize;
        this.EnergryConversionEfficency = other.EnergryConversionEfficency;
        this.FruitCreationEnergy = other.FruitCreationEnergy;
        this.FruitSizeMultiplier = other.FruitSizeMultiplier;
        this.SeedCreationEnergy = other.SeedCreationEnergy;
        this.LaunchSeedEnergy = other.LaunchSeedEnergy;
    }

    virtual public PlantGenes Clone()
    {
        return new PlantGenes(this);
    }

    static public PlantGenes CreatePrototype(PlantData data)
    {
        PlantGenes protoPlant = new PlantGenes();

        protoPlant.SpeciesName = data.SpeciesName;
        protoPlant.Plant = data.Plant;
        protoPlant.Fruit = data.Fruit;
        protoPlant.Seed = data.Seed;
        protoPlant.minEnergyRange = data.minEnergyRange;
        protoPlant.maxEnergyRange = data.maxEnergyRange;
        protoPlant.minGrowthRange = data.minGrowthRange;
        protoPlant.maxGrowthRange = data.maxGrowthRange;
        protoPlant.MatureGrowthStage = data.matureGrowthStage;
        protoPlant.PlantMaxSize = data.PlantMaxSize;
        protoPlant.GrowthEnergyUse = data.growthEnergyUse;
        protoPlant.MatureGrowthEnergyUse = data.matureGrowthEnergyUse;
        protoPlant.EnergryConversionEfficency = data.energryConversionEfficency;
        protoPlant.FruitCreationEnergy = data.growNewFruitEnergyNeeded;
        protoPlant.FruitSizeMultiplier = data.fruitSizeMultiplier;
        protoPlant.SeedCreationEnergy = data.growNewSeedEnergy;
        protoPlant.LaunchSeedEnergy = data.launchSeedEnergy;

        return protoPlant;
    }
    #endregion

    static public PlantGenes CreatePlant(PlantGenes proto)
    {
        PlantGenes plantObj = proto.Clone();

        plantObj.SetUp();

        return plantObj;
    }

    public void SetUp()
    {
        MaxEnergy = Random.Range(minEnergyRange, maxEnergyRange);
        MaxGrowth = Random.Range(minGrowthRange, maxGrowthRange);
    }
}
