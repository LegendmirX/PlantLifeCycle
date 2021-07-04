using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class WorldController : MonoBehaviour
{
    public static WorldController current;

    public Transform plantParent;
    [Space]
    [Header("PlantSpawnData")]
    [SerializeField] private Vector2 spawnAreaSize;
    [SerializeField] private float spacingRadius;

    private Dictionary<string, PlantGenes> PlantPrototypes;

    private void Awake()
    {
        if (current != null)
        {
            Debug.LogError("There should only be one WorldController");
            Destroy(this);
        }
        else
        {
            current = this;
        }
    }

    void Start()
    {
        PrototypeManager protoManager = FindObjectOfType<PrototypeManager>();

        PlantPrototypes = protoManager.BuildPlantPrototypes();

        Camera.main.transform.position = new Vector3(spawnAreaSize.x/2, spawnAreaSize.y/2, Camera.main.transform.position.z);

        SpawnPlants();
    }

    private void SpawnPlants()
    {
        List<Vector2> points = PoissonDiscSampling.GeneratePoints(spacingRadius, spawnAreaSize);

        foreach (Vector2 point in points)
        {
            SpawnPlant(point);
        }
    }

    private void SpawnPlant( Vector2 point)
    {
        float startingEnergy = 10; /*randomize a starting energy*/
        int plantChoiceInt = UnityEngine.Random.Range(1, PlantPrototypes.Count+1);
        Debug.Log("RandomNum: " + plantChoiceInt + "  ProtosCount: " + PlantPrototypes.Count);
        string plantChoice = null;
        int i = 1;
        foreach (string key in PlantPrototypes.Keys)
        {
            if (i == plantChoiceInt)
            {
                plantChoice = key;
            }
            i++;
        }


        PlantGenes plantGenes = PlantGenes.CreatePlant(PlantPrototypes[plantChoice]);
        GameObject plantGO = Instantiate(PlantPrototypes[plantChoice].Plant);
        plantGO.transform.position = point;
        plantGO.transform.SetParent(plantParent);
        IPlant plant = plantGO.GetComponent<IPlant>();
        plant.SetUp(plantGenes, startingEnergy);
        
    }

    public enum PlantBasedGOType
    {
        Seed,
        Fruit,
        Plant
    }
    public GameObject GetPlantBasedGO(string speciesName, PlantBasedGOType GOType)
    {
        GameObject go = null;

        switch (GOType)
        {
            case PlantBasedGOType.Seed:
                go = PlantPrototypes[speciesName].Seed;
                break;
            case PlantBasedGOType.Fruit:
                go = PlantPrototypes[speciesName].Fruit;
                break;
            case PlantBasedGOType.Plant:
                go = PlantPrototypes[speciesName].Plant;
                break;
        }

        return go;
    }
}
