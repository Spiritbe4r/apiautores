namespace webapiautores.services
{
    public interface IService
    {
        void RealizarTarea();
        Guid ObtenerTransient();
        Guid ObtenerSingleton();
        Guid ObtenerScope();
    }
    public class ServicioA:IService
    {
        private readonly ILogger<ServicioA>logger;
        private readonly ServicioTransient servicioTransient;
        private readonly ServicioScope servicioScope;
        private readonly ServicioSingleton servicioSingleton;

        public ServicioA(ILogger<ServicioA> logger,ServicioTransient servicioTransient,
        ServicioScope servicioScope,ServicioSingleton servicioSingleton)
        {
            this.logger=logger;
            this.servicioTransient=servicioTransient;
            this.servicioScope=servicioScope;
            this.servicioSingleton=servicioSingleton;
        }

        public Guid ObtenerTransient(){return servicioTransient.Guid ;}
        public Guid ObtenerScope(){return servicioScope.Guid ;}
        public Guid ObtenerSingleton(){return servicioSingleton.Guid ;}
        public void RealizarTarea(){
            throw new NotImplementedException();
        }
    }

    public class ServicioTransient
    {
        public Guid Guid =Guid.NewGuid();
    }
    public class ServicioScope
    {
        public Guid Guid =Guid.NewGuid();
    }
    public class ServicioSingleton
    {
        public Guid Guid =Guid.NewGuid();
    }

    public class ServicioB : IService
    {
        public Guid ObtenerScope()
        {
            throw new NotImplementedException();
        }

        public Guid ObtenerSingleton()
        {
            throw new NotImplementedException();
        }

        public Guid ObtenerTransient()
        {
            throw new NotImplementedException();
        }

        public void RealizarTarea()
        {
            throw new NotImplementedException();
        }
    }
}