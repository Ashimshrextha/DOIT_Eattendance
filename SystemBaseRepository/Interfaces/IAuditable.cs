using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemBaseRepository.Interfaces
{
    public interface IAuditable<T>
    {
        T Id { get; set; }
        bool IsActive { get; set; }
        string TerminalIP { get; set; }
        string CreatedBy { get; set; }
        string UpdatedBy { get; set; }
        DateTime CreatedOn { get; set; }
        Nullable<DateTime> UpdatedOn { get; set; }
    }
}
