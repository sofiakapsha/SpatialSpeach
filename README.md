# Spatial Search — Linear Scan vs K-d Tree (C#)

A console application that searches for points of interest within a given radius, 
comparing a brute-force linear approach with an optimized K-d Tree implementation.

## Features

- **Linear search** — scans all points (OpenStreetMap POI data for Ukraine: 
  coordinates, type, name) and calculates the distance to a given point using 
  the Haversine formula (accounts for Earth's curvature).
- **K-d Tree** — builds a binary space-partitioning tree by recursively splitting 
  the point set along latitude/longitude medians, enabling much faster range queries.
- **Polygon search (Bagatokutnik)** — extends the radius search to arbitrary 
  (not necessarily convex) polygons, not just circles.
- **Performance benchmarking** — measures and compares execution time of the 
  linear scan vs. the K-d Tree query using `Stopwatch`.
- **Custom equality/hashing for points** — implemented `ToString`, `Equals`, 
  and `GetHashCode` on the `Point` class for coordinate-based comparisons 
  and hash-set usage.

## Tech Stack

C#, .NET

## How to Run

```bash
dotnet run --project SpatialSearch -- --db=ukraine_poi.csv --lat=50.4501 --long=30.5234 --size=20
```

The program outputs all points found within the given radius and prints the 
elapsed time for both the linear and K-d Tree search methods.

## Project Structure

- `Point.cs` — point representation with custom equality and hashing
- `LinearSearch.cs` — brute-force search implementation
- `KdTree.cs` — K-d Tree construction and range query logic
- `Bagatokutnik.cs` — polygon-based search extension

## Team Project

This project was completed in pairs as part of the CS210 Algorithms for Engineers course.

**My contribution (sofiiakapsha):** linear search implementation, distance calculation 
fixes, polygon search optimization, console interface, performance measurement setup, 
bug fixes across calculations.

**Partner's contribution (sofiialavrynenko25):** K-d Tree construction, `Node` class, 
`Point` class methods, initial polygon search logic.
