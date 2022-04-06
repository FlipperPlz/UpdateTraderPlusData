using System.Data;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using Microsoft.VisualBasic;

class ATMUpdater {
    static void Main(string[] arguments) {
        var inputFolder = arguments[0];
        var outputFolder = inputFolder + "\\" + "_new";
        List<string> oneHundredClub = new();
        Directory.CreateDirectory(outputFolder);
        
        var oldJsonFiles = Directory.GetFiles(inputFolder, "*.json", SearchOption.AllDirectories);


        foreach (var filePath in oldJsonFiles) {

            var filename = Path.GetFileName(filePath);
            var newFilePath = $"{outputFolder}\\{filename}";

            try {
                AccountData? oldAccountData = JsonSerializer.Deserialize<AccountData>(File.ReadAllText(filePath));


                var newAccountData = new AccountData {
                    Version = "2.2",
                    SteamID64 = Path.GetFileName(filePath).Remove(0, 8)
                        .Substring(0, (Path.GetFileName(filePath).Length - 8) - 5),
                    Name = oldAccountData.Value.Name,
                    MoneyAmount = oldAccountData.Value.MoneyAmount,
                    MaxAmount = oldAccountData.Value.MaxAmount,
                    Licences = oldAccountData.Value.Licences,
                    Insurances = oldAccountData.Value.Insurances
                };
                
                File.WriteAllText(newFilePath,
                    JsonSerializer.Serialize(newAccountData, new JsonSerializerOptions {WriteIndented = true}));
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }

            foreach (var s in oneHundredClub) {
                Console.WriteLine(s);
            }
        }
    }
    struct AccountData {
        public string Version { get; set; }
        public string SteamID64 { get; set; }
        public string Name { get; set; }
        public int MoneyAmount { get; set; }
        public int MaxAmount { get; set; }
        public object[] Licences { get; set; }
        public object Insurances { get; set; }
    }
}
