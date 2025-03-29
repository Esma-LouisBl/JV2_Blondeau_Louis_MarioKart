using UnityEngine;

[CreateAssetMenu(fileName = "ItemMushroom", menuName = "Scriptable Objects/ItemMushroom")]
public class ItemChampignon : Item
{
    public override void Activation(PlayerItemManager player)
    {
        player.carControler.Turbo();
    }
}