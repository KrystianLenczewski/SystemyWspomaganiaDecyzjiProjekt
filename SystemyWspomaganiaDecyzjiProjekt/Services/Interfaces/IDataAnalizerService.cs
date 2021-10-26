﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemyWspomaganiaDecyzjiProjekt.Models.ViewModels;

namespace SystemyWspomaganiaDecyzjiProjekt.Services.Interfaces
{
    public interface IDataAnalizerService
    {
        List<DiscretizeInterval> GetEqualDiscretizeIntervals(string columnName, int intervalCount);
    }
}
