# AvaKit
AvaKit is a modular UI component library for Avalonia UI that provides reusable controls with consistent design and a scalable architecture for .NET desktop applications.

Avalonia UI Component Library

## 📦 Components

- InputComponent
- SearchComponent

---

## 🚀 Usage

### InputComponent

```xml
xmlns:input="clr-namespace:AvaKit.Controls.Input;assembly=AvaKit"

<input:InputComponent 
    Label="Name"
    Placeholder="Enter your name"
    Value="{Binding Name}" />

```

### SearchComponent

```xml
xmlns:search="clr-namespace:AvaKit.Controls.Search;assembly=AvaKit"

<search:SearchComponent 
    Placeholder="Search..."
    Value="{Binding SearchText}" />

```