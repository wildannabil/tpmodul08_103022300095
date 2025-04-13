using System;
using System.IO;
using System.Text.Json;

public class CovidConfigData
{
    public string satuan_suhu { get; set; }
    public int batas_hari_deman { get; set; }
    public string pesan_ditolak { get; set; }
    public string pesan_diterima { get; set; }
}

public class CovidConfig
{
    private string filePath = "covid_config.json";

    public string satuan_suhu { get; set; }
    public int batas_hari_deman { get; set; }
    public string pesan_ditolak { get; set; }
    public string pesan_diterima { get; set; }

    public CovidConfig()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            CovidConfigData? config = JsonSerializer.Deserialize<CovidConfigData>(json);

            if (config != null)
            {
                satuan_suhu = config.satuan_suhu;
                batas_hari_deman = config.batas_hari_deman;
                pesan_ditolak = config.pesan_ditolak;
                pesan_diterima = config.pesan_diterima;
            }
        }
        else
        {
            // Default config
            satuan_suhu = "celcius";
            batas_hari_deman = 14;
            pesan_ditolak = "Anda tidak diperbolehkan masuk ke dalam gedung ini";
            pesan_diterima = "Anda dipersilahkan untuk masuk ke dalam gedung ini";
            SaveConfig();
        }
    }

    public void SaveConfig()
    {
        var config = new CovidConfigData
        {
            satuan_suhu = this.satuan_suhu,
            batas_hari_deman = this.batas_hari_deman,
            pesan_ditolak = this.pesan_ditolak,
            pesan_diterima = this.pesan_diterima
        };

        string json = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filePath, json);
    }

    public void UbahSatuan()
    {
        if (satuan_suhu == "celcius")
        {
            satuan_suhu = "fahrenheit";
        }
        else
        {
            satuan_suhu = "celcius";
        }
        SaveConfig();
    }
}