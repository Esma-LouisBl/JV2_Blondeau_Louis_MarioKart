using UnityEngine;

[CreateAssetMenu(fileName = "ItemLaunchable", menuName = "Scriptable Objects/ItemLaunchable")]
public class ItemLaunchable : Item
{
    public GameObject objectToLaunch;
    public float launchForce = 10f;

    public override void Activation(PlayerItemManager player)
    {
        GameObject launchedObject = Instantiate(objectToLaunch, player.transform.position + player.transform.forward * 4, player.transform.rotation);

        Rigidbody rb = launchedObject.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddForce(player.transform.forward * launchForce, ForceMode.VelocityChange);
        }
    }
}
