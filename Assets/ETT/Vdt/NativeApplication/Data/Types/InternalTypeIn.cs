namespace Ett.Vdt.NativeApplication.Data.Types
{
    internal enum InternalTypeIn
    {
        /**
         * In C#, la prima label degli enum assume sempre valore 0, salvo quando viene specificato un valore diverso.
         * In questo caso li ho definiti a priori per mantenere la relazione con l'enum InternalTypeOut, visto che sono
         * un bambino "speciale" e mi piace avere le cose ordinate in ordine alfabetico
         */

        // Enitry Point
        // ReSharper disable once InconsistentNaming
        EPT = 1,
        // Beacon
        // ReSharper disable once InconsistentNaming
        BCN = 2,
        // Map Item
        // ReSharper disable once InconsistentNaming
        MAP_ITM = 3,
        // ReSharper disable once InconsistentNaming
        GPS = 4,
        // Qr Code
        // ReSharper disable once InconsistentNaming
        QRC = 5,
        // Sheet item
        // ReSharper disable once InconsistentNaming
        SHT_ITM = 6,
        // Pin Code
        // ReSharper disable once InconsistentNaming
        PIN = 7
    }
}