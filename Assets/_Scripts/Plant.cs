using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    private float currentWaterReceived;
    public PlantValue plantValue;


    void Start()
    {
        switch (plantValue.state_plant)
        {
            case PlantValue.StatePlant.Seed: currentWaterReceived = plantValue.WaterReceived_Seed; break;
            case PlantValue.StatePlant.Phase1: currentWaterReceived = plantValue.WaterReceived_Phase1; break;
            case PlantValue.StatePlant.Phase2: currentWaterReceived = plantValue.WaterReceived_Phase2; break;
            case PlantValue.StatePlant.FullGrowth:
                transform.localScale = Vector3.one * plantValue.startScale;
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (plantValue.state_plant == PlantValue.StatePlant.FullGrowth)
        {
            if (transform.localScale.x < plantValue.endScale)
            {
                transform.localScale += Vector3.one * plantValue.growRate * Time.deltaTime;
            }

            return;
        }

        if(currentWaterReceived <= 0)
        {
            switch(plantValue.state_plant)
            {
                case PlantValue.StatePlant.Seed:
                    if(plantValue.state_grow == PlantValue.StateGrow.FullGrowth_In_Seed)
                    {
                        plantValue.state_plant = PlantValue.StatePlant.FullGrowth;
                        CreateNewPlant(plantValue.FullGrowth);
                    }
                    else
                    {
                        CreateNewPlant(plantValue.Phase1);
                    }
                    break;

                case PlantValue.StatePlant.Phase1:
                    if (plantValue.state_grow == PlantValue.StateGrow.FullGrowth_In_Phase1)
                    {
                        CreateNewPlant(plantValue.FullGrowth);
                    }
                    else
                    {
                        CreateNewPlant(plantValue.Phase2); 
                    }
                    break;

                case PlantValue.StatePlant.Phase2:
                    CreateNewPlant(plantValue.FullGrowth); break;

                default: return;
            }

            Destroy(gameObject);
        }        
    }

    void CreateNewPlant(GameObject Gb)
    {
        PlantValue plant = this.plantValue;

        GameObject game = Instantiate(Gb, transform.position, Quaternion.identity);
        game.AddComponent<Plant>();
        Plant pl = game.GetComponent<Plant>();
        pl.plantValue = plant;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("WaterCollider"))
        {
            currentWaterReceived -= 1;
        }
    }
}
