using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomB : Custom
{
    [SerializeField] GameObject Pet;
    [SerializeField] Vector4 MinAndMax_Pos;

    protected override void Start()
    {
        base.Start();
        Set_Action(null, Call_Pet);
    }

    public void Call_Pet()
    {
        Vector3 target_pos = new Vector3(Random.Range(MinAndMax_Pos.x, MinAndMax_Pos.y), Random.Range(MinAndMax_Pos.z, MinAndMax_Pos.w));
        while (target_pos.x > -1.9f && target_pos.x < 1.33f)
        {
            target_pos = new Vector3(Random.Range(MinAndMax_Pos.x, MinAndMax_Pos.y), Random.Range(MinAndMax_Pos.z, MinAndMax_Pos.w));
        }
        Vector3 pos = target_pos;
        GameObject pet = Instantiate(Pet, pos, Quaternion.identity);
        pet.GetComponent<Pet>().Set_Custom(GetComponent<CustomB>());
    }
}
