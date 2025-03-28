using UnityEngine;

[CreateAssetMenu(fileName = "ItemSplash", menuName = "Scriptable Objects/ItemSplash")]
public class ItemSplash : Item
{
    public override void Activation(PlayerItemManager player)
    {
        player.carControler.Splash();
    }
}
