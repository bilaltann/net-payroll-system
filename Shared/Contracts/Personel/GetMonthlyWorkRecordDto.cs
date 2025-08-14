using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Contracts.Personel
{
    public  sealed record GetMonthlyWorkRecordDto
    (
        Guid Id,
        Guid EmployeeId,
        int Year,
        int Month,
        int WorkDays,
        decimal OvertimeHours
        
     );
}
