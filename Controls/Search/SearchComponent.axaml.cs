using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Material.Icons.Avalonia;
using System.Linq;

namespace AvaKit.Controls.Search;

public partial class SearchComponent : UserControl
{
    public static readonly StyledProperty<string> PlaceholderProperty =
        AvaloniaProperty.Register<SearchComponent, string>(nameof(Placeholder), "Cari...");

    public static readonly StyledProperty<string> ValueProperty =
        AvaloniaProperty.Register<SearchComponent, string>(nameof(Value),
            defaultBindingMode: BindingMode.TwoWay);

    public static readonly new StyledProperty<bool> IsEnabledProperty =
        AvaloniaProperty.Register<SearchComponent, bool>(nameof(IsEnabled), true);

    public static readonly StyledProperty<bool> ShowClearButtonProperty =
        AvaloniaProperty.Register<SearchComponent, bool>(nameof(ShowClearButton), false);

    public static readonly StyledProperty<string> CustomStyleClassProperty =
        AvaloniaProperty.Register<SearchComponent, string>(nameof(CustomStyleClass), string.Empty);

    public string Placeholder
    {
        get => GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    public string Value
    {
        get => GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    public new bool IsEnabled
    {
        get => GetValue(IsEnabledProperty);
        set => SetValue(IsEnabledProperty, value);
    }

    public bool ShowClearButton
    {
        get => GetValue(ShowClearButtonProperty);
        set => SetValue(ShowClearButtonProperty, value);
    }

    public string CustomStyleClass
    {
        get => GetValue(CustomStyleClassProperty);
        set => SetValue(CustomStyleClassProperty, value);
    }

    private TextBox? _textBox;
    private Button? _clearButton;

    public SearchComponent()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
        this.Loaded += OnLoaded;
    }

    private void OnLoaded(object? sender, RoutedEventArgs e)
    {
        _textBox = this.FindControl<TextBox>("SearchTextBox");
        _clearButton = this.FindControl<Button>("ClearButton");
        
        if (_textBox != null)
        {
            _textBox.PropertyChanged += OnTextBoxPropertyChanged;
        }
        
        UpdateClearButtonVisibility();
        UpdateCustomStyleClass();
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);
        if (change.Property == CustomStyleClassProperty)
        {
            UpdateCustomStyleClass();
        }
    }

    private void OnTextBoxPropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
    {
        if (e.Property == TextBox.TextProperty)
        {
            UpdateClearButtonVisibility();
        }
    }

    private void UpdateClearButtonVisibility()
    {
        ShowClearButton = !string.IsNullOrEmpty(Value);
    }

    private void UpdateCustomStyleClass()
    {
        // Hapus semua kelas kustom yang sudah ada (kecuali kelas internal seperti _xxx)
        var toRemove = Classes.Where(c => !c.StartsWith("_")).ToList();
        foreach (var c in toRemove)
        {
            Classes.Remove(c);
        }
        if (!string.IsNullOrEmpty(CustomStyleClass))
        {
            Classes.Add(CustomStyleClass);
        }
    }

    private void OnClearClick(object sender, RoutedEventArgs e)
    {
        Value = string.Empty;
        _textBox?.Focus();
    }
}