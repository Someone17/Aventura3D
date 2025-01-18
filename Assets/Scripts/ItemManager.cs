using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ebac.Singleton;

namespace Items{
public enum ItemType{
    COIN,
    LIFE_PACK
}
public class ItemManager : Singleton<ItemManager>
 {

    public List<ItemSetup> itemSetups;
   /* public SOInt coins;
    public TextMeshProUGUI uiTextCoins;*/

    private void Start(){
        Reset();
    }

    private void Reset(){
        foreach(var i in itemSetups){

            i.soInt.value = 0;
        }
        //UpdateUI();
    }

    // Update is called once per frame
    public ItemSetup GetItemByType(ItemType itemType)
    {
        
        return itemSetups.Find(i => i.itemType == itemType);
      //  UpdateUI();
    }

    public void AddByType(ItemType itemType, int amount = 1)
    {
        if(amount < 0) return;
        itemSetups.Find(i => i.itemType == itemType).soInt.value += amount;
      //  UpdateUI();
    }

    public void RemoveByType(ItemType itemType, int amount = 1){
        
        var item = itemSetups.Find(i => i.itemType == itemType);
        item.soInt.value -= amount;

        if(item.soInt.value < 0) item.soInt.value = 0;
    }

    [NaughtyAttributes.Button]
    private void AddCoin(){
        AddByType(ItemType.COIN);
    }

    [NaughtyAttributes.Button]
    private void AddLifePack(){
        AddByType(ItemType.LIFE_PACK);
    }


 }
    [System.Serializable]
    public class ItemSetup{
        public ItemType itemType;
        public SOInt soInt;
        public Sprite icon;
    }
}
