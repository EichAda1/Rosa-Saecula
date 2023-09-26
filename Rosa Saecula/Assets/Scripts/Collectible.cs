using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour, IDataPersistence
{
    [SerializeField] private string id;

    [ContextMenu("Generate guid for id")]
    private void GenerateGUID()
    {
        id = System.Guid.NewGuid().ToString();
    }

    private SpriteRenderer collectibleSprite;
    private ParticleSystem collectParticle;
    private bool collected = false;

    private void Awake()
    {
        collectibleSprite = this.GetComponentInChildren<SpriteRenderer>();
        collectParticle = this.GetComponentInChildren<ParticleSystem>();
        collectParticle.Stop();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collected)
        {
            collectParticle.Play();
            CollectItem();
        }
    }

    public void LoadData(GameData data)
    {
        data.itemCollection.TryGetValue(id, out collected);
        if (collected) 
        {
            collectibleSprite.gameObject.SetActive(false);
        }
    }

    public void SaveData(ref GameData data) 
    {
        if (data.itemCollection.ContainsKey(id))
        {
            data.itemCollection.Remove(id);
        }
        data.itemCollection.Add(id, collected);
    }

    private void CollectItem()
    {
        collected = true;
        collectibleSprite.gameObject.SetActive(false);
    }
}
