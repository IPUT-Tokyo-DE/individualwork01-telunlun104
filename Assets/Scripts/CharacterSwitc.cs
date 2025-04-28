using UnityEngine;

public class CharacterSwitc : MonoBehaviour
{
    public GameObject[] characterPrefabs; // 切り替え先キャラのプレハブ
    private GameObject currentCharacter; // 今操作中のキャラ
    private int currentIndex = 0;        // 現在のキャラ番号

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnCharacter(currentIndex, Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J)) SwitchToCharacter(0);
        if (Input.GetKeyDown(KeyCode.K)) SwitchToCharacter(1);
        if (Input.GetKeyDown(KeyCode.L)) SwitchToCharacter(2);
    }

    void SwitchToCharacter(int index)
    {
        if (index < 0 || index >= characterPrefabs.Length) return;

        if (index == currentIndex) return;

        if (currentCharacter != null)
        {
            var attachScript = currentCharacter.GetComponent<PlayerAttach>();
            if (attachScript != null)
            {
                attachScript.DetachAllEnemies();
            }

            Vector3 currentPosition = currentCharacter.transform.position;
            Quaternion currentRotation = currentCharacter.transform.rotation;

            currentIndex = index;
            Destroy(currentCharacter);

            currentIndex = index;

            SpawnCharacter(currentIndex, currentPosition, currentRotation);
        }
    }

    void SpawnCharacter(int index, Vector3 position, Quaternion rotation = default)
    {
        currentCharacter = Instantiate(characterPrefabs[index], position, rotation);
    }
}