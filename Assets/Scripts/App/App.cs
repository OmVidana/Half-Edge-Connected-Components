using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class to handle the application logic for rendering meshes and computing connected components using Half-Edges.
/// </summary>
/// <remarks>
/// This script was partially generated with GPT-4o, to which I contributed code lines, logic and troubleshooting.
/// Mesh Rendering was achieved by watching Brackes: "MESH GENERATION in Unity - Basics": https://www.youtube.com/watch?v=eJEpeUH1EMg 
/// </remarks>
public class App : MonoBehaviour
{
    public GameObject meshHolder;
    private Mesh _mesh;

    private void Start()
    {
        _mesh = new Mesh();
        meshHolder.GetComponent<MeshFilter>().mesh = _mesh;
    }

    public void RenderMesh(List<HalfEdge> halfEdges)
    {
        if (halfEdges == null || halfEdges.Count == 0)
        {
            Debug.LogError("Structure is null or empty. Cannot render mesh.");
            return;
        }

        CreateMeshFromHalfEdge(halfEdges);
    }

    private void CreateMeshFromHalfEdge(List<HalfEdge> halfEdges)
    {
        var verticesSet = new HashSet<Vertex>();
        var facesSet = new HashSet<Face>();

        foreach (var halfEdge in halfEdges)
        {
            verticesSet.Add(halfEdge.Origin);
            facesSet.Add(halfEdge.Face);
        }

        int vertexCount = verticesSet.Count;
        int faceCount = facesSet.Count;

        List<Vector3> vertices = new List<Vector3>(vertexCount);
        List<int> triangles = new List<int>(faceCount * 3);

        Dictionary<Vertex, int> vertexIndexMapping = new Dictionary<Vertex, int>(vertexCount);

        foreach (var vertex in verticesSet)
        {
            vertices.Add(vertex.Origin);
            vertexIndexMapping[vertex] = vertices.Count - 1;
        }

        foreach (var face in facesSet)
        {
            var halfEdge = face.HalfEdges[0];
            int start = vertexIndexMapping[halfEdge.Origin];
            halfEdge = halfEdge.Next;
            int second = vertexIndexMapping[halfEdge.Origin];
            halfEdge = halfEdge.Next;

            do
            {
                int third = vertexIndexMapping[halfEdge.Origin];
                triangles.Add(start);
                triangles.Add(second);
                triangles.Add(third);
                second = third;
                halfEdge = halfEdge.Next;
            } while (halfEdge != face.HalfEdges[0]);
        }

        _mesh.Clear();
        _mesh.SetVertices(vertices);
        _mesh.SetTriangles(triangles, 0);
        _mesh.RecalculateNormals();
    }


    public int ConnectedComponents()
    {
        if (FileProcessor.Structure == null || FileProcessor.Structure.Count == 0)
        {
            Debug.LogError("Structure is null or empty. Cannot compute connected components.");
            return 0;
        }

        List<HalfEdge> halfEdges = FileProcessor.Structure;

        int componentsCount = 0;
        HashSet<Face> visitedFaces = new HashSet<Face>();

        foreach (var halfEdge in halfEdges)
        {
            if (halfEdge.Face != null && !visitedFaces.Contains(halfEdge.Face))
            {
                Stack<Face> stack = new Stack<Face>();
                stack.Push(halfEdge.Face);

                while (stack.Count > 0)
                {
                    var currentFace = stack.Pop();
                    if (!visitedFaces.Contains(currentFace))
                    {
                        visitedFaces.Add(currentFace);

                        foreach (var edge in currentFace.HalfEdges)
                        {
                            if (edge.Twin != null && edge.Twin.Face != null && !visitedFaces.Contains(edge.Twin.Face))
                            {
                                stack.Push(edge.Twin.Face);
                            }
                        }
                    }
                }

                componentsCount++;
            }
        }

        return componentsCount;
    }
}