using UnityEngine;

public class AreaZone : MonoBehaviour
{
    public string areaShape;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerAttach playerAttach = other.GetComponent<PlayerAttach>();
        PlayerController playerController = other.GetComponent<PlayerController>();

        if (playerAttach != null && playerController != null)
        {
            string playerShape = playerController.GetShapeType();

            // プレイヤーの形とエリアの形が一致する場合のみスコア加算対象
            if (playerShape == areaShape)
            {
                int gainedPoints = playerAttach.DetachAndCountMatchingEnemies(areaShape);

                if (gainedPoints > 0)
                {
                    GameManager.Instance.AddScore(gainedPoints);
                }
            }
            else
            {
                // プレイヤーの形と違ったら敵は全削除するだけ
                playerAttach.DetachAndCountMatchingEnemies(""); // 一致しないのでスコア加算はしない
            }
        }
    }
}
