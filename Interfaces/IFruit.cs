using System;

public interface IFruit
{
    void SetUp(PlantGenes plantGenes, float energyFromCreation, Action<IFruit> onDestroyedCallback);
    void EnergyTransfer(float energyTransferred);
    void OnDeath(float emergencyEnergy);
}
