﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unified.Domain.Entities;

namespace Unified.Domain.Interfaces
{
    public interface IAuditTrailRepository
    {
        Task<IEnumerable<AuditTrail>> GetAllAuditTrailAsync();
    }
}
