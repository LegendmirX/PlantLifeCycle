using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeManager: MonoBehaviour
{

    public List<PlantData> PlantPrototypes;
    //public List<ItemData> ItemPrototypes;

    public Dictionary<string, PlantGenes> BuildPlantPrototypes()
    {
        Debug.Log("PlantProtos");
        Dictionary<string, PlantGenes> plantProtos = new Dictionary<string, PlantGenes>();

        foreach(PlantData data in PlantPrototypes)
        {
            Debug.Log("-"+data.SpeciesName);
            plantProtos.Add(data.SpeciesName, PlantGenes.CreatePrototype(data));
        }

        return plantProtos;
    }

    //public Dictionary<Item.ItemType, Item> BuildItemPrototypes()
    //{
    //    Debug.Log("ItemProtos");
    //    Dictionary<Item.ItemType, Item> itemProtos = new Dictionary<Item.ItemType, Item>();

    //    foreach (ItemData data in ItemPrototypes)
    //    {
    //        itemProtos.Add(data.itemType,
    //            Item.CreatePrototype(
    //                data.itemType,
    //                data.resourceType,
    //                data.rarity,
    //                data.slotType,
    //                data.cost,
    //                data.weight,
    //                data.isStackable,
    //                data.sprite
    //                )
    //            );
    //    }

    //    return itemProtos;
    //}

    //public static Dictionary<Task.Type, Task> BuildTaskPrototypes()
    //{
    //    Dictionary<Task.Type, Task> tasks = new Dictionary<Task.Type, Task>();

    //    tasks.Add(Task.Type.Collect,
    //        Task.CreatePrototype(
    //            Task.Type.Collect,
    //            TaskActions.GetActions(Task.Type.Collect)
    //            )
    //        );
    //    tasks.Add(Task.Type.Deliver,
    //        Task.CreatePrototype(
    //            Task.Type.Deliver,
    //            TaskActions.GetActions(Task.Type.Deliver)
    //            )
    //        );
    //    tasks.Add(Task.Type.Construct,
    //        Task.CreatePrototype(
    //            Task.Type.Construct,
    //            TaskActions.GetActions(Task.Type.Construct)
    //            )
    //        );
    //    tasks.Add(Task.Type.Cut,
    //        Task.CreatePrototype(
    //            Task.Type.Cut,
    //            TaskActions.GetActions(Task.Type.Cut)
    //            )
    //        );

    //    return tasks;
    //}

    //public static Dictionary<string, Job> BuildJobPrototypes()
    //{
    //    Dictionary<string, Job> jobProtos = new Dictionary<string, Job>();

    //    jobProtos.Add("House",
    //        Job.CreatePrototype(
    //            Job.Priority.High,
    //            "House",
    //            new Dictionary<string, int> { ["Wood"] = 5 },
    //            10f,
    //            3
    //            )
    //        );
    //    jobProtos.Add("StoreHouse",
    //        Job.CreatePrototype(
    //            Job.Priority.High,
    //            "StoreHouse",
    //            new Dictionary<string, int> { ["Wood"] = 20 },
    //            40f,
    //            5
    //            )
    //        );
    //    jobProtos.Add("Storage",
    //        Job.CreatePrototype(
    //            Job.Priority.High,
    //            "Storage",
    //            new Dictionary<string, int>(),
    //            2f,
    //            1
    //            )
    //        );

    //    return jobProtos;
    //}
    
}

[CreateAssetMenu(fileName = "PlantData", menuName = "Prototypes/PlantData")]
public class PlantData : ScriptableObject
{
    public string SpeciesName;
    public GameObject Plant;

    [Space]
    [Header("EnergyStats")]
    public int minEnergyRange;
    public int maxEnergyRange;
    [Range(0, 1)] public float energryConversionEfficency; /* How effecient will it get the suns energy. 0=0%, 1=100% */

    [Space]
    [Header("GrowthStats")]
    public int minGrowthRange;
    public int maxGrowthRange;
    public int matureGrowthStage;
    [Range(0, 1)] public float growthEnergyUse; /* % of Energy used this frame. 0=0%, 1=100% */
    [Range(0, 1)] public float matureGrowthEnergyUse;/* % of Energy used this frame. 0=0%, 1=100% */
    public float PlantMaxSize; //This will be used for the image scale for now till i come up with a better way of doing this. 

    [Space]
    [Header("FruitStats")]
    public GameObject Fruit;
    public int growNewFruitEnergyNeeded;
    public float fruitSizeMultiplier;
    [Range(0, 1)] public float FruitEnergyEfficency; /* How effecient will it transfer energy to fruit. 0=0%, 1=100% */

    [Space]
    [Header("Seed")]
    public GameObject Seed;
    public int growNewSeedEnergy;
    public int launchSeedEnergy;

}

//[CreateAssetMenu(fileName = "ItemData", menuName = "Prototypes/ItemData")]
//public class ItemData : ScriptableObject
//{
//    [Space]
//    [Header("Settings")]
//    public Item.ItemType itemType;
//    public Town.ResourceType resourceType;
//    public Item.Rarity rarity;
//    public EquipSlotType slotType;
//    public int cost;
//    public float weight;
//    public bool isStackable;
//    public Sprite sprite;

//    public List<BaseModifier> baseModifiers;
//}
