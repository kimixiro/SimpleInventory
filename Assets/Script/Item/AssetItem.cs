using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item")]
public class AssetItem : ScriptableObject, IItem
{
    public string Name
    {
        get => _name;
        set => _name = value;
    }

    public Sprite UIIcon
    {
        get => _iuIcon;
        set => _iuIcon = value;
    }

    [SerializeField]private string _name;
    [SerializeField]private Sprite _iuIcon;
}
