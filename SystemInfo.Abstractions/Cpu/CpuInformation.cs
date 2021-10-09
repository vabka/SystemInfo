namespace SystemInfo.Abstractions.Cpu;

public record CpuInformation(
    string Name,
    int Cores,
    int Threads,
    int L2CacheSize,
    int L3CacheSize,
    CpuArchitecture Architecture,
    bool VirtualizationEnabled);