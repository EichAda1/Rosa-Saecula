using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistence
{
    void LoadData(GameData data);

    void SaveData(ref GameData data);

    //To save/load:
    //Have field to be saved, EX. -> dataFieldText = this.GetComponent<TextMeshProUGUI>(); 

    //public void LoadData(GameData data)
    //{
    //    this.dataField = data.dataField;
    //}

    //public void SaveData(ref GameData data)
    //{
    //    data.dataField = this.dataField;
    //}

}
