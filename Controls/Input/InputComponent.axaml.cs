using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Material.Icons;
using Material.Icons.Avalonia;
using System.Linq;

namespace AvaKit.Controls.Input;

public partial class InputComponent : UserControl
{
    public static readonly StyledProperty<string> LabelProperty =
        AvaloniaProperty.Register<InputComponent, string>(nameof(Label));

    public static readonly StyledProperty<string> PlaceholderProperty =
        AvaloniaProperty.Register<InputComponent, string>(nameof(Placeholder));

    public static readonly StyledProperty<string> ValueProperty =
        AvaloniaProperty.Register<InputComponent, string>(nameof(Value),
            defaultBindingMode: BindingMode.TwoWay);

    public static readonly StyledProperty<bool> IsInputEnabledProperty =
        AvaloniaProperty.Register<InputComponent, bool>(nameof(IsInputEnabled), true);

    public static readonly StyledProperty<bool> IsPasswordProperty =
        AvaloniaProperty.Register<InputComponent, bool>(nameof(IsPassword), false);

    public static readonly StyledProperty<bool> ShowPasswordToggleProperty =
        AvaloniaProperty.Register<InputComponent, bool>(nameof(ShowPasswordToggle), true);

    public static readonly StyledProperty<string> ErrorTextProperty =
        AvaloniaProperty.Register<InputComponent, string>(nameof(ErrorText), string.Empty);

    public static readonly StyledProperty<bool> HasErrorProperty =
        AvaloniaProperty.Register<InputComponent, bool>(nameof(HasError), false);

    public static readonly StyledProperty<string> CustomStyleClassProperty =
        AvaloniaProperty.Register<InputComponent, string>(nameof(CustomStyleClass), string.Empty);

    public string Label
    {
        get => GetValue(LabelProperty);
        set => SetValue(LabelProperty, value);
    }

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

    public bool IsInputEnabled
    {
        get => GetValue(IsInputEnabledProperty);
        set => SetValue(IsInputEnabledProperty, value);
    }

    public bool IsPassword
    {
        get => GetValue(IsPasswordProperty);
        set => SetValue(IsPasswordProperty, value);
    }

    public bool ShowPasswordToggle
    {
        get => GetValue(ShowPasswordToggleProperty);
        set => SetValue(ShowPasswordToggleProperty, value);
    }

    public string ErrorText
    {
        get => GetValue(ErrorTextProperty);
        set => SetValue(ErrorTextProperty, value);
    }

    public bool HasError
    {
        get => GetValue(HasErrorProperty);
        set => SetValue(HasErrorProperty, value);
    }

    public string CustomStyleClass
    {
        get => GetValue(CustomStyleClassProperty);
        set => SetValue(CustomStyleClassProperty, value);
    }

    private TextBox? _textBox;
    private MaterialIcon? _toggleIcon;

    public InputComponent()
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
        _textBox = this.FindControl<TextBox>("InputTextBox");
        _toggleIcon = this.FindControl<MaterialIcon>("ToggleIcon");

        var toggleButton = this.FindControl<Button>("PasswordToggleButton");
        if (toggleButton != null)
        {
            toggleButton.IsVisible = IsPassword && ShowPasswordToggle;
        }

        if (_textBox != null && IsPassword)
        {
            _textBox.PasswordChar = '*';
        }

        UpdateErrorClass();
        UpdateCustomStyleClass();
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);
        if (change.Property == HasErrorProperty)
        {
            UpdateErrorClass();
        }
        else if (change.Property == CustomStyleClassProperty)
        {
            UpdateCustomStyleClass();
        }
    }

    private void UpdateErrorClass()
    {
        if (_textBox != null)
        {
            if (HasError)
                _textBox.Classes.Add("error");
            else
                _textBox.Classes.Remove("error");
        }
    }

    private void UpdateCustomStyleClass()
    {
        // Hapus semua kelas kustom sebelumnya (yang bukan kelas internal)
        var toRemove = Classes.Where(c => c != "error" && !c.StartsWith("_")).ToList();
        foreach (var c in toRemove)
        {
            Classes.Remove(c);
        }
        if (!string.IsNullOrEmpty(CustomStyleClass))
        {
            Classes.Add(CustomStyleClass);
        }
    }

    private void OnTogglePasswordClick(object sender, RoutedEventArgs e)
    {
        if (_textBox == null || _toggleIcon == null) return;

        if (_textBox.PasswordChar == '*')
        {
            _textBox.PasswordChar = '\0';
            _toggleIcon.Kind = MaterialIconKind.EyeOff;
        }
        else
        {
            _textBox.PasswordChar = '*';
            _toggleIcon.Kind = MaterialIconKind.Eye;
        }
    }
}