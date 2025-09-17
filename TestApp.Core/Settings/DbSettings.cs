namespace TestApp.Core.Settings;

/// <summary>
/// Настройки для БД
/// </summary>
public class DbSettings
{
    /// <summary>Имя пользователя</summary>
    public string User { get; set; }

    /// <summary>Пароль</summary>
    public string Password { get; set; }

    /// <summary>Хост</summary>
    public string Host { get; set; }

    /// <summary>Порт</summary>
    public string Port { get; set; }

    /// <summary>Имя бд</summary>
    public string DbName { get; set; }
}