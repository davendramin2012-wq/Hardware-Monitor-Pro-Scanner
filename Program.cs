using System;
using System.IO;
using System.Management;
using System.Diagnostics;
using System.Linq;

namespace DanieleDevProfessional
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = "SystemReport_DanieleDev.txt";

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== DANIELE DEV PROFESSIONAL WORKSTATION SCANNER ===");
            Console.ResetColor();

            try
            {
                using (StreamWriter sw = new StreamWriter(fileName))
                {
                    sw.WriteLine($"Report generato il: {DateTime.Now}");
                    sw.WriteLine("--------------------------------------------");

                    // 1. SCANSIONE CPU (Ryzen 9 7900X3D)
                    var cpuSearcher = new ManagementObjectSearcher("select Name, NumberOfCores from Win32_Processor");
                    foreach (var item in cpuSearcher.Get())
                    {
                        string cpuInfo = $"[CPU] {item["Name"]} - Core Fisici: {item["NumberOfCores"]}";
                        Console.WriteLine(cpuInfo);
                        sw.WriteLine(cpuInfo);
                    }

                    // 2. SCANSIONE GPU (RTX 5070)
                    var gpuSearcher = new ManagementObjectSearcher("select Name, AdapterRAM from Win32_VideoController");
                    foreach (var item in gpuSearcher.Get())
                    {
                        string gpuName = item["Name"].ToString();
                        // Convertiamo la RAM della GPU in GB
                        long ramBytes = Convert.ToInt64(item["AdapterRAM"]);
                        string gpuInfo = $"[GPU] {gpuName}";
                        Console.WriteLine(gpuInfo);
                        sw.WriteLine(gpuInfo);
                    }

                    // 3. TEST DI CALCOLO (Benchmark per telemetria)
                    Console.WriteLine("[>] Avvio stress test per verifica Developer...");
                    Stopwatch timer = Stopwatch.StartNew();
                    double result = 0;
                    for (int i = 1; i < 5000000; i++)
                    {
                        result += Math.Sqrt(i) * Math.Sin(i);
                    }
                    timer.Stop();

                    string benchInfo = $"[BENCHMARK] Completato in {timer.ElapsedMilliseconds}ms";
                    Console.WriteLine(benchInfo);
                    sw.WriteLine(benchInfo);
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n[SUCCESS] Report salvato in: {Path.GetFullPath(fileName)}");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n[ERROR] Errore durante la scansione: " + ex.Message);
            }

            Console.WriteLine("\nPremi INVIO per inviare i dati ai server di sviluppo...");
            Console.ReadLine();
        }
    }
}