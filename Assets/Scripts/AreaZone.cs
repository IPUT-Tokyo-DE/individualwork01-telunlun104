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

            // �v���C���[�̌`�ƃG���A�̌`����v����ꍇ�̂݃X�R�A���Z�Ώ�
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
                // �v���C���[�̌`�ƈ������G�͑S�폜���邾��
                playerAttach.DetachAndCountMatchingEnemies(""); // ��v���Ȃ��̂ŃX�R�A���Z�͂��Ȃ�
            }
        }
    }
}
