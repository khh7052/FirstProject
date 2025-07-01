using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public Transform target;    // ���� ������Ʈ(���, �÷��̾� ��)
    public float yOffset = 0;  // Ÿ�ٺ��� ī�޶� �󸶳� ���� ������
    public float smoothSpeed = 1;

    void LateUpdate()
    {
        if (target == null) return; // 1. ���� ������Ʈ�� ������ �ƹ��͵� ���� �ʰ� ����
        Vector3 desiredPosition = new Vector3(transform.position.x, target.position.y + yOffset, transform.position.z);
        // 2. ī�޶� ���� ���� ��ġ(desiredPosition)�� ���
        //    - x�� z�� ���� ī�޶� ��ġ �״��
        //    - y�� Ÿ���� y ��ġ + yOffset��ŭ ����
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        // 3. ī�޶� �� ���� �������� �ʰ�, �ε巴��(desiredPosition ������) �̵��ϵ��� ���
        //    - Lerp�� ���� ��ġ���� ��ǥ ��ġ(desiredPosition)���� ���ݾ� �̵�������
        transform.position = smoothedPosition;
        // 4. ������ ī�޶� ��ġ�� �ε巴�� �̵��� ��ġ�� ����
    }

    public void SetCameraTarget(Transform targetPos)
    {
        target = targetPos;
    }
}