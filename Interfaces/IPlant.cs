using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlant
{
    void SetUp(PlantGenes plantGenes, float energyLeftFromSeedState);
}
