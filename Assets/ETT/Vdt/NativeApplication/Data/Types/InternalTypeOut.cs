namespace Ett.Vdt.NativeApplication.Data.Types
{
    internal enum InternalTypeOut
    {
        /**
         * In C#, la prima label degli enum assume sempre valore 0, salvo quando viene specificato un valore diverso.
         * In questo caso li ho definiti a priori per mantenere la relazione con l'enum InternalTypeOut, visto che sono
         * un bambino "speciale" e mi piace avere le cose ordinate in ordine alfabetico
         */

        // Galleries
        // ReSharper disable once InconsistentNaming
        GLR = 1,
        // Sheets
        // ReSharper disable once InconsistentNaming
        SHT = 2,
        // Unity Scene Actiovator
        // ReSharper disable once InconsistentNaming
        US_AC = 3,
        // Map
        // ReSharper disable once InconsistentNaming
        MAP = 4,
        // Vr
        // ReSharper disable once InconsistentNaming
        VR = 5,
        // Poi
        // ReSharper disable once InconsistentNaming
        POI = 6,
        // PikkartAr Scene
        // ReSharper disable once InconsistentNaming
        PAR_SCN = 7

    }
}