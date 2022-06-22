namespace KT.Quanta.Service.Devices.Common
{
    public interface IHandleElevatorSequenceList<T1, T2>
    {
        void Add(T1 key, T2 data);
        T2 Get(T1 key);
    }
}