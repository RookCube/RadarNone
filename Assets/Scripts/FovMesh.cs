using System.Collections.Generic;
using UnityEngine;

public class FovMesh : MonoBehaviour
{
    private Fov _fov;
    private Mesh _mesh;
    private float _meshRes = 2;
    [Range(0, 10)]
    public float quality;
    void Start()
    {
        _mesh = GetComponent<MeshFilter>().mesh;
        _fov = GetComponentInParent<Fov>();
    }

    void LateUpdate()
    {
        MakeMesh();
    }

    private void MakeMesh()
    {
        var stepCount = Mathf.RoundToInt(_fov.viewAngle * _meshRes * quality);
        var stepAngle = _fov.viewAngle / stepCount;
        var viewVertex = new List<Vector3>();
        for (int i = 0; i <= stepCount; i++)
        {
            var angle = _fov.transform.eulerAngles.y - _fov.viewAngle / 2 + stepAngle * i;
            Vector3 dir = _fov.DirFromAngle(angle, false);
            var hit = Physics2D.Raycast(_fov.transform.position, dir, _fov.viewRadius, _fov.obstacleMask);

            if (hit.collider == null)
            {
                viewVertex.Add(transform.position + dir.normalized * (_fov.viewRadius));
            }
            else
            {
                viewVertex.Add(transform.position + dir.normalized * hit.distance);
            }
        }

        int vertexCount = viewVertex.Count + 1;

        var verticals = new Vector3[vertexCount];
        var triangles = new int[(vertexCount - 2) * 3];
        for (int i = 0; i < vertexCount - 1; i++)
        {
            verticals[i + 1] = transform.InverseTransformPoint(viewVertex[i]);
            if (i < vertexCount - 2)
            {
                var calculate = i * 3;
                triangles[calculate + 2] = 0;
                triangles[calculate + 1] = i + 1;
                triangles[calculate] = i + 2;   
            }
        }
        _mesh.Clear();

        _mesh.vertices = verticals;
        _mesh.triangles = triangles;
        _mesh.RecalculateNormals();
    }
}
