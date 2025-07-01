using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public Transform target;    // 따라갈 오브젝트(블록, 플레이어 등)
    public float yOffset = 0;  // 타겟보다 카메라가 얼마나 위에 있을지
    public float smoothSpeed = 1;

    void LateUpdate()
    {
        if (target == null) return; // 1. 따라갈 오브젝트가 없으면 아무것도 하지 않고 끝냄
        Vector3 desiredPosition = new Vector3(transform.position.x, target.position.y + yOffset, transform.position.z);
        // 2. 카메라가 가고 싶은 위치(desiredPosition)를 계산
        //    - x와 z는 현재 카메라 위치 그대로
        //    - y는 타겟의 y 위치 + yOffset만큼 위로
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        // 3. 카메라가 한 번에 움직이지 않고, 부드럽게(desiredPosition 쪽으로) 이동하도록 계산
        //    - Lerp는 현재 위치에서 목표 위치(desiredPosition)까지 조금씩 이동시켜줌
        transform.position = smoothedPosition;
        // 4. 실제로 카메라 위치를 부드럽게 이동한 위치로 변경
    }

    public void SetCameraTarget(Transform targetPos)
    {
        target = targetPos;
    }
}