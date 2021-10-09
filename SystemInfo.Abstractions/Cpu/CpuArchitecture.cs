namespace SystemInfo.Abstractions.Cpu;

[Flags]
public enum CpuArchitecture
{
    X64 = 1,
    X86 = 2,
    Arm = 4,
    Arm32 = 8 | Arm,
    Arm64 = 16 | Arm,
    Unknown = 0
}