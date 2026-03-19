using UnityEngine;
using UnityEngine.UI;

public class ArrowIndicator : MonoBehaviour
{
    [SerializeField] private Transform _finishPoint;
    [SerializeField] private Transform _player;

    private void Update()
    {
        if (_finishPoint == null || _player == null) return;

        // ✅ Sembunyikan panah saat game over atau congrats muncul
        if (GameManager.instance.IsGameEnded())
        {
            gameObject.SetActive(false);
            return;
        }

        Vector2 direction = (Vector2)(_finishPoint.position - _player.position);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}