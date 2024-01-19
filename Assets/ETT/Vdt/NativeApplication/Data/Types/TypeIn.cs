namespace Ett.Vdt.NativeApplication.Data.Types
{
    public enum TypeIn
    {
        /**
         * In C#, la prima label degli enum assume sempre valore 0, salvo quando viene specificato un valore diverso.
         * In questo caso li ho definiti a priori per mantenere la relazione con l'enum InternalTypeOut, visto che sono
         * un bambino "speciale" e mi piace avere le cose ordinate in ordine alfabetico
         */

        Beacon = 2,
        EntryPoint = 1,
        Gps = 4,
        MapItem = 3,
        PinCode = 7,
        QrCode = 5,
        SheetItem = 6,
    }
}