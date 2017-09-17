using System.Collections.Generic;
using Domain.UsersDB;

namespace Domain.Abstract
{
    public interface IExperimentsRepository
    {
        IEnumerable<ExperimentsDB> Experiments { get; }

        void SaveExperiment(ExperimentsDB exper);

        ExperimentsDB DeleteExperiment(int ExpID);
    }
}
