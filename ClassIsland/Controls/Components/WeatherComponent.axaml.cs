using System;
using Avalonia;
using Avalonia.Interactivity;
using ClassIsland.Core.Abstractions.Controls;
using ClassIsland.Core.Abstractions.Services;
using ClassIsland.Core.Attributes;
using ClassIsland.Core.Models.Weather;
using ClassIsland.Models;
using ClassIsland.Models.ComponentSettings;
using ClassIsland.Services;
using ReactiveUI;

namespace ClassIsland.Controls.Components;

/// <summary>
/// WeatherComponent.xaml 的交互逻辑
/// </summary>
[ComponentInfo("CA495086-E297-4BEB-9603-C5C1C1A8551E", "天气简报", "\uf465", "显示当前的天气概况和气象预警。")]
public partial class WeatherComponent : ComponentBase<WeatherComponentSettings>
{
    public IWeatherService WeatherService { get; }

    public SettingsService SettingsService { get; }

    private int _aqiLevel;

    public static readonly DirectProperty<WeatherComponent, int> AqiLevelProperty = AvaloniaProperty.RegisterDirect<WeatherComponent, int>(
        nameof(AqiLevel), o => o.AqiLevel, (o, v) => o.AqiLevel = v);

    public int AqiLevel
    {
        get => _aqiLevel;
        set => SetAndRaise(AqiLevelProperty, ref _aqiLevel, value);
    }
    
    private static CurrentWeather GetSpecificWeatherDay(WeatherInfo weatherInfo, int deltaDays)
    {
        if (deltaDays is < 0 or > 13)
            throw new ArgumentOutOfRangeException();
        
        if (deltaDays == 0)
            return weatherInfo.Current;

        var nullValueUnitPair = new ValueUnitPair { Value = "", Unit = "" };
        var temperaturePair = weatherInfo.ForecastDaily.Temperature.Value[deltaDays].OrderedIf(p => int.Parse(p.From) > int.Parse(p.To));
        var windDirectionPair = weatherInfo.ForecastDaily.Wind.Direction.Value[deltaDays];
        var windSpeedPair = weatherInfo.ForecastDaily.Wind.Speed.Value[deltaDays];

        return new CurrentWeather
        {
            FeelsLike = nullValueUnitPair,
            Humidity = nullValueUnitPair,
            Pressure = nullValueUnitPair,
            Temperature = new ValueUnitPair
            {
                Value = $"{temperaturePair.From} ~ {temperaturePair.To}",
                Unit = weatherInfo.ForecastDaily.Temperature.Unit
            },
            Visibility = nullValueUnitPair,
            Weather = weatherInfo.ForecastDaily.Weather.Value[deltaDays].From,
            PublishTime = weatherInfo.ForecastDaily.PublishTime,
            Wind = new CurrentWindInfo
            {
                Direction = new ValueUnitPair
                {
                    Value = $"{windDirectionPair.From} ~ {windDirectionPair.To}",
                    Unit = weatherInfo.ForecastDaily.Wind.Direction.Unit
                },
                Speed = new ValueUnitPair
                    { Value = $"{windSpeedPair.From} ~ {windSpeedPair.To}", Unit = weatherInfo.ForecastDaily.Wind.Speed.Unit }
            }
        };
    }

    private CurrentWeather CurrentWeather => GetSpecificWeatherDay(SettingsService.Settings.LastWeatherInfo, Settings.DeltaDays);
    private bool ShouldShowDeltaDaysText => Settings.DeltaDays > 0;
    private string DeltaDaysText => Settings.DeltaDays == 1 ? "明天" : $"第{Settings.DeltaDays + 1}天";
    private bool ShouldShowAlerts => Settings is { ShowAlerts: true, DeltaDays: 0 };
    

    private IDisposable? _observer;

    public WeatherComponent(IWeatherService weatherService, SettingsService settingsService)
    {
        WeatherService = weatherService;
        SettingsService = settingsService;
        InitializeComponent();
    }

    private void UpdateAqiInfo()
    {
        AqiLevel = SettingsService.Settings.LastWeatherInfo.Aqi.AqiLevel;
    }
    
    private void Control_OnLoaded(object? sender, RoutedEventArgs e)
    {
        _observer?.Dispose();
        _observer = SettingsService.Settings.ObservableForProperty(x => x.LastWeatherInfo)
            .Subscribe(_ => UpdateAqiInfo());
        UpdateAqiInfo();
    }

    private void Control_OnUnloaded(object? sender, RoutedEventArgs e)
    {
        _observer?.Dispose();
    }
}

