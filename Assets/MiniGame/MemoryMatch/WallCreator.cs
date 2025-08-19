using UnityEngine;

public class WallCreator : MonoBehaviour
{
    private void Start()
    {
        CreateWalls();
    }

    private void CreateWalls()
    {
        // Получаем границы камеры
        float screenAspect = (float)Screen.width / Screen.height;
        float cameraHeight = Camera.main.orthographicSize * 2;
        Vector2 cameraSize = new Vector2(cameraHeight * screenAspect, cameraHeight);

        // Создаем 4 стены
        LayerMask layer = LayerMask.NameToLayer("MiniGame");

        CreateWall("TopWall", new Vector2(0, cameraSize.y / 2), new Vector2(cameraSize.x, 0.1f), layer);
        CreateWall("BottomWall", new Vector2(0, -cameraSize.y / 2), new Vector2(cameraSize.x, 0.1f), layer);
        CreateWall("LeftWall", new Vector2(-cameraSize.x / 2, 0), new Vector2(0.1f, cameraSize.y), layer);
        CreateWall("RightWall", new Vector2(cameraSize.x / 2, 0), new Vector2(0.1f, cameraSize.y), layer);
    }

    private void CreateWall(string name, Vector2 position, Vector2 size, LayerMask layer)
    {
        GameObject wall = new GameObject(name);
        wall.layer = layer;
        wall.tag = "Wall";
        wall.transform.position = position;
        BoxCollider2D collider = wall.AddComponent<BoxCollider2D>();
        collider.size = size;
    }
}