using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]private List<AssetItem> _item;
    [SerializeField]private InventoryCell _inventoryCellTemplate;
    [SerializeField]private Transform _container;
    [SerializeField]private Transform _draggingParent;
    [SerializeField]private ItemEnjector _enjector;
    
    private void OnEnable()
    {
        Render(_item);
    }

    public void Render(List<AssetItem> items)
    {
        //clean container
        foreach (Transform child in _container)
            Destroy(child.gameObject);
        
        
        items.ForEach(item =>
        {
            var cell = Instantiate(_inventoryCellTemplate, _container);
            cell.Init(_draggingParent);
            cell.Render(item);

            cell.Ejecting += () => Destroy(cell.gameObject);
            cell.Ejecting += (() => _enjector.EjectFormPool(item, _enjector.transform.position,_enjector.transform.right));
        });
    }
}
