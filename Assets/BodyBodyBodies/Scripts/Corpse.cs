using UnityEngine;

public class Corpse : MonoBehaviour
{
    private Transform movingSwitch;
    private Vector3 lastBlockPos;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Switch"))
        {
            movingSwitch = collision.transform;
            lastBlockPos = movingSwitch.position;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("movingBlock") || collision.gameObject.CompareTag("Switch"))
        {
            movingSwitch = null;
        }
    }

    private void LateUpdate()
    {
        if (movingSwitch != null)
        {
            Vector3 movement = movingSwitch.position - lastBlockPos;
            transform.position += movement;
            lastBlockPos = movingSwitch.position;
        }
    }
}
