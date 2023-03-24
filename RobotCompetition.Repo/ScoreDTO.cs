using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotCompetition.Repo
{
    public class ScoreDTO
    {
        public int TeamCode { get; set; }
        public string? CheckpointCode { get; set; }
        public int ScoreValue { get; set; }
        public DateTime CheckTime { get; set; }
        public string? KeyId { get; set; }
    }
}

