using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Items{
public class ItemCollectable : MonoBehaviour
{
    [SerializeField] private string compareTag = "Player";
   
    [Header("Sounds")]
    public AudioSource audioSource;

    public ParticleSystem particleSystem;

    public ItemType itemType;

    public Collider collider;

    private void OnTriggerEnter(Collider other){
        if(other.transform.CompareTag(compareTag)){
            Collect();
        }
    }

   protected virtual void Collect()
    {
        if(collider != null) collider.enabled = false;
        Debug.Log("Collect");
        gameObject.SetActive(false);
        OnCollect();
    }

    // Update is called once per frame
   protected virtual void OnCollect()
    {
        ItemManager.Instance.AddByType(ItemType.COIN);
        if(audioSource != null) audioSource.Play();
        if(particleSystem != null) particleSystem.Play();
        ItemManager.Instance.AddByType(itemType);
    }
}
}
