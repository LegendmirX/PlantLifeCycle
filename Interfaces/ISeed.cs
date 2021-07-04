using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISeed
{
    void SetUp(PlantGenes plantGenes, float energyFromCreation);
    void Launch(float energyForLaunch);
}
