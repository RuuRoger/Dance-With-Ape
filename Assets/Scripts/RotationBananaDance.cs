using System.Collections;
using UnityEngine;

public class RotationBananaDance : MonoBehaviour
{
    private void RotationBackGroundBanana() => StartCoroutine(RotationBanana());

    private IEnumerator RotationBanana()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            transform.rotation = Quaternion.Euler(0f, 0f, 45f);

            yield return new WaitForSeconds(1f);
            transform.rotation = Quaternion.Euler(0f, 0f, 90f);

            yield return new WaitForSeconds(1f);
            transform.rotation = Quaternion.Euler(0f, 0f, 135f);

            yield return new WaitForSeconds(1f);
            transform.rotation = Quaternion.Euler(0f, 0f, 180f);

            yield return new WaitForSeconds(1f);
            transform.rotation = Quaternion.Euler(0f, 0f, 225f);

            yield return new WaitForSeconds(1f);
            transform.rotation = Quaternion.Euler(0f, 0f, 270f);

            yield return new WaitForSeconds(1f);
            transform.rotation = Quaternion.Euler(0f, 0f, 315f);

            yield return new WaitForSeconds(1f);
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);

        }

    }

    private void Start() => RotationBackGroundBanana();

}
