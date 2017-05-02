using ComputerWars.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComputerWars.DAL
{
    public interface IScoreDAL
    {
        void RecordScore(Player player);
    }
}