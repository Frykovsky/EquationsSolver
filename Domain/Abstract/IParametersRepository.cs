using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.UsersDB;

namespace Domain.Abstract
{
    public interface IParametersRepository
    {
        IEnumerable<EquationParameters> EquationParameters { get; }

        void SaveParams(EquationParameters eqparams);

        EquationParameters DeleteParams(int EquID);
    }
}
