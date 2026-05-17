# TP2 - Metodología de Pruebas de Sistemas

Práctica Formativa 2 — Tecnicatura en Desarrollo de Software

## Descripción

Implementación de pruebas unitarias para un sistema simple de gestión de productos con las funcionalidades de agregar producto, calcular precio total con impuestos y buscar producto por nombre.

---

## Estructura del proyecto

TP2-METODOLOGIA-DE-ESTUDIO/
├── GestionProductos.Core/             ← lógica de negocio
│   ├── Product.cs                     ← clase Product
│   ├── ProductManager.cs              ← clase ProductManager
│   └── GestionProductos.Core.csproj
├── TP2-METODOLOGIA-DE-ESTUDIO.Tests/  ← pruebas NUnit
│   ├── ProductManagerTests.cs
│   └── TP2-METODOLOGIA-DE-ESTUDIO.Tests.csproj
└── TP2-METODOLOGIA-DE-ESTUDIO.sln

---

## Clases

### Product
Representa un producto del sistema con las propiedades:
- `Id` — identificador único
- `Name` — nombre del producto
- `Price` — precio base
- `Category` — categoría: `"Electrónica"` o `"Alimentos"`

### ProductManager
Gestiona la colección de productos y aplica las reglas de negocio:
- `AddProduct(product)` — agrega un producto a la lista interna. Lanza `ArgumentException` si el precio es negativo.
- `CalculateTotalPrice(product)` — calcula el precio con impuestos según categoría.
- `FindProductByName(name)` — busca un producto por nombre (insensible a mayúsculas). Devuelve `null` si no existe.
- `GetAllProducts()` — devuelve la lista interna como solo lectura.

---

## Reglas de negocio

- El precio base no puede ser negativo.
- Impuesto por categoría:
  - `Electrónica`: +10%
  - `Alimentos`: +5%

---

## Pruebas

| # | Test | Descripción |
|---|------|-------------|
| 1 | `Product_CreadoConDatosValidos_TieneCorrectasLasPropiedades` | Verifica que un producto se crea correctamente con todos sus datos |
| 2 | `AddProduct_ProductoValido_SeAgregaALaLista` | Verifica que el producto se agrega a la lista interna |
| 3 | `FindProductByName_VariosProductosAgregados_EncuentraCadaUno` | Agrega varios productos y verifica que se encuentren por nombre |
| 4 | `CalculateTotalPrice_CategoriaElectronica_AplicaImpuesto10PorCiento` | Verifica el cálculo de precio con 10% para Electrónica |
| 5 | `CalculateTotalPrice_CategoriaAlimentos_AplicaImpuesto5PorCiento` | Verifica el cálculo de precio con 5% para Alimentos |
| 6 | `AddProduct_PrecioNegativo_LanzaArgumentException` | Verifica que un precio negativo lanza excepción |
| 7 | `FindProductByName_NombreInexistente_DevuelveNull` | Verifica que un nombre inexistente devuelve null |

---

## Cómo ejecutar

Requisito: tener instalado .NET SDK.

```bash
# Desde la carpeta raíz del proyecto
dotnet test
```

Resultado esperado:

Resumen de pruebas: total: 7; con errores: 0; correcto: 7; omitido: 0

---

## Tecnologías

- C# / .NET 10
- NUnit 4
- NUnit3TestAdapter
- Microsoft.NET.Test.Sdk