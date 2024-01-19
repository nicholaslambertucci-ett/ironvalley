using Ett.Vdt.NativeApplication.Data.Types;

namespace Ett.Vdt.NativeApplication.Data.Activators
{
    public struct Activator
    {
        public readonly int Id;
        public readonly TypeIn Type;

        public readonly bool IsValid;

        public Activator(TypeIn type, int id)
        {
            this.Id = id;
            this.Type = type;
            this.IsValid = true;
        }
    }
}