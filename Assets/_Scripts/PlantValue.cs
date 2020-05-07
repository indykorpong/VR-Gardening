using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlantValue
{
    public enum StatePlant { Seed, Phase1, Phase2, FullGrowth, Harvested}
    public enum StateGrow { FullGrowth_In_Seed ,FullGrowth_In_Phase1, FullGrowth_In_Phase2}

    [Header("plant")]
    public StatePlant state_plant = StatePlant.Seed;

    [Tooltip("Select how many phase it has")] public StateGrow state_grow = StateGrow.FullGrowth_In_Phase1;
    [Space(2)]

    public float WaterReceived_Seed = 10, WaterReceived_Phase1 = 10 , WaterReceived_Phase2 = 10;
    [Space(10)]
    public int num = 0;
    public GameObject Phase1,Phase2,FullGrowth,Harvested;

    [Space(10)]
    public float startScale = 0.4f;
    public float endScale = 1.0f;
    public float growRate = 0.05f;
}
