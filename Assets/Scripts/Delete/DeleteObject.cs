using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteObject : MonoBehaviour
{
    public CreateBussines bussines;
    public void Destroy()
    {
        bussines.idTaxi = bussines.idTaxi - 1;
        PlayerPrefs.SetInt("TaxiCompanyId", bussines.idTaxi);
        Destroy(gameObject);
    }
}
