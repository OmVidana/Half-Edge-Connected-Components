using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Class to handle File Processing for loading 3D models using the format .obj and creating the Half-Edge structure.
/// </summary>
/// <remarks>
/// This script was partially generated with GPT-4o, to which I contributed code lines, logic and troubleshooting.
/// </remarks>
public class FileProcessor
{
    public static List<HalfEdge> Structure { get; private set; }
    public static List<Face> Faces { get; private set; }
    public static List<Vertex> Vertices { get; private set; }

    public static async Task LoadFileAsync(string path)
    {
        string extension = Path.GetExtension(path).ToLower();
        if (extension == ".obj")
            await LoadObjAsync(path);
        else
            Debug.LogError("Unsupported file format: " + extension);
    }

    private static async Task LoadObjAsync(string path)
    {
        if (!File.Exists(path)) return;

        var vertices = new List<Vertex>();
        var faces = new List<Face>();
        
        string[] lines = await Task.Run(() => File.ReadAllLines(path));
        foreach (string line in lines)
        {
            string[] parts = line.Split(new[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries);

            if (line.StartsWith("v "))
            {
                float x = float.Parse(parts[1]);
                float y = float.Parse(parts[2]);
                float z = float.Parse(parts[3]);
                vertices.Add(new Vertex(new Vector3(x, y, z)));
            }
            else if (line.StartsWith("f "))
            {
                List<Vertex> faceVertices = new List<Vertex>();
                for (int i = 1; i < parts.Length; i++)
                {
                    string part = parts[i];
                    int vertexIndex;
                    if (part.Contains('/'))
                        vertexIndex = int.Parse(part.Split('/')[0]) - 1;
                    else
                        vertexIndex = int.Parse(part) - 1;
                    faceVertices.Add(vertices[vertexIndex]);
                }

                faces.Add(CreateHalfEdgesForFace(faceVertices));
            }
        }

        Vertices = vertices;
        Faces = faces;
        Structure = CreateHalfEdgeStructure();
    }

    private static Face CreateHalfEdgesForFace(List<Vertex> vertices)
    {
        var face = new Face();
        var faceHalfEdges = new HalfEdge[vertices.Count];

        for (int i = 0; i < vertices.Count; i++)
        {
            Vertex origin = vertices[i];
            HalfEdge halfEdge = new HalfEdge(origin);
            faceHalfEdges[i] = halfEdge;
            origin.HalfEdge = halfEdge;

            faceHalfEdges[i].Face = face;
            face.HalfEdges.Add(halfEdge);
        }

        for (int i = 0; i < vertices.Count; i++)
        {
            faceHalfEdges[i].Next = faceHalfEdges[(i + 1) % vertices.Count];
            faceHalfEdges[i].Prev = faceHalfEdges[(i + vertices.Count - 1) % vertices.Count];
        }

        face.Vertices.AddRange(vertices);

        return face;
    }

    private static List<HalfEdge> CreateHalfEdgeStructure()
    {
        List<HalfEdge> halfEdges = new List<HalfEdge>();
        Dictionary<(Vertex, Vertex), HalfEdge> halfEdgeDict = new Dictionary<(Vertex, Vertex), HalfEdge>();

        foreach (var face in Faces)
        {
            foreach (var halfEdge in face.HalfEdges)
            {
                halfEdges.Add(halfEdge);
                var twinKey = (halfEdge.Next.Origin, halfEdge.Origin);
                if (halfEdgeDict.TryGetValue(twinKey, out var twin))
                {
                    halfEdge.Twin = twin;
                    twin.Twin = halfEdge;
                }
                else
                {
                    halfEdgeDict[(halfEdge.Origin, halfEdge.Next.Origin)] = halfEdge;
                }
            }
        }

        return halfEdges;
    }
}