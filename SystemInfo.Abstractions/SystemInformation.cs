using System.Collections.Immutable;
using SystemInfo.Abstractions.Cpu;

namespace SystemInfo.Abstractions;

public record SystemInformation(ImmutableArray<CpuInformation> Cpus);

public interface ISystemInformationProvider
{
    SystemInformation Get();
}