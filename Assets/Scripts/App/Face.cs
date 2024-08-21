// The MIT License
// Copyright © 2024 Omar Vidaña Rodríguez
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions: The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Represents a face used for Mesh Rendering and Processing.
/// </summary>
public class Face
{
    /// <summary>
    /// Gets the vertices of the face.
    /// </summary>
    public List<Vertex> Vertices { get; private set; }

    /// <summary>
    /// Gets or sets the half-edges associated with this face.
    /// </summary>
    public List<HalfEdge> HalfEdges { get; set; }

    /// <summary>
    /// Gets or sets the normal vector of the face.
    /// </summary>
    public Vector3 Normal { get; set; }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="Face"/> class.
    /// </summary>
    public Face()
    {
        Vertices = new List<Vertex>();
        HalfEdges = new List<HalfEdge>();
        Normal = Vector3.zero;
    }
}
