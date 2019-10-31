#if VDT_V_2
namespace Ett.Vdt.NativeApplication
{
    public partial class NativeAppController
    {

        #region Native Callback

        /// <summary>
        /// Callback richiamata dal nativo per istruire l'app dell'avvenuta ricezione del nome
        /// del gameObject a cui inoltrare le risposte.
        /// operazione avviata tramite il metodo <code>SendNativeAppReady</code> richiamato in
        /// <code>Start</code>
        /// </summary>
        // ReSharper disable once UnusedMember.Local
        private void ReceiveNativeAppReady()
        {
            this.OnStart();
        }
        #endregion


        #region Unity Events

        private void Start()
        {
            SendNativeAppReady(this.name);

        }

        #endregion


        #region Platform Methods

        static partial void SendNativeAppReady(string gameObjectName);

        #endregion

    }
}
#endif