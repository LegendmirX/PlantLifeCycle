using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour, ISeed
{
    PlantGenes plantGenes;
    public float Energy;

    bool HasLaunched = false;
    bool HasLanded = false;
    float launchForce;
    float massKg = 0.1f;
    float gravity = 9.8f;

    float startTime;
    float xPosForVertexOfTheParabola;
    Vector2 direction;
    Vector2 startPos;

    float rootGrowthTime = 5f;
    float rootGrowthTimer = 0f;

    public void SetUp(PlantGenes plantGenes, float energyFromCreation)
    {
        startPos = this.transform.position;
        this.plantGenes = plantGenes;
        Energy = energyFromCreation;
    }

    void Update()
    {
        if(HasLanded == false && HasLaunched == true)
        {
            float time = Time.realtimeSinceStartup - startTime;

            float distanceMoved = (-gravity / 2) * (time * time) + (launchForce * time);
            this.transform.position = startPos + (direction * distanceMoved);

            if (time > xPosForVertexOfTheParabola)
            {
                rootGrowthTimer = 0;
                HasLanded = true;
            }
        }
        else if(HasLanded == true)
        {
            Grow(Time.deltaTime);
        }

    }

    public void Launch(float energyForLaunch)
    {
        float stepOne = energyForLaunch / (massKg / 2);
        launchForce = Mathf.Sqrt(stepOne); //These 2 lines get the launch force from the energy given

        float angle = Random.value * Mathf.PI * 2;
        direction = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)); //these 2 lines pick a direction

        startTime = Time.realtimeSinceStartup;
        xPosForVertexOfTheParabola = -launchForce / (2 * (-gravity/2));

        HasLaunched = true;
    }

    public void Grow(float deltaTime)
    {
        rootGrowthTimer += deltaTime;

        if(rootGrowthTimer >= rootGrowthTime)
        {
            Collider2D[] others = Physics2D.OverlapCircleAll(this.transform.position, (plantGenes.PlantMaxSize/2)*1.2f);
            if(others.Length > 0)
            {
                //bool hasEnoughSpace = true;
                //foreach(Collider2D collider in others)
                //{
                //    float distance = Vector3.Distance(this.transform.position, collider.transform.position);

                //    if(distance < plantGenes.PlantMaxSize * 1.2f)
                //    {
                //        hasEnoughSpace = false;
                //    }
                //}
                //if(hasEnoughSpace == false)
                //{
                //    Destroy(this.gameObject);
                //    return;
                //}

                Destroy(this.gameObject);
                return;
            }

            GameObject plantGO = Instantiate(WorldController.current.GetPlantBasedGO(plantGenes.SpeciesName, WorldController.PlantBasedGOType.Plant));
            IPlant plant = plantGO.GetComponent<IPlant>();

            plantGO.transform.position = this.transform.position;
            plantGO.transform.SetParent(WorldController.current.plantParent);
            plant.SetUp(plantGenes, Energy * plantGenes.EnergryConversionEfficency);

            Destroy(this.gameObject);
        }
    }
}
