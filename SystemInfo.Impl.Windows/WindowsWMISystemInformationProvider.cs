using System.Collections.Immutable;
using System.Management;
using SystemInfo.Abstractions;
using SystemInfo.Abstractions.Cpu;

namespace SystemInfo.Impl.Windows;

public class WindowsWmiSystemInformationProvider : ISystemInformationProvider
{
    private static readonly ManagementClass ProcessorClass = new("win32_processor");

    public SystemInformation Get()
    {
        return new SystemInformation(GetCpus().ToImmutableArray());
    }

    private IEnumerable<CpuInformation> GetCpus()
    {
        foreach (var instance in ProcessorClass.GetInstances())
        {
            yield return new CpuInformation(
                Name: (string)instance.Properties["Name"].Value,
                Cores: (int)(uint)instance.Properties["NumberOfCores"].Value,
                Threads: (int)(uint)instance.Properties["ThreadCount"].Value,
                L2CacheSize: (int)(uint)instance.Properties["L2CacheSize"].Value * 1024,
                L3CacheSize: (int)(uint)instance.Properties["L3CacheSize"].Value * 1024,
                Architecture: ConvertArchitecture((ushort)instance.Properties["Architecture"].Value),
                VirtualizationEnabled: (bool)instance.Properties["VirtualizationFirmwareEnabled"].Value
            );
        }
    }

    private static CpuArchitecture ConvertArchitecture(ushort cmiValue) =>
        cmiValue switch
        {
            0 => CpuArchitecture.X86,
            5 => CpuArchitecture.Arm,
            9 => CpuArchitecture.X64,
            _ => CpuArchitecture.Unknown
        };
}