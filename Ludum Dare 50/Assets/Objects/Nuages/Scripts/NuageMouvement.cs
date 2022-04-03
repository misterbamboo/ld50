
using System.Collections;
using UnityEngine;

namespace Assets.Objects.Montgolfiere.Scripts
{
    public class NuageMouvement : MonoBehaviour
    {
        [SerializeField] bool printRandomTarget = false;
        [SerializeField] float travelTime = 2f;
        [SerializeField] float distancePerTarget = 2f;
        [SerializeField] Vector3 Offset = new Vector3(20, 5, 20);
        private Vector3 initialPos;

        private Vector3 previousPos;
        private Vector3 targetPos;

        private float movementT;

        private Rigidbody rb;

        void Start()
        {
            rb = GetComponent<Rigidbody>();

            initialPos = transform.position;
            targetPos = initialPos;
            previousPos = targetPos;
            StartCoroutine(ChangeTarget());
        }

        private IEnumerator ChangeTarget()
        {
            while (true)
            {
                previousPos = targetPos;
                var targetVector = RandomVector() * distancePerTarget;
                var candidatePos = previousPos + targetVector;
                targetPos = LimitBounds(candidatePos);
                movementT = 0;
                yield return new WaitForSeconds(travelTime);
            }
        }

        private Vector3 LimitBounds(Vector3 candidatePos)
        {
            float xLimit = Mathf.Clamp(candidatePos.x, initialPos.x - Offset.x, initialPos.x + Offset.x);
            float yLimit = Mathf.Clamp(candidatePos.y, initialPos.y - Offset.y, initialPos.y + Offset.y);
            float zLimit = Mathf.Clamp(candidatePos.z, initialPos.z - Offset.z, initialPos.z + Offset.z);
            return new Vector3(xLimit, yLimit, zLimit);
        }

        private Vector3 RandomVector()
        {
            var randomX = UnityEngine.Random.Range(-1f, 1f);
            var randomY = UnityEngine.Random.Range(-1f, 1f);
            var randomZ = UnityEngine.Random.Range(-1f, 1f);
            var result = new Vector3(randomX, randomY, randomZ).normalized;
            if (printRandomTarget)
            {
                print(result);
            }
            return result;
        }

        private void FixedUpdate()
        {
            if (!rb.isKinematic)
            {
                movementT += Time.deltaTime;
                var t = movementT / travelTime;
                var followPos = Vector3.Lerp(previousPos, targetPos, t);
                var newPos = Vector3.Lerp(transform.position, followPos, 1f * Time.deltaTime);
                rb.MovePosition(newPos);
            }
        }
    }
}
