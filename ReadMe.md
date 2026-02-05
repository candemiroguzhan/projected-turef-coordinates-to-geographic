# 📐 Projected Turef Coordinates to Geographic

[![Stars](https://img.shields.io/github/stars/candemiroguzhan/projected-turef-coordinates-to-geographic?style=flat)](https://github.com/candemiroguzhan/projected-turef-coordinates-to-geographic/stargazers) [![Forks](https://img.shields.io/github/forks/candemiroguzhan/projected-turef-coordinates-to-geographic?style=flat)](https://github.com/candemiroguzhan/projected-turef-coordinates-to-geographic/network/members) [![Issues](https://img.shields.io/github/issues/candemiroguzhan/projected-turef-coordinates-to-geographic?style=flat)](https://github.com/candemiroguzhan/projected-turef-coordinates-to-geographic/issues) [![Contributors](https://img.shields.io/github/contributors/candemiroguzhan/projected-turef-coordinates-to-geographic?style=flat)](https://github.com/candemiroguzhan/projected-turef-coordinates-to-geographic/graphs/contributors) [![.NET](https://img.shields.io/badge/.NET-9.0-blue?style=flat)](https://dotnet.microsoft.com/) [![License](https://img.shields.io/badge/License-MIT-yellow.svg?style=flat)](./LICENSE)

A lightweight .NET class library for converting 2D geometries projected in the TUREF Transverse Mercator (TM) system into geographic coordinates (latitude/longitude). Works well with NetTopologySuite and ProjNet and accepts WKT (Well-Known Text) geometries.

---

## ✨ Features

- 📍 Convert TUREF TM zones (TM27, TM30, TM33, TM36, TM39, TM42, TM45) to geographic coordinates
- 🔁 Support for single and batch geometry conversion
- 📦 WKT input/output compatible with NetTopologySuite
- 🌐 Uses GRS80 ellipsoid for transformations
- 🧭 Focused on 2D geometries (POINT, LINESTRING, POLYGON, etc.) for Turkey

### ✅ Supported EPSG codes

- TUREF / TM27 — EPSG:5253
- TUREF / TM30 — EPSG:5254
- TUREF / TM33 — EPSG:5255
- TUREF / TM36 — EPSG:5256
- TUREF / TM39 — EPSG:5257
- TUREF / TM42 — EPSG:5258
- TUREF / TM45 — EPSG:5259

---

## 🛠️ Requirements

- .NET 6/7/8/9 (project configured for .NET 9)
- NetTopologySuite >= 2.6.0
- ProjNet >= 2.0.0

## 🚀 Installation & Build

Clone the repository and build:

```powershell
git clone https://github.com/candemiroguz/projected-turef-coordinates-to-geographic.git
cd projected-turef-coordinates-to-geographic
dotnet build -c Release
```

Optionally package/publish a standalone executable:

```powershell
dotnet publish -c Release -r win-x64 --self-contained true
```

---

## ▶️ Usage

### Single geometry conversion (WKT -> WKT)

```csharp
using ProjectedTurefCoordinatesToGeographic.Dtos;
using ProjectedTurefCoordinatesToGeographic.Converters;

var input = new InputEntityDto
{
    ProjectedWKT = "POLYGON((...))",
    CentralMeridian = 33 // one of: 27, 30, 33, 36, 39, 42, or 45
};

string geographicWkt = GeometryConverter.ConvertTmToGeographic(input);
```

### Batch conversion

```csharp
var list = new List<InputEntityDto> { input /*, other inputs */ };
List<string> results = GeometryConverter.ConvertMultipleTmToGeographic(list);
```

Notes:
- Input/output use WKT strings; integrate NetTopologySuite if you need Geometry objects.
- Library targets 2D projected coordinates (Z/M ignored).

---

## 📤 Output structure

When saving results or packages locally, a recommended structure is:

downloads/
├── output-1.wkt
├── output-2.wkt
└── metadata.csv

CSV metadata columns (suggested): Package/Job name, Input WKT, Output WKT, CentralMeridian, Timestamp, Notes

---

## 📁 Project structure

- `Converters/GeometryConverter.cs` — Core conversion logic
- `Dtos/InputEntityDto.cs` — Input DTO (ProjectedWKT, CentralMeridian, etc.)
- `Helpers/CoordinateSystemFactoryHelper.cs` — Coordinate system and transform creation helper
- `Helpers/InputValidator.cs` — Input validation helpers

---

## ⚠️ Limitations

- Only 2D geometries are supported (Z/M ignored)
- Only the listed TUREF TM zones are supported
- Uses GRS80 ellipsoid assumptions

---

## 🧩 Roadmap

- ✅ Core TM -> geographic conversion
- ✅ Batch conversion support
- 🔄 Add CLI wrapper for common workflows
- 🔄 Add unit/integration tests and CI
- 🔄 Add sample console app and usage scripts

---

## 🤝 Contributing

Contributions are welcome! Please:

1. Fork the repo and create a feature branch
2. Add changes and tests
3. Open a Pull Request describing your changes

For larger changes, open an issue first to discuss the design.

---

## 👤 Author

Oğuzhan Candemir — Geospatial Software Developer
GitHub: https://github.com/candemiroguzhan

---

## 📄 License

This project is licensed under the MIT License. See the `LICENSE` file for details.

---

## ⭐ Support

If you find this project useful:
- Star the repository ⭐
- Share feedback
- Contribute enhancements
