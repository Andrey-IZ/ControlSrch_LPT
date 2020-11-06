namespace Drivers.LptIO.lib
{
    public interface IPinsControl
    {
        bool Pin1 { get; }
        bool Pin14 { get; }
        bool Pin16 { get; }
        bool Pin17 { get; }
        void SetPin1 ();
        void SetPin14();
        void SetPin16();
        void SetPin17();
    }

    public interface IPinsData
    {
        bool Pin2 { get; }
        bool Pin3 { get; }
        bool Pin4 { get; }
        bool Pin5 { get; }
        bool Pin6 { get; }
        bool Pin7 { get; }
        bool Pin8 { get; }
        bool Pin9 { get; }
        void SetPin2();
        void SetPin3();
        void SetPin4();
        void SetPin5();
        void SetPin6();
        void SetPin7();
        void SetPin8();
        void SetPin9();
    }

    public interface IPinsStatus
    {
        bool Pin10 { get; }
        bool Pin11 { get; }
        bool Pin12 { get; }
        bool Pin13 { get; }
        bool Pin15 { get; }
    }
}