namespace BookSale.Managerment.Domain.Extension
{
  public static class LoadEnvFile
  {
    public static Dictionary<string, string> Load(string filePath)
    {
      var envVars = new Dictionary<string, string>();

      if (File.Exists(filePath))
      {
        foreach (var line in File.ReadAllLines(filePath))
        {
          if (!string.IsNullOrWhiteSpace(line) && !line.StartsWith("#"))
          {
            var parts = line.Split('=', 2);
            if (parts.Length == 2)
            {
              envVars[parts[0].Trim()] = parts[1].Trim();
            }
          }
        }
      }
      else
      {
        Console.WriteLine("⚠️ File .env không tồn tại!");
      }

      return envVars;
    }
  }
}