using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopData", menuName = "ScriptableObjects/ShopData", order = 1)]
public class ShopData : ScriptableObject
{
    public ShopItemDatas<HatType> hats;
    public ShopItemDatas<PantType> pants;
    public ShopItemDatas<AccessoryType> accessorys;
    public ShopItemDatas<SkinType> skins;
}

[System.Serializable]
public class ShopItemDatas<T> where T : System.Enum
{
    [SerializeField] List<ShopItemData<T>> items;
    public List<ShopItemData<T>> Iteams => items;

    // Lấy item trong list theo enum t
    public ShopItemData<T> GetItem(T t)
    {
        return items.Single(q => q.type.Equals(t));
    }
}

[System.Serializable]
public class ShopItemData<T> : ShopItemData
{
    public T type;
}

public class ShopItemData
{
    public Sprite sprite;
    public int cost;
}

public enum HatType
{
    None = PoolType.None,
    Arrow = PoolType.HAT_Arrow,
    Cap = PoolType.HAT_Cap,
    Cowboy = PoolType.HAT_Cowboy,
    Crown = PoolType.HAT_Crown,
    Ear = PoolType.HAT_Ear,
    Headphone = PoolType.HAT_Headphone,
    Horn = PoolType.HAT_Horn,
    Police = PoolType.HAT_Police,
    StrawHat = PoolType.HAT_StrawHat,
}
public enum AccessoryType
{
    None = PoolType.None,
    Book = PoolType.ACC_Book,
    Captain = PoolType.ACC_Captain,
    HeadPhone = PoolType.ACC_Headphone,
    Shield = PoolType.ACC_Shield,
}

public enum WeaponType
{
    Hammer_1 = PoolType.W_Hammer_1,
    Hammer_2 = PoolType.W_Hammer_2,
    Hammer_3 = PoolType.W_Hammer_3,
    Candy_1 = PoolType.W_Candy_1,
    Candy_2 = PoolType.W_Candy_2,
    Candy_3 = PoolType.W_Candy_3,
    Boomerang_1 = PoolType.W_Boomerang_1,
    Boomerang_2 = PoolType.W_Boomerang_2,
    Boomerang_3 = PoolType.W_Boomerang_3,
}
public enum BulletType
{
    Hammer_1 = PoolType.B_Hammer_1,
    Hammer_2 = PoolType.B_Hammer_2,
    Hammer_3 = PoolType.B_Hammer_3,
    Candy_1 = PoolType.B_Candy_1,
    Candy_2 = PoolType.B_Candy_2,
    Candy_3 = PoolType.B_Candy_3,
    Boomerang_1 = PoolType.B_Boomerang_1,
    Boomerang_2 = PoolType.B_Boomerang_2,
    Boomerang_3 = PoolType.B_Boomerang_3,
}

public enum SkinType
{
    Angle = PoolType.SKIN_Angle,
    Deadpool = PoolType.SKIN_Deadpool,
    Devil = PoolType.SKIN_Devil,
    Normal = PoolType.SKIN_Normal,
    Thor = PoolType.SKIN_Thor,
    Witch = PoolType.SKIN_Witch,
}
public enum PantType
{
    Pant_1, Pant_2, Pant_3, Pant_4, Pant_5, Pant_6, Pant_7, Pant_8, Pant_9,
}
