namespace ToDo
{
  public sealed class ConfigurationSingleton
  {
    private static readonly Lazy<ConfigurationSingleton> _lazy
        = new Lazy<ConfigurationSingleton>(() => new ConfigurationSingleton());

    public static ConfigurationSingleton Instance { get { return _lazy.Value; } }

    public string? ApplicationName { get; set; }
    public string? ConnectionString { get; set; }

    private ConfigurationSingleton()
    {

    }
  }
}