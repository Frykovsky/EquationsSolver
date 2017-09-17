using System.Collections.Generic;
using Domain.Abstract;
using Domain.UsersDB;

namespace Domain.Concrete
{
    public class ExperimentsParametersRepository : IExperimentsRepository, IParametersRepository
    {
        private EFDbContext context = new EFDbContext();
        public IEnumerable<EquationParameters> EquationParameters
        {
            get { return context.EquationParameters; }
        }
        public IEnumerable<ExperimentsDB> Experiments
        {
            get { return context.ExperimentsDBs; }
        }
        public void SaveParams(EquationParameters equparams)
        {
            context.EquationParameters.Add(equparams);
            context.SaveChanges();
        }
        public EquationParameters DeleteParams(int ParId)
        {
            EquationParameters dbEntry = context.EquationParameters.Find(ParId);
            if(dbEntry != null)
            {
                context.EquationParameters.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public void SaveExperiment(ExperimentsDB expers)
        {
            //expers.UserEquationID = context.Summary.Where(x => x.User == expers.User).Count()+1;
            context.ExperimentsDBs.Add(expers);
            context.SaveChanges();
        }
        public ExperimentsDB DeleteExperiment(int ExpId)
        {
            ExperimentsDB dbEntry = context.ExperimentsDBs.Find(ExpId);
            if (dbEntry != null)
            {
                context.ExperimentsDBs.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

    }
}
