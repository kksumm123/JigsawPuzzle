using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PuzzlePiecePoint : MonoBehaviour
{
    public Texture2D originalTexture;
    public Transform pieceParent;
    public List<Sprite> sprites;
    public int xCount = 4;
    public int yCount = 3;

    private void Awake()
    {
        SliceImage();
        InitPosition();
    }

    [ContextMenu("!!!이미지 짤라뻐려!!!")]
    void SliceImage()
    {
        float width = originalTexture.width / xCount;
        float height = originalTexture.height / yCount;
        Vector2 pivot = new Vector2(0.5f, 0.5f);
        float ppu = 100;

        sprites.Clear();
        for (int y = 0; y < yCount; y++)
        {
            for (int x = 0; x < xCount; x++)
            {
                var newSprite = Sprite.Create(originalTexture,
                                                new Rect(x * width,
                                                         originalTexture.height - ((y + 1) * height),
                                                         width,
                                                         height),
                                                pivot,
                                                ppu);
                newSprite.name = $"{x} : {y}";
                sprites.Add(newSprite);
            }
        }
    }

    [ContextMenu("퍼즐 배치")]
    private void InitPosition()
    {
        DeleteOldPiece(transform);
        DeleteOldPiece(pieceParent);
        float pieceWidth = sprites[0].textureRect.width;
        float pieceHeight = sprites[0].textureRect.height;

        int imageIndex = 0;
        List<GameObject> images = new List<GameObject>();
        for (int y = 0; y < yCount; y++)
        {
            for (int x = 0; x < xCount; x++)
            {
                var item = new GameObject($"{x} : {y}", typeof(FixedPiece));
                item.transform.parent = transform;
                RectTransform rt = item.AddComponent<RectTransform>();
                //크기
                rt.sizeDelta = new Vector2(pieceWidth, pieceHeight);

                //위치
                float xPos = x * pieceWidth + (pieceWidth * 0.5f);
                float yPos = -y * pieceHeight - (pieceHeight * 0.5f);
                item.transform.localPosition = new Vector3(xPos, yPos);

                item.AddComponent<Image>().sprite = sprites[imageIndex];
                images.Add(Instantiate(item, pieceParent));

                var curImage = item.GetComponent<Image>();
                var curImageColor = curImage.color;
                curImageColor.a = 0.5f;
                curImage.color = curImageColor;
                imageIndex++;
            }
        }
        foreach (var item in images)
        {
            if (item == null)
                continue;

            if (Application.isPlaying)
                Destroy(item.GetComponent<FixedPiece>());
            else
                DestroyImmediate(item.GetComponent<FixedPiece>());

            item.AddComponent<Piece>();
            item.transform.position =
                new Vector3(Random.Range(0 + pieceWidth * 0.5f, Camera.main.pixelWidth),
                            Random.Range(0 + pieceHeight * 0.5f, Camera.main.pixelHeight));
        }

        GameManager.Instance.completeScore = sprites.Count * 100;
    }

    private void DeleteOldPiece(Transform tr)
    {
        var childs = tr.GetComponentsInChildren<Image>();
        foreach (var item in childs)
        {
            if (Application.isPlaying)
                Destroy(item.gameObject);
            else
                DestroyImmediate(item.gameObject);
        }
    }
}
