using System.Threading.Tasks;
using UnityEngine;

public class PieceAnimator : MonoBehaviour
{
    public static async Task ShakePieceAsync(Piece piece)
    {
        float shakeDuration = 0.5f;
        float shakeIntensity = 0.1f;
        float elapsedTime = 0f;

        Vector3 originalPosition = piece.transform.position;

        while (elapsedTime < shakeDuration)
        {
            elapsedTime += Time.deltaTime;
            float offsetX = Mathf.Sin(Time.time * 25) * shakeIntensity;
            piece.transform.position = new Vector3(originalPosition.x + offsetX, originalPosition.y, originalPosition.z);
            await Task.Yield();
        }

        piece.transform.position = originalPosition;
    }
}