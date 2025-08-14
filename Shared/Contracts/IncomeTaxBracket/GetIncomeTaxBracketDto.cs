using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Contracts.IncomeTaxBracket
{
    public sealed record GetIncomeTaxBracketDto
    (
    Guid Id,
    decimal LowerLimit,
    decimal? UpperLimit,
    decimal Rate,
    DateTime StartDate,
    DateTime? EndDate
    );
}
