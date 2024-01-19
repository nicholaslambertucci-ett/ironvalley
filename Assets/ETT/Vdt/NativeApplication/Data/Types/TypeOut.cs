namespace Ett.Vdt.NativeApplication.Data.Types
{
    public enum TypeOut
    {
        /**
         * In C#, la prima label degli enum assume sempre valore 0, salvo quando viene specificato un valore diverso.
         * In questo caso li ho definiti a priori per mantenere la relazione con l'enum InternalTypeOut, visto che sono
         * un bambino "speciale" e mi piace avere le cose ordinate in ordine alfabetico
         */

        Unknown = 0,
        Galleries = 1,
        Map = 4,
        PikkartArScene = 7,
        Poi = 6,
        Sheet = 2,
        UnitySceneActivator = 3,
        Vr = 5,
    }
}