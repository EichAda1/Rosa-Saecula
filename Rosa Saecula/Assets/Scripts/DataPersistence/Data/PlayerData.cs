using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerData : MonoBehaviour, IDataPersistence
{

    private int deathCount = 0;

    private TextMeshProUGUI deathCountText;

    private void Awake()
    {
        deathCountText = this.GetComponent<TextMeshProUGUI>();
    }

    public void LoadData(GameData data)
    {
        this.deathCount = data.deathCount;
    }

    public void SaveData(ref GameData data)
    {
        data.deathCount = this.deathCount;
    }

    private void Start()
    {
        //GameEventsManager.instance.onPlayerDeath += OnPlayerDeath;
    }

    private void OnDestroy()
    {
        //GameEventsManager.instance.OnPlayerDeath -= OnPlayerDeath;
    }

    private void OnPlayerDeath()
    {
        deathCount++;
    }

    void Update()
    {
        deathCountText.text = "" + deathCount;
    }
}
