using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public float offsetx = 1f;
    public float offsety = 1f;
    [SerializeField] private Transform player;

    void Update()
    {
        transform.position = new Vector3(player.position.x + offsety, player.position.y + offsetx, transform.position.z);
    }


}
