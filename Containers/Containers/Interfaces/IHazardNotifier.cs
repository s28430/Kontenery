using Containers.Enums;

namespace Containers.Interfaces;

public interface IHazardNotifier
{
    public string Notify(DangerCause cause);
}